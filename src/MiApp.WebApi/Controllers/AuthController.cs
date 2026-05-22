using Microsoft.AspNetCore.Mvc;
using MiApp.Application.Interfaces;
using MiApp.Application.Contracts.Persistence;
using MiApp.Domain.Exceptions;

namespace MiApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepo;
    private readonly ITokenService _tokenService;

    public AuthController(IUserRepository userRepo, ITokenService tokenService)
    {
        _userRepo = userRepo;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _userRepo.GetByNombreUsuarioAsync(request.User);

        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new DomainRuleException("Credenciales incorrectas");

        var token = _tokenService.GenerateToken(user);
        return Ok(new { token });
    }
}

public record LoginRequest(string User, string Password);
