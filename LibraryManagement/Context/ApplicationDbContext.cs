using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Context
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        public required DbSet<Book> Books { get; set; }
    }
}
