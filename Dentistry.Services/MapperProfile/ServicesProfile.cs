using AutoMapper;
using Dentistry.Entities.Models;
using Dentistry.Services.Models;

namespace Dentistry.Services.MapperProfile;

public class ServicesProfile : Profile
{
    public ServicesProfile() 
    {
        #region Users

        CreateMap<User, UserModel>().ReverseMap();
        CreateMap<User, UserPreviewModel>()
            .ForMember(x => x.FullName, y => y.MapFrom(u => $"{u.LastName} {u.FirstName} {u.Patronymic}"));

        #endregion

        #region Doctors

        CreateMap<Doctor, DoctorModel>().ReverseMap();
        CreateMap<Doctor, DoctorPreviewModel>()
            .ForMember(x => x.Speciality, y => y.MapFrom(u => u.Speciality));

        #endregion

        #region Receptions

        CreateMap<Reception, ReceptionModel>().ReverseMap();
        CreateMap<Reception, ReceptionPreviewModel>();

        #endregion

        #region Schedule

        CreateMap<Schedule, ScheduleModel>().ReverseMap();
        CreateMap<Schedule, SchedulePreviewModel>();

        #endregion
    }
}