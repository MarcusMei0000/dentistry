namespace Dentistry.WebAPI.Models;
using Dentistry.Entities.Models;
public class ReceptionPreviewResponse
{
    public Guid PatientId { get; set; }
    public DateTime ReceptionDateTimeStart { get; set; }
    public Guid ScheduleId { get; set; }
    public Status Status { get; set; }
}