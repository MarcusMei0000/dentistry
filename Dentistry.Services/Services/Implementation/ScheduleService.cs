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
    public ScheduleModel CreateSchedule(CreateScheduleModel createScheduleModel, Guid DoctorId, Guid ReceptionId)
    { 
        if(schedulesRepository.GetAll(x => x.Id == createScheduleModel.Id).FirstOrDefault()!=null)
        {
            throw new Exception ("Attempt to create a non-unique object!");
        }

        if(doctorsRepository.GetAll(x => x.Id == createScheduleModel.DoctorId).FirstOrDefault()!=null)
        {
            throw new Exception ("The object does not exist in the database!");
        }

        if(receptionsRepository.GetAll(x => x.Id == createScheduleModel.ReceptionId).FirstOrDefault() == null)
        {
            throw new Exception ("The object does not exist in the database!");
        }

        CreateScheduleModel createSchedule = new CreateScheduleModel();
        createSchedule.DoctorId = createScheduleModel.DoctorId;
        createSchedule.ReceptionId = createScheduleModel.ReceptionId;
        createSchedule.ReceptionStart = createScheduleModel.ReceptionStart;
        createSchedule.ReceptionEnd = createScheduleModel.ReceptionEnd;
        schedulesRepository.Save(mapper.Map<Ticket>(createSchedule));

        return createSchedule;
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