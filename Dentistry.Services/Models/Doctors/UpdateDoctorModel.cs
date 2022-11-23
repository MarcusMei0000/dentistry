using Dentistry.Entities.Models;

namespace Dentistry.Services.Models;

public class UpdateDoctorModel : UpdateUserModel
{    public short ReceptionRoom { get; set; }
    public virtual ICollection<Schedule> Schedules { get; set; }

}