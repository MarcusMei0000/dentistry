namespace Dentistry.Services.Models;

public class BaseModel
{
    public Guid Id { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime ModificationTime { get; set; }
}