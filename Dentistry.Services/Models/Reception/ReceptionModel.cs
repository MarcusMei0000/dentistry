using Dentistry.Entities.Models;

namespace Dentistry.Services.Models;

public class ReceptionModel : BaseModel
{
    public Guid PatientId { get; set; }
    public DateTime ReceptionDateTimeStart { get; set; }    
    public DateTime ReceptionDateTimeFinish { get; set; }
    public Guid ScheduleId { get; set; }
    public Status Status { get; set; }
}