using AutoMapper;
using Dentistry.Entities.Models;
using Dentistry.Repository;
using Dentistry.Services.Abstract;
using Dentistry.Services.Models;

namespace Dentistry.Services.Implementation;

public class ScheduleService : IScheduleService
{
    private readonly IRepository<Schedule> schedulesRepository;
    private readonly IMapper mapper;
    public ScheduleService(IRepository<Schedule> schedulesRepository, IMapper mapper)
    {
        this.schedulesRepository = schedulesRepository;
        this.mapper = mapper;
    }
    public ScheduleModel CreateSchedule(CreateScheduleModel createScheduleModel)
    {
        Schedule schedule = mapper.Map<Schedule>(createScheduleModel);
        return mapper.Map<ScheduleModel>(schedulesRepository.Save(schedule));
    }

    public void DeleteSchedule(Guid id)
    {
        var scheduleToDelete = schedulesRepository.GetById(id);
        if (scheduleToDelete == null)
        {
            throw new Exception("Schedule not found");
        }

        schedulesRepository.Delete(scheduleToDelete);
    }

    public ScheduleModel GetSchedule(Guid id)
    {
        var schedule = schedulesRepository.GetById(id);
        return mapper.Map<ScheduleModel>(schedule);
    }

    public PageModel<SchedulePreviewModel> GetSchedules(int limit = 20, int offset = 0)
    {
        var schedules = schedulesRepository.GetAll();
        int totalCount = schedules.Count();
        var chunk = schedules.OrderBy(x => x.DoctorId).Skip(offset).Take(limit);

        return new PageModel<SchedulePreviewModel>()
        {
            Items = mapper.Map<IEnumerable<SchedulePreviewModel>>(schedules),
            TotalCount = totalCount
        };
    }

    public ScheduleModel UpdateSchedule(Guid id, UpdateScheduleModel schedule)
    {
        var existingSchedule = schedulesRepository.GetById(id);
        if (existingSchedule == null)
        {
            throw new Exception("Schedule not found");
        }

        existingSchedule.ReceptionStart = schedule.ScheduleStart;
        existingSchedule.ReceptionEnd = schedule.ScheduleEnd;
        existingSchedule.Receptions = schedule.Receptions;

        existingSchedule = schedulesRepository.Save(existingSchedule);
        return mapper.Map<ScheduleModel>(existingSchedule);
    }
}