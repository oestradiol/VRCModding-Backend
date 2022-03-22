using System.ComponentModel.DataAnnotations;
using DaviCodes.Api.DisplayName;

namespace DaviCodes.Api.Account;

public class AccountModel
{
    [Required]
    [MaxLength(40)]
    public string Id { get; set; }

    public DisplayNameModel CurrentDisplayName { get; set; }
}