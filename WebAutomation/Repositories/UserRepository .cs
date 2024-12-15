using WebAutomation.Context;
using WebAutomation.Models;

namespace WebAutomation.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(AppUser user)
        {
            _context.Users.Add(user);
        }

        public void Remove(AppUser user)
        {
            _context.Users.Remove(user);
        }

        public async Task<AppUser> FindByIdAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
