using WebAutomation.Models;

namespace WebAutomation.Repositories
{
    public interface IUserRepository
    {
        void Add(AppUser user);
        void Remove(AppUser user);
        Task<AppUser> FindByIdAsync(string id);
    }
}