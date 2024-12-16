using WebAutomation.Context;

namespace WebAutomation.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IUserRepository Users { get; }
        public IBookRepository Books { get; }

        public UnitOfWork(ApplicationDbContext context, IUserRepository userRepository, IBookRepository bookRepository)
        {
            _context = context;
            Users = userRepository;
            Books = bookRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}