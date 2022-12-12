using System;
using Dentistry.Entities;
using Dentistry.Entities.Models;
using Dentistry.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dentistry.WebAPI.AppConfiguration;

public static class RepositoryInitializer
{
    public const string MASTER_ADMIN_EMAIL = "master@mail.ru";
    public const string MASTER_ADMIN_PASSWORD = "password";

    private static async Task CreateGlobalAdmin(IAuthService authService, Context ? context)
    {        
        await authService.RegisterUser(new Services.Models.RegisterUserModel()
        {
            Login = MASTER_ADMIN_EMAIL,
            Password = MASTER_ADMIN_PASSWORD,
            Role = Entities.Models.Role.Admin
            //RoleId = context.Roles.Where(x => x.RoleName == "admin").First().Id
        });
    }

    public static async Task InitializeRepository(IServiceProvider provider)
    {
        using (var scope = provider.GetService<IServiceScopeFactory>().CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<Context>();            
            context.Database.Migrate();
            
            var userManager = (UserManager<User>)scope.ServiceProvider.GetRequiredService(typeof(UserManager<User>));
            var user = await userManager.FindByEmailAsync(MASTER_ADMIN_EMAIL);
            if (user == null)
            {
                var authService = (IAuthService)scope.ServiceProvider.GetRequiredService(typeof(IAuthService));
                await CreateGlobalAdmin(authService, context);
            }
        }
    }
}