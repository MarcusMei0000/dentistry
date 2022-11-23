using Dentistry.Entities.Models;

namespace Dentistry.Services.Models;

public class UpdateUserModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
}