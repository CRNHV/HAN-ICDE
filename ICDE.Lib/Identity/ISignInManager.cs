using ICDE.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ICDE.Lib.Identity;
public interface ISignInManager
{
    Task<SignInResult> PasswordSignInAsync(User user, string password, bool isPersistent, bool lockoutOnFailure);
    Task SignOutAsync();
}
