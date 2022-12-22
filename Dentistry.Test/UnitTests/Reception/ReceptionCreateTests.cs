using Dentistry.Entities.Models;
using Dentistry.Services.Models;
using Dentistry.Shared.Exceptions;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Dentistry.Test;

[TestFixture]
public partial class ReceptionTests
{
    [Test]
    public async Task CreateReception_Success()
    {
        var model = new CreateReceptionModel(){
            ReceptionDateTimeStart = DateTime.Today, 
            ReceptionDateTimeFinish = DateTime.Now,
            Status = Status.Assigned
        };
        
        var PatientId = new Guid("E40B66BC-84CE-407D-A3B4-033A340D1E68");
        
        var ScheduleId = new Guid("E40B66BC-84CE-407D-A3B4-033A340D1658");

        var addReception = receptionService.CreateReception(model, PatientId, ScheduleId);

        var reception = receptionService.GetReception(addReception.Id);

        Assert.AreEqual(PatientId, reception.PatientId);
        Assert.AreEqual(model.ReceptionDateTimeStart, reception.ReceptionDateTimeStart);
        Assert.AreEqual(model.ReceptionDateTimeFinish, reception.ReceptionDateTimeFinish);         
        Assert.AreEqual(model.Status, reception.Status);         
        Assert.AreEqual(ScheduleId, reception.ScheduleId);
    }

    [Test]
    public async Task CreateReception_NotExisting()
    {
        var model = new CreateReceptionModel(){
            ReceptionDateTimeStart = DateTime.Today, 
            ReceptionDateTimeFinish = DateTime.Now,
            Status = Status.Assigned
        };
        var PatientId = new Guid("E40B66BC-84CE-407D-A3B4-033A340D1E68");
        
        var ScheduleId = new Guid("E40B66BC-84CE-407D-A3B4-033A340D1658");

        var addReception = receptionService.CreateReception(model, PatientId, ScheduleId);

        Assert.Throws<Exception>( ()=>
        {
            receptionService.CreateReception(model,PatientId, ScheduleId);
        });   
    }
}