using DaviCodes.Api.User;

namespace DaviCodes.Api.Login;

public class LoginModel
{
    public UserModel User { get; set; }
    
    public object? ProvidedCredentials { get; set; }
}