using Dentistry.Entities.Models;

namespace Dentistry.Services.Models;

public class DoctorModel : UserModel
{
    public Speciality Speciality { get; set; }
}