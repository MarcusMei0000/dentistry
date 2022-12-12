using AutoMapper;
using Dentistry.Entities.Models;
using Dentistry.Repository;
using Dentistry.Services.Abstract;
using Dentistry.Services.Models;

namespace Dentistry.Services.Implementation;

public class ReceptionService : IReceptionService
{
    private readonly IRepository<Reception> receptionsRepository;
    private readonly IRepository<Patient> patientRepository;
    private readonly IMapper mapper;
    public ReceptionService(IRepository<Reception> receptionsRepository, IRepository<Patient> patientRepository, IMapper mapper)
    {
        this.receptionsRepository = receptionsRepository;
        this.patientRepository = patientRepository;
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
        if(receptionsRepository.GetAll(x => x.Id == createReceptionModel.Id).FirstOrDefault()!=null)
        {
            throw new Exception ("Attempt to create a non-unique object!");
        }

        if(patientRepository.GetAll(x => x.Id == createReceptionModel.PatientId).FirstOrDefault() == null)
        {
            throw new Exception ("The object does not exist in the database!");
        }

        ReceptionModel createReception = new ReceptionModel();
        createReception.ScheduleId = createReceptionModel.ScheduleId;
        createReception.PatientId = createReceptionModel.PatientId;
        createReception.ReceptionDateTimeStart = createReceptionModel.ReceptionDateTimeStart;
        createReception.ReceptionDateTimeFinish = createReceptionModel.ReceptionDateTimeFinish;

        receptionsRepository.Save(mapper.Map<Reception>(createReception));

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