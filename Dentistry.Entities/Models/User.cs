namespace Dentistry.Entities.Models;

public class User : BaseEntity
{
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Patronymic { get; set; }
    public string? PhotoLink { get; set; }
    public Role Role { get; set; }
}





