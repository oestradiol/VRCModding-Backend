using DaviCodes.Api;
using Microsoft.AspNetCore.Authorization;

namespace DaviCodes.Helpers; 

public class AuthorizePerms : AuthorizeAttribute {
	private Permissions roleEnum;
	public Permissions RoleEnum
	{
		get => roleEnum;
		set {
			roleEnum = value; 
			Roles = value.ToString();
		}
	}
}
