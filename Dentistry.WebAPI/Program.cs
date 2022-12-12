using Dentistry.WebAPI.AppConfiguration.ServicesExtensions;
using Dentistry.WebAPI.AppConfiguration.ApplicationExtensions;
using Dentistry.WebAPI.AppConfiguration;
using Dentistry.Entities;
using Microsoft.EntityFrameworkCore;
using Dentistry.Repository;
using Dentistry.Services;
using Serilog;

var configuration = new ConfigurationBuilder()
.AddJsonFile("appsettings.json", optional: false)
.Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddSerilogConfiguration();
builder.Services.AddDbContextConfiguration(configuration);
builder.Services.AddVersioningConfiguration();
builder.Services.AddMapperConfiguration();
builder.Services.AddControllers();
builder.Services.AddSwaggerConfiguration(configuration);

builder.Services.AddRepositoryConfiguration();
builder.Services.AddBusinessLogicConfiguration();
builder.Services.AddAuthorizationConfiguration(configuration);

builder.Services.AddScoped<DbContext, Context>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();
await RepositoryInitializer.InitializeRepository(app.Services);
app.UseSerilogConfiguration();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration();
}

app.UseHttpsRedirection();

app.UseAuthorizationConfiguration();
app.UseMiddleware(typeof(ExceptionsMiddleware));
app.MapControllers();

try
{
    Log.Information("Application starting...");

    app.Run();
}
catch (Exception ex)
{
    Log.Error("Application finished with error {error}", ex);
}
finally
{
    Log.Information("Application stopped");
    Log.CloseAndFlush();
}