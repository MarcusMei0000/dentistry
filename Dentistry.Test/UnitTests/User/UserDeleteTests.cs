using Dentistry.Services.Models;
using Dentistry.Shared.Exceptions;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Dentistry.Test;

[TestFixture]
public partial class UserTests
{
    [Test]
    public async Task DeleteUser_Success()
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
        userService.DeleteUser(resultModel.Id);
        
        Assert.Throws<LogicException>(()=>
            {
                var checkUser = userService.GetUser(resultModel.Id);
            }
        );
    }

    [Test]
    public async Task DeleteUser_NotExisting()
    {
        var randomGuid = Guid.NewGuid();
        Assert.Throws<LogicException>(()=>
            userService.DeleteUser(randomGuid)
        );
    }
}