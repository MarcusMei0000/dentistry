using Dentistry.Services.Abstract;
using Dentistry.Services.Implementation;
using Dentistry.Services.MapperProfile;
using Microsoft.Extensions.DependencyInjection;

namespace Dentistry.WebAPI.AppConfiguration.ServicesExtensions;

public static partial class ServicesExtensions
{
      public static void AddBusinessLogicConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServicesProfile));

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IDoctorService, DoctorService>();
        services.AddScoped<IReceptionService, ReceptionService>();
        services.AddScoped<IScheduleService, ScheduleService>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
    }
}