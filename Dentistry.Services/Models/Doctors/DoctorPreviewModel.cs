using Dentistry.Entities.Models;
namespace Dentistry.Services.Models;

public class DoctorPreviewModel : UserPreviewModel
{
    public string Speciality { get; set; }
    public string? PhotoLink { get; set; }

}