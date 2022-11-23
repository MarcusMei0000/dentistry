namespace Dentistry.WebAPI.Models;
using Dentistry.Entities.Models;
public class DoctorPreviewResponse : UserPreviewResponse
{
    public string Speciality { get; set; }
    public string? PhotoLink { get; set; }
}