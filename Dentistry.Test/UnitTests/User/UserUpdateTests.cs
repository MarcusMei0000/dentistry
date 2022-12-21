using Dentistry.Entities.Models;
using Dentistry.Services.Models;
using Dentistry.Shared.Exceptions;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Dentistry.Test;

[TestFixture]
public partial class UserTests
{
    [Test]
    public async Task UpdateUser_Success()
    {
        var model = new RegisterUserModel(){
            FirstName = "Test 1",
            LastName = "Test 2",
            Patronimyc = "Test 3",
            Password = "Test 4",
            Email = "test@test",
            Role = Entities.Models.Role.Admin            
        };

        var resultModel = await authService.RegisterUser(model);

        var newModel = new UpdateUserModel(){
            FirstName = "new first name",
            LastName = "new last name",
            Patronymic = "new patronymic"
        };

        var resultModel2 = userService.UpdateUser(resultModel.Id, newModel);

        Assert.AreEqual(resultModel.Email, resultModel2.Email);
        Assert.AreEqual(newModel.FirstName, resultModel2.FirstName);
        Assert.AreEqual(newModel.LastName, resultModel2.LastName);
        Assert.AreEqual(newModel.Patronymic, resultModel2.Patronymic);
        Assert.AreEqual(resultModel.Role, resultModel2.Role);
    }

    [Test]
    public async Task UpdateUser_NotExisting()
    {
        var newModel = new UpdateUserModel(){
            FirstName = "new first name",
            LastName = "new last name",
            Patronymic = "new patronymic"
        };
        var randonGuid = Guid.NewGuid();

        Assert.Throws<LogicException>( ()=>
        {
            var result = userService.UpdateUser(randonGuid, newModel); 
        });   
    }
}