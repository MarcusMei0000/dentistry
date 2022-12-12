using Dentistry.Entities.Models;

namespace Dentistry.Services.Models;
public class CreateScheduleModel : BaseModel
{
    public Guid DoctorId { get; set; }
    public Doctor Doctor { get; set; }

    
    public DateTime ReceptionStart { get; set; }
    public DateTime ReceptionEnd { get; set; }

    public ICollection<Reception> Receptions { get; set; }
}