using System.Security.Claims;
using DaviCodes.Api;
using DaviCodes.Api.Account;
using DaviCodes.Business;
using DaviCodes.Business.Services;
using DaviCodes.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DaviCodes.Controllers;

[ApiController]
[Route("api/v1/account")]
public class AccountController : ControllerBase {
	private readonly AccountService accountService;
    private readonly ExceptionBuilder exceptionBuilder;

    public AccountController(AccountService accountService, ExceptionBuilder exceptionBuilder) {
	    this.accountService = accountService;
        this.exceptionBuilder = exceptionBuilder;
    }

    /// <summary>
    /// On Post, will receive account id and display name. If finds in db, will update the display name. If not, will create a new account. If Id or DisplayName is null, will throw an exception.
    /// </summary>
    /// <param name="accountData">Contains account display name and ID</param>
    /// <returns>HTTP OK Response</returns>
    /// <exception cref="ApiException"></exception>
    [HttpPost]
    [AuthorizePerms(RoleEnum = Permissions.User | Permissions.Admin)]
    public async Task<IActionResult> CreateOrUpdateAsync([FromBody] AccountData accountData) { // No need to check if null at displayNameRepo and usedDisplayNameRepo (for now), since already checking here.
		var account = await accountService.TryGetAsync(accountData.Id, true);
		if (account == null) {
			await accountService.CreateAsync(accountData.Id, new Guid(User.Claims.First(c => c.Type == ClaimTypes.PrimarySid).Value), accountData.CurrentDisplayName);
		} else {
			await accountService.UpdateAsync(account, accountData.CurrentDisplayName);
		}

		return Ok();
	}
}