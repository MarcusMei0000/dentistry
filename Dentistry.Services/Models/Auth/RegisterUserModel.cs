using Dentistry.Entities.Models;

namespace Dentistry.Services.Models;

public class RegisterUserModel
{
    public string Login { get; set; }
    public string Password { get; set; }
    public Guid RoleId { get; set; }
    public Role Role {get; set; }
}