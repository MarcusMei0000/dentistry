namespace Dentistry.Test;

using Dentistry.Repository;
using Dentistry.Services;
using Dentistry.WebAPI.AppConfiguration;
using Dentistry.WebAPI.AppConfiguration.ServicesExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;

public class TestServices
{
    private readonly IServiceCollection services = new ServiceCollection();
    public IServiceProvider ServiceProvider { get; set; }

    public TestServices()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var logger = new LoggerConfiguration()
            .Enrich.WithCorrelationIdHeader()
            .Enrich.FromLogContext()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        services.AddHttpContextAccessor();
        services.AddSingleton<IConfiguration>(configuration);
        services.AddDbContextConfiguration(configuration);
        services.AddVersioningConfiguration();
        services.AddMapperConfiguration(); 
        services.AddControllers();
        services.AddSwaggerConfiguration(configuration);
        services.AddRepositoryConfiguration();
        services.AddBusinessLogicConfiguration(); 
        services.AddAuthorizationConfiguration(configuration);

        ServiceProvider = services.BuildServiceProvider();        

        RepositoryInitializer.InitializeRepository(ServiceProvider).GetAwaiter().GetResult();
    }

    public T Get<T>()
    {
        return ServiceProvider.GetService<T>();
    }    
}