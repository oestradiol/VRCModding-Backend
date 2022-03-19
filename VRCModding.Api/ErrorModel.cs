using Newtonsoft.Json;

namespace VRCModding.Api;

public class ErrorModel
{
    [JsonProperty("code")] 
    public string CodeStr { get; set; }

    [JsonIgnore] 
    public ErrorCodes Code 
    { 
        get 
        { 
            ErrorCodes res; 
            try 
            { 
                res = (ErrorCodes)Enum.Parse(typeof(ErrorCodes), CodeStr); 
            } 
            catch 
            { 
                res = ErrorCodes.Unknown; 
            } 
            return res; 
        } 
        set => CodeStr = value.ToString();
    }

    public string Message { get; set; }

    public Dictionary<string, string>? Details { get; set; }

    public override string ToString() 
    { 
        var detailsString = string.Empty; 
        if (Details is {Count: > 0})
            detailsString = Environment.NewLine + string.Join(Environment.NewLine, Details); 
        return $"{CodeStr} - {Message}{detailsString}"; 
    }
}