using ICDE.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ICDE.Lib.Identity;
internal class SignInManager : ISignInManager
{
    private readonly SignInManager<User> _signInManager;

    public SignInManager(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<SignInResult> PasswordSignInAsync(User user, string password, bool isPersistent, bool lockoutOnFailure)
    {
        return await _signInManager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}
