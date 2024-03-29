using System.ComponentModel.DataAnnotations;
using VRCModding.Api;

namespace VRCModding.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; }
    
    public string? Name { get; set; }
    
    // ReSharper disable once InconsistentNaming
    public List<Hwid> KnownHWIDs { get; set; }
    public List<Ip> KnownIPs { get; set; }
    public List<Account> KnownAccounts { get; set; }

    public Permissions Permissions { get; set; } = Permissions.None;
    
    public DateTime LastLogin { get; set; } = DateTime.UtcNow;
    public DateTime CreationDateUtc { get; set; } = DateTime.UtcNow;
}

public interface IUserInfo
{
	public string Id { get; set; }
    
	// ReSharper disable once InconsistentNaming
	public Guid UserFK { get; set; }
	public User User { get; set; }
    
	public DateTime LastLogin { get; set; }
}
