using System.ComponentModel.DataAnnotations;
using DaviCodes.Api.DisplayName;
using DaviCodes.Api.User;

namespace DaviCodes.Api.AccountInfo;

public class AccountInfoModel
{
    [Required]
    [MaxLength(40)]
    public string Id { get; set; }

    public DisplayNameModel CurrentDisplayName { get; set; }
}