using System.Security.Claims;
using ICDE.Data.Entities.Identity;
using ICDE.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ICDE.Data.Repositories;
public class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;

    public UserRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User?> CreateUser(string username, string password)
    {
        var user = new User()
        {
            UserName = username,
        };
        var result = await _userManager.CreateAsync(user, password);
        return result.Succeeded ? user : null;
    }

    public async Task<User?> GetByName(string username)
    {
        return await _userManager.FindByNameAsync(username);
    }

    public async Task<bool> AddUserRole(User user, string role)
    {
        var result = await _userManager.AddToRoleAsync(user, role);
        return result.Succeeded;
    }

    public async Task<bool> AddUserClaim(User user, string claimType, string claimValue)
    {
        var result = await _userManager.AddClaimAsync(user, new Claim(claimType, claimValue));
        return result.Succeeded;
    }
}
