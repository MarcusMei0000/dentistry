using Dentistry.Services.Models;

namespace Dentistry.Services.Abstract;

public interface IDoctorService
{
    DoctorModel CreateDoctor(CreateDoctorModel createDoctorModel, Guid ScheduleId, Guid SpecialityId);
    DoctorModel GetDoctor(Guid id);

    DoctorModel UpdateDoctor(Guid id, UpdateDoctorModel doctor);

    void DeleteDoctor(Guid id);

    PageModel<DoctorPreviewModel> GetDoctors(int limit = 20, int offset = 0);
}