using AutoMapper;
using Dentistry.WebAPI.Models;
using Dentistry.Services.Models;

namespace Dentistry.WebAPI.MapperProfile;

public class PresentationProfile : Profile
{
    public PresentationProfile()
    {
        #region  Pages

        CreateMap(typeof(PageModel<>), typeof(PageResponse<>));

        #endregion

        #region Users

        CreateMap<UserModel, UserResponse>();
        CreateMap<UpdateUserRequest, UpdateUserModel>();
        CreateMap<UserPreviewModel, UserPreviewResponse>();

        #endregion

        #region Doctors

        CreateMap<DoctorModel, DoctorResponse>();
        CreateMap<UpdateDoctorRequest, UpdateDoctorModel>();
        CreateMap<DoctorPreviewModel, DoctorPreviewResponse>();

        #endregion
        
        #region Receptions

        CreateMap<ReceptionModel, ReceptionResponse>();
        CreateMap<UpdateReceptionRequest, UpdateReceptionModel>();
        CreateMap<ReceptionPreviewModel, ReceptionPreviewResponse>();

        #endregion
        
        #region Schedules

        CreateMap<ScheduleModel, ScheduleResponse>();
        CreateMap<UpdateScheduleRequest, UpdateScheduleModel>();
        CreateMap<SchedulePreviewModel, SchedulePreviewResponse>();

        #endregion
    }
}