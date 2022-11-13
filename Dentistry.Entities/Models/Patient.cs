namespace Dentistry.Entities.Models;

public class Patient : BaseEntity
{
    public Guid UserId { get; set; }
    public DateTime Birthday { get; set; }
    public long InsurancePolicy { get; set; }
    public virtual ICollection<Reception> Receptions { get; set; }
}