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
    public async Task GetReception_Success()
    {
        var model = new ReceptionModel(){
            ReceptionDateTimeStart = DateTime.Today, 
            ReceptionDateTimeFinish = DateTime.Now,
            Status = Status.Assigned
        };
        
        var receptionId = new Guid("E40B66BC-84CE-407D-A3B4-033A340D1E58");

        var reception = receptionService.GetReception(receptionId);

        Assert.AreEqual(model.ReceptionDateTimeStart, reception.ReceptionDateTimeStart);
        Assert.AreEqual(model.ReceptionDateTimeFinish, reception.ReceptionDateTimeFinish);
        Assert.AreEqual(model.Status, reception.Status);
    }

    [Test]
    public async Task GetReception_NotExisting()
    {
        var receptionId = new Guid("E40B66BC-84CE-407D-A3B4-033A340D1E68");

        Assert.Throws<Exception>( ()=>
        {
            receptionService.GetReception(receptionId);
        });   
    }
}