using ICDE.Data.Entities.Identity;
using ICDE.Data.Repositories.Interfaces;
using ICDE.Lib.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ICDE.Lib.Services;
public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly SignInManager<User> _signInManager;

    public AuthenticationService(SignInManager<User> signInManager, IUserRepository userRepository)
    {
        _signInManager = signInManager;
        _userRepository = userRepository;
    }

    public async Task<bool> Login(string username, string password)
    {
        var dbUser = await _userRepository.GetByName(username);
        if (dbUser is null)
            return false;

        var result = await _signInManager.PasswordSignInAsync(dbUser, password, false, false);
        return result.Succeeded;
    }

    public async Task<bool> Register(string username, string password, string rol)
    {
        var userCreated = await _userRepository.CreateUser(username, password);
        if (userCreated is null)
            return false;

        var dbUser = await _userRepository.GetByName(userCreated.UserName);
        if (dbUser is null)
            return false;

        await _userRepository.AddUserClaim(dbUser, "Id", dbUser.Id.ToString());
        await _userRepository.AddUserClaim(dbUser, "Role", rol);

        return true;
    }
}
