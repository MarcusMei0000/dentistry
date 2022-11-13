
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Dentistry.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dentistry.WebAPI.AppConfiguration.ServicesExtensions;

public static partial class ServicesExtensions
{
    /// <summary>
    /// Add serilog configuration
    /// </summary>
    /// <param name="builder"></param>
    public static void AddDbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Context");
        services.AddDbContext<Context>(options =>
        {
            options
                .UseLazyLoadingProxies()
                .UseSqlServer(connectionString, sqlServerOption =>
                        {
                            sqlServerOption.CommandTimeout(60 * 60);
                        });

        }); 
        services.AddDatabaseDeveloperPageExceptionFilter();
    }
    
}



