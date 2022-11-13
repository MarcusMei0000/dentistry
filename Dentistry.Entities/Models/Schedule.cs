namespace Dentistry.Entities.Models;

public class Schedule : BaseEntity
{
    public Guid DoctorId { get; set; }
    public virtual Doctor Doctor { get; set; }

    public DateTime ReceptionStart { get; set; }
    public DateTime ReceptionEnd { get; set; }

    public virtual ICollection<Reception> Receptions { get; set; }
}