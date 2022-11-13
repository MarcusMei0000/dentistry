namespace Dentistry.Entities.Models;

public class Speciality : BaseEntity
{
    public Guid SpecialityId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public virtual ICollection<DoctorSpeciality> Doctors { get; set; }

}