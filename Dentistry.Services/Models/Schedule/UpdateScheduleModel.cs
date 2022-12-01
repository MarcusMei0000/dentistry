using Dentistry.Entities.Models;

namespace Dentistry.Services.Models;

public class UpdateScheduleModel
{
    public DateTime ScheduleStart { get; set; }
    public DateTime ScheduleEnd { get; set; }

    public ICollection<Reception> Receptions { get; set; }
}