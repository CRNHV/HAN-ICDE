using System.Threading.Tasks;
using ICDE.Lib.Services.Interfaces;
using ICDE.Web.Models.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ICDE.Web.Controllers;

[Route("Auth")]
public class AuthenticationController : Controller
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
        if (HttpContext.Request.Method == "POST")
        {
            var result = await _authService.Register(request.Username, request.Password, request.Role);
            return result ? View() : Unauthorized();
        }

        return View();
    }

    [HttpGet("login")]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] LoginViewModel request)
    {
        if (HttpContext.Request.Method == "POST")
        {
            var result = await _authService.Login(request.Username, request.Password);
            return result ? View() : Unauthorized();
        }

        return View();
    }
}
