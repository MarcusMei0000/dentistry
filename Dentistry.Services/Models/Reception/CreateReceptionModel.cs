using Dentistry.Entities.Models;

namespace Dentistry.Services.Models;
public class CreateReceptionModel
{
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; }

    public Guid ScheduleId { get; set; }
    public Schedule Schedule { get; set; }

    public DateTime ReceptionDateTimeStart { get; set; }
    public DateTime ReceptionDateTimeFinish { get; set; }

    public Status Status { get; set; }
}