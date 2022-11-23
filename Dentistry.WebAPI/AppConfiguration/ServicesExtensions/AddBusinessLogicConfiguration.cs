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

        //services
        services.AddScoped<IUserService, UserService>();
    }
}