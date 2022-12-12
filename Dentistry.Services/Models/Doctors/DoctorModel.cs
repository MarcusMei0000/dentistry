using Dentistry.Entities.Models;

namespace Dentistry.Services.Models;

public class DoctorModel : BaseModel
{
     public short ReceptionRoom { get; set; }

    public Speciality Speciality { get; set; }
    public ICollection<Schedule> Schedules { get; set; }
}