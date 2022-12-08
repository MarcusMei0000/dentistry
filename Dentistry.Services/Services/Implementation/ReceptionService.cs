using AutoMapper;
using Dentistry.Entities.Models;
using Dentistry.Repository;
using Dentistry.Services.Abstract;
using Dentistry.Services.Models;

namespace Dentistry.Services.Implementation;

public class ReceptionService : IReceptionService
{
    private readonly IRepository<Reception> receptionsRepository;
    private readonly IMapper mapper;
    public ReceptionService(IRepository<Reception> receptionsRepository, IMapper mapper)
    {
        this.receptionsRepository = receptionsRepository;
        this.mapper = mapper;
    }

    public void DeleteReception(Guid id)
    {
        var receptionToDelete = receptionsRepository.GetById(id);
        if (receptionToDelete == null)
        {
            throw new Exception("Reception not found");
        }

        receptionsRepository.Delete(receptionToDelete);
    }
    
    public ReceptionModel CreateReception(CreateReceptionModel createReceptionModel, Guid ScheduleId, Guid PatientId)
    {
        if(receptionsRepository.GetAll(x => x.Id == createDoctorModel.Id).FirstOrDefault()!=null)
        {
            throw new Exception ("Attempt to create a non-unique object!");
        }

        if(schedulesRepository.GetAll(x => x.Id == createDoctorModel.ScheduleId).FirstOrDefault()!=null)
        {
            throw new Exception ("The object does not exist in the database!");
        }

        if(patientRepository.GetAll(x => x.Id == createDoctorModel.PatientId).FirstOrDefault() == null)
        {
            throw new Exception ("The object does not exist in the database!");
        }

        CreateReceptionModel createReception = new CreateReceptionModel();
        createReception.ScheduleId = createReceptionModel.ScheduleId;
        createReception.PatientId = createReceptionModel.PatientId;
        createReception.ReceptionDateTimeStart = createReceptionModel.ReceptionDateTimeStart;
        createReception.ReceptionDateTimeFinish = createReceptionModel.ReceptionDateTimeFinish;

        receptionsRepository.Save(mapper.Map<Ticket>(createReception));

        return createReception;
    }
    public ReceptionModel GetReception(Guid id)
    {
        var reception = receptionsRepository.GetById(id);
        return mapper.Map<ReceptionModel>(reception);
    }

    public PageModel<ReceptionPreviewModel> GetReceptions(int limit = 20, int offset = 0)
    {
        var receptions = receptionsRepository.GetAll();
        int totalCount = receptions.Count();
        var chunk = receptions.OrderBy(x => x.PatientId).Skip(offset).Take(limit);

        return new PageModel<ReceptionPreviewModel>()
        {
            Items = mapper.Map<IEnumerable<ReceptionPreviewModel>>(receptions),
            TotalCount = totalCount
        };
    }

    public ReceptionModel UpdateReception(Guid id, UpdateReceptionModel reception)
    {
        var existingReception = receptionsRepository.GetById(id);
        if (existingReception == null)
        {
            throw new Exception("Reception not found");
        }

        existingReception.Status = reception.Status;

        existingReception = receptionsRepository.Save(existingReception);
        return mapper.Map<ReceptionModel>(existingReception);
    }
}