namespace ICDE.Lib.Services.Interfaces;
public interface IAuthenticationService
{
    Task<bool> Login(string username, string password);
    Task<bool> Register(string username, string password, string rol);
}
