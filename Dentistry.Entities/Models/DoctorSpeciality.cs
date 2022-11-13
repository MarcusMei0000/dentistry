namespace Dentistry.Entities.Models;
public class DoctorSpeciality : BaseEntity
{
    public Guid DoctorId { get; set; }
    public virtual Doctor Doctor { get; set; }

    public Guid SpecialityId { get; set; }
    public virtual Speciality Speciality { get; set; }

}