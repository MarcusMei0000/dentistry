using Dentistry.Entities.Models;
namespace Dentistry.Services.Models;

public class ReceptionPreviewModel
{
    public Guid PatientId { get; set; }
    public DateTime ReceptionDateTimeStart { get; set; }
    public Guid ScheduleId { get; set; }
    public Status Status { get; set; }
}