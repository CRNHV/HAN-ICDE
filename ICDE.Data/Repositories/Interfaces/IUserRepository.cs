using ICDE.Data.Entities.Identity;

namespace ICDE.Data.Repositories.Interfaces;
public interface IUserRepository
{
    Task<User?> CreateUser(string username, string password);
    Task<User?> GetByName(string username);
    Task<bool> AddUserRole(User user, string role);
    Task<bool> AddUserClaim(User user, string claimType, string claimValue);
}
