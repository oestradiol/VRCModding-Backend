using System.ComponentModel.DataAnnotations;

namespace VRCModding.Entities;

public class User
{
    [Key]
    public Guid Id { get; set; }
    
    public string? Name { get; set; }
    
    public List<Hwid> KnownHWIDs { get; set; }
    public List<Ip> KnownIPs { get; set; }
    public List<Account> Accounts { get; set; }
    
    public DateTime LastLogin { get; set; } = DateTime.UtcNow;
    public DateTime CreationDateUtc { get; set; } = DateTime.UtcNow;
}

public interface IUserInfo
{
	public string? Id { get; set; }
    
	public Guid UserFK { get; set; }
	public User User { get; set; }
    
	public DateTime LastLogin { get; set; } // Todo: don't forget to update time parameters whenever login occurs
}
