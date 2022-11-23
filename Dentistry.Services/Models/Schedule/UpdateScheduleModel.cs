using Dentistry.Entities.Models;

namespace Dentistry.Services.Models;

public class UpdateScheduleModel
{
    public DateTime ReceptionStart { get; set; }
    public DateTime ReceptionEnd { get; set; }

    public ICollection<Reception> Receptions { get; set; }
}