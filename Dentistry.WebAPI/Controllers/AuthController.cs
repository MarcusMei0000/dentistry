using AutoMapper;
using Dentistry.Services.Abstract;
using Dentistry.Services.Models;
using Dentistry.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dentistry.WebAPI.Controllers.AuthController;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AuthController(IMapper mapper, IAuthService authService)
    {
        _mapper = mapper;
        _authService = authService;
    }

    [HttpPost("")]
    public async Task<RegisterUserModel> RegisterUser([FromBody] RegisterUserModel model)
    {
        var registreModel = _mapper.Map<RegisterUserModel >(model);
        return _mapper.Map<RegisterUserModel>(await _authService.RegisterUser(model));
    }

    [HttpPost("login")]
    public async Task<IdentityModel.Client.TokenResponse> LoginUser([FromBody] LoginUserModel model)
    {
        return await _authService.LoginUser(_mapper.Map<LoginUserModel>(model));
    }
}
    