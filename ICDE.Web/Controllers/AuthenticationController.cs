using System.Threading.Tasks;
using ICDE.Lib.Services.Interfaces;
using ICDE.Web.Models.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers;

[Route("Auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authService;

    public AuthenticationController(IAuthenticationService authService)
    {
        _authService = authService;
    }

    [HttpGet("register")]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegisterViewModel request)
    {
        if (IsRequestMethod("POST"))
        {
            var result = await _authService.Register(request.Username, request.Password, request.Role);
            return result ? Redirect("/auth/login") : View(new RegisterViewModel()
            {
                Message = "Unable to register. Try again."
            });
        }

        return View(new RegisterViewModel());
    }

    [HttpGet("login")]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] LoginViewModel request)
    {
        if (IsRequestMethod("POST"))
        {
            var result = await _authService.Login(request.Username, request.Password);
            return result ? Redirect("/") : View(new LoginViewModel()
            {
                Message = "Unable to login. Try again."
            });
        }

        return View(new LoginViewModel());
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutUser();
        return Redirect("/auth/login");
    }
}
