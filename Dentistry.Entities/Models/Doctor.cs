namespace Dentistry.Entities.Models;
public class Doctor : BaseEntity
{
    public Guid UserId { get; set; }
    public short ReceprionRoom { get; set; }

    public virtual ICollection<DoctorSpeciality> Specialities { get; set; }
    public virtual ICollection<Schedule> Schedules { get; set; }
}
