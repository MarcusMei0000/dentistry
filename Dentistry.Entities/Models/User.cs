using Microsoft.AspNetCore.Identity;
namespace Dentistry.Entities.Models;

public class User : IdentityUser<Guid>, IBaseEntity
{
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Patronymic { get; set; }
    public string? PhotoLink { get; set; }
    public Guid RoleId { get; set; }
    public virtual Role Role { get; set; }
    public string Login { get; set; }    
    public string RoleName { get; set; }

     #region BaseEntity

    public DateTime CreationTime { get; set; }
    public DateTime ModificationTime { get; set; }

    public bool IsNew()
    {
        return Id == Guid.Empty;
    }

    public void Init()
    {
        Id = Guid.NewGuid();
        CreationTime = DateTime.UtcNow;
        ModificationTime = DateTime.UtcNow;
    }

    #endregion    
}

public class UserRole : IdentityRole<Guid>
{
    
}





