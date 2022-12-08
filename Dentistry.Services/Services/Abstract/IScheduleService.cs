using Dentistry.Services.Models;

namespace Dentistry.Services.Abstract;

public interface IScheduleService
{
    ScheduleModel CreateSchedule(CreateScheduleModel ScheduleModell, Guid DoctorId, Guid ReceptionId);
    ScheduleModel GetSchedule(Guid id);

    ScheduleModel UpdateSchedule(Guid id, UpdateScheduleModel schedule);

    void DeleteSchedule(Guid id);

    PageModel<SchedulePreviewModel> GetSchedules(int limit = 20, int offset = 0);
}