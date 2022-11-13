namespace Dentistry.Entities.Models;

public class Reception : BaseEntity
{
    public Guid PatientId { get; set; }
    public virtual Patient Patient { get; set; }

    public Guid ScheduleId { get; set; }
    public virtual Schedule Schedule { get; set; }

    public DateTime ReceptionDateTimeStart { get; set; }
    public DateTime ReceptionDateTimeFinish { get; set; }

    public Status Status { get; set; } //?

}
