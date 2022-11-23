namespace Dentistry.WebAPI.Models;
using Dentistry.Entities.Models;
public class DoctorResponse : UserResponse
{
    public Speciality Speciality { get; set; }
}