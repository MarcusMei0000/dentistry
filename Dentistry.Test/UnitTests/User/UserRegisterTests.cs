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
    public async Task RegisterUser_Success()
    {
        var model = new RegisterUserModel(){
            Login = "t@tt",
            Password = "test1", 
            Role = Entities.Models.Role.Admin            
        };

        var resultModel = await authService.RegisterUser(model);
        Assert.AreEqual(model.Login, resultModel.Login);
        Assert.AreEqual(model.Role, resultModel.Role);

        var user = userRepository.GetById(resultModel.Id);
        
        var signInManager = services.Get<SignInManager<User>>();
        var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        Assert.AreEqual(result.Succeeded, true);
    }

    [Test]
    public async Task RegisterUser_EmailExists()
    {
        var model = new RegisterUserModel(){           
            Login = "t@tt",
            Password = "test1", 
            Role = Entities.Models.Role.Admin               
        };

        var resultModel = await authService.RegisterUser(model);
        Assert.ThrowsAsync<LogicException>(async ()=>
        {
            var result2 = await authService.RegisterUser(model); 
        });   
    }

    [Test]
    [TestCaseSource(typeof(UserCaseSource),nameof(UserCaseSource.InvalidPasswords))]
    public async Task RegisterUser_PasswordIsInvalid(string password)
    {
        var model = new RegisterUserModel(){
            Login = "t@tt",
            Password = password, 
            Role = Entities.Models.Role.Admin            
        };

        Assert.ThrowsAsync<LogicException>(async ()=>
        {
            var result = await authService.RegisterUser(model); 
        });   
    }
}