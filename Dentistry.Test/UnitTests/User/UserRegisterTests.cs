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
            FirstName = "Test 1",
            LastName = "Test 2",
            Patronimyc = "Test 3",
            Password = "Test 4",
            Email = "test@test",
            Role = Entities.Models.Role.Admin            
        };

        var resultModel = await authService.RegisterUser(model);
        Assert.AreEqual(model.Email, resultModel.Email);
        Assert.AreEqual(model.FirstName, resultModel.FirstName);
        Assert.AreEqual(model.LastName, resultModel.LastName);
        Assert.AreEqual(model.Patronimyc, resultModel.Patronymic);
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
            FirstName = "Test 1",
            LastName = "Test 2",
            Patronimyc = "Test 3",
            Password = "Test 4",
            Email = "test@test",
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
            FirstName = "Test 1",
            LastName = "Test 2",
            Patronimyc = "Test 3",
            Password = password,
            Email = "test@test",
            Role = Entities.Models.Role.Admin            
        };

        Assert.ThrowsAsync<LogicException>(async ()=>
        {
            var result = await authService.RegisterUser(model); 
        });   
    }
}