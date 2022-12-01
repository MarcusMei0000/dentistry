using Dentistry.Entities.Models;

namespace Dentistry.Services.Models;

public class ScheduleModel : BaseModel
{
    public Guid DoctorId { get; set; }

    public DateTime ScheduleStart { get; set; }
    public DateTime ScheduleEnd { get; set; }

    public ICollection<Schedule> Schedules { get; set; }
}