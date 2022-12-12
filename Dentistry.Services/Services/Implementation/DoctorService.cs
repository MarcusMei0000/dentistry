using AutoMapper;
using Dentistry.Entities.Models;
using Dentistry.Repository;
using Dentistry.Services.Abstract;
using Dentistry.Services.Models;

namespace Dentistry.Services.Implementation;

public class DoctorService : IDoctorService
{
    private readonly IRepository<Doctor> doctorsRepository;
    private readonly IRepository<Schedule> schedulesRepository;
    private readonly IRepository<Speciality> specialityRepository;
    private readonly IMapper mapper;
    public DoctorService(IRepository<Doctor> doctorsRepository, IRepository<Schedule> schedulesRepository, IRepository<Speciality> specialityRepository, IMapper mapper)
    {
        this.doctorsRepository = doctorsRepository;
        this.schedulesRepository = schedulesRepository;
        this.specialityRepository = specialityRepository;
        this.mapper = mapper;
    }
    public DoctorModel CreateDoctor(CreateDoctorModel createDoctorModel, Guid ScheduleId, Guid SpecialityId)
    {
        if(doctorsRepository.GetAll(x => x.Id == createDoctorModel.Id).FirstOrDefault()!=null)
        {
            throw new Exception ("Attempt to create a non-unique object!");
        }

        DoctorModel createDoctor = new DoctorModel();
        createDoctor.Schedules = createDoctorModel.Schedules;
        createDoctor.Speciality = createDoctorModel.Speciality;
        createDoctor.ReceptionRoom = createDoctorModel.ReceptionRoom;
        doctorsRepository.Save(mapper.Map<Doctor>(createDoctor));

        return createDoctor;
    }

    public void DeleteDoctor(Guid id)
    {
        var doctorToDelete = doctorsRepository.GetById(id);
        if (doctorToDelete == null)
        {
            throw new Exception("Doctor not found");
        }

        doctorsRepository.Delete(doctorToDelete);
    }

    public DoctorModel GetDoctor(Guid id)
    {
        var doctor = doctorsRepository.GetById(id);
        return mapper.Map<DoctorModel>(doctor);
    }

    public PageModel<DoctorPreviewModel> GetDoctors(int limit = 20, int offset = 0)
    {
        var doctors = doctorsRepository.GetAll();
        int totalCount = doctors.Count();
     
        var chunk = doctors.OrderBy(x => x.UserId).Skip(offset).Take(limit);

        return new PageModel<DoctorPreviewModel>()
        {
            Items = mapper.Map<IEnumerable<DoctorPreviewModel>>(doctors),
            TotalCount = totalCount
        };
    }

    public DoctorModel UpdateDoctor(Guid id, UpdateDoctorModel doctor)
    {
        var existingDoctor = doctorsRepository.GetById(id);
        if (existingDoctor == null)
        {
            throw new Exception("Doctor not found");
        }

        existingDoctor.ReceptionRoom = doctor.ReceptionRoom;
        existingDoctor.Schedules = doctor.Schedules;

        existingDoctor = doctorsRepository.Save(existingDoctor);
        return mapper.Map<DoctorModel>(existingDoctor);
    }
}