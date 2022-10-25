using Dentistry.WebAPI.AppConfigure.ApplicationExtensions;
using Dentistry.WebAPI.AppConfigure.ServicesExtensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilogConfiguration(); //Add serilog
builder.Services.AddVersioningConfiguration(); //add api versioning
builder.Services.AddControllers();
builder.Services.AddSwaggerConfiguration(); //add swagger configuration

var app = builder.Build();

app.UseSerilogConfiguration(); //use serilog

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration(); //use swagger
}

app.UseHttpsRedirection();
app.UseAuthorization();
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
