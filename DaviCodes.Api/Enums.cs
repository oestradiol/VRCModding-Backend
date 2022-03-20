namespace DaviCodes.Api;

public enum ErrorCodes
{ 
    Unknown ,
    InsufficientCredentials,
    HwidIsRequired,
    UserIdIsRequired,
    IpIsRequired,
    FailedToFetchIp,
    FailedToDeduceUser,
    UserHoneypot
}