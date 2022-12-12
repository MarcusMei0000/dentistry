using Dentistry.Entities.Models;
namespace Dentistry.Services.Models;

public class SchedulePreviewModel
{
    public Guid DoctorId { get; set; }

    public DateTime ReceptionStart { get; set; }
    public DateTime ReceptionEnd { get; set; }

    public ICollection<Schedule> Schedules { get; set; }
}