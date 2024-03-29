﻿using VRCModding.Api.Login;
using VRCModding.Business;
using VRCModding.Business.Services;
using VRCModding.Entities;
using VRCModding.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VRCModding.Controllers;

[ApiController]
[Route("api/v1/login")]
public class LoginController : ControllerBase {
	private readonly UserService userService;
	private readonly AccountService accountService;
    private readonly HwidService hwidService;
    private readonly IpService ipService;
    private readonly TokenGenerator tokenGenerator;
    private readonly ModelConverter modelConverter;
    private readonly ExceptionBuilder exceptionBuilder;

    public LoginController(UserService userService, AccountService accountService, HwidService hwidService, IpService ipService, TokenGenerator tokenGenerator, ModelConverter modelConverter, ExceptionBuilder exceptionBuilder) {
	    this.userService = userService;
	    this.accountService = accountService;
        this.hwidService = hwidService;
        this.ipService = ipService;
        this.tokenGenerator = tokenGenerator;
        this.modelConverter = modelConverter;
        this.exceptionBuilder = exceptionBuilder;
    }

    /// <summary>
    /// Tries to either create a new user (if none found) or deduce who the user is (if no ambiguity) through the received data.
    /// Will update the user if extra information is provided.
    /// </summary>
    /// <param name="loginData">Contains user HWID and ID</param>
    /// <returns>UserModel with obtained or generated user's data</returns>
    /// <exception cref="ApiException"></exception>
    [HttpPost]
    [AllowAnonymous]
    public async Task<LoginModel> LoginAsync([FromBody] LoginData loginData) {
	    var ip = "";

	    // Checks if parameters fulfill minimum of two to authenticate
	    var info = new [] { loginData.Hwid, loginData.LastAccountId, ip }.Where(s => !string.IsNullOrEmpty(s));
	    var infoAsStrings = info as string?[] ?? info.ToArray();
	    if (infoAsStrings.Length < 2)
		    throw exceptionBuilder.Api(Api.ErrorCodes.InsufficientCredentials, infoAsStrings);

	    //// Mounts info array and possible GUIDs array. Detail: IUserInfos cannot contain null UserFK/User.
	    // Boxed because these need to be casted back to each type later on update
	    var userInfosBoxed = new (string? id, object? userInfo)[] {
		    (loginData.Hwid, await hwidService.TryGetAsync(loginData.Hwid)), 
		    (loginData.LastAccountId, await accountService.TryGetAsync(loginData.LastAccountId)), 
		    (ip, await ipService.TryGetAsync(ip))
	    };
	    
	    // Unboxing because values need to be accessed here
	    var infoUnboxed = userInfosBoxed.Select(ui => (ui.id, (IUserInfo?)ui.userInfo));
	    var infoUnboxedArr = infoUnboxed as (string? id, IUserInfo? userInfo)[] ?? infoUnboxed.ToArray();
	    
	    // Grouping by Guid to separate each case later on
	    var infoGroupedByUserFks = infoUnboxedArr.GroupBy(d => d.userInfo?.UserFK).Where(g => g.Key != null);
	    var infoGroupedByUserFksArr = infoGroupedByUserFks as IGrouping<Guid?,(string? id, IUserInfo? userInfo)>[] ?? infoGroupedByUserFks.ToArray();
	    ////
	    
	    // Todo: Report user login attempts below to Discord bot
	    // Filters out any null userInfos and then tries to deduce who the user is
	    var user = infoGroupedByUserFksArr.Length switch {
		    0 => await userService.CreateAsync(userInfosBoxed), // User provided enough info but wasn't known
		    1 => infoGroupedByUserFksArr[0].Count() switch { // User was known
			    1 => throw exceptionBuilder.Api(Api.ErrorCodes.UserHoneypot, infoUnboxedArr), // Todo: Only one piece of data was known about user, so honeypot. Notify Discord with data, but don't approve yet.
			    _ => await userService.UpdateAsync((await userService.TryGetByIdAsync(infoGroupedByUserFksArr[0].Key!.Value))!, userInfosBoxed, isLogin: true), // User provided enough and/or known/unknown extra data and so we check that, and store any new info. Notify login
		    },
		    _ => throw exceptionBuilder.Api(Api.ErrorCodes.FailedToDeduceUser, infoUnboxedArr) // Todo: Failed to deduce user. Improve to detect users with multiple instances in db. Attempt merge (2 point to one user, 1 point to anonymous) and then login if possible, or honeypot (1 point to one user, 2 point to anonymous). Don't forget to notify Discord.
	    };
	    
		var loginModel = new LoginModel {
			User = modelConverter.ToModel(user), 
			ProvidedCredentials = modelConverter.ToModel(infoUnboxedArr) // Todo: Once finished debugging, clean this up and only show main credentials (infoAsStrings).
		};

		if (!string.IsNullOrEmpty(loginModel.User.Name)) { // Todo: This will go away once honeypot is done, as it'll be throwing an API exception instead.
			loginModel.BearerToken = tokenGenerator.GenerateToken(loginModel);
		}
		
		return loginModel;
    }
}