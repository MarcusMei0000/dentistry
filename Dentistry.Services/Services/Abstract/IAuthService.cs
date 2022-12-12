using Dentistry.Services.Models;
using IdentityModel.Client;

namespace Dentistry.Services.Abstract;

public interface IAuthService
{
    Task<UserModel> RegisterUser(RegisterUserModel model);
    Task<TokenResponse> LoginUser(LoginUserModel model);
}


