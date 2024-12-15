using Microsoft.EntityFrameworkCore;
using WebAutomation.Context;
using WebAutomation.Models;

namespace WebAutomation.Repositories
{
    public class BookRepository(ApplicationDbContext context) : IBookRepository
    {
        private readonly ApplicationDbContext _context = context;

        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public Book GetBookById(int id)
        {
            return _context.Books.FirstOrDefault(b => b.Id == id);
        }

        public void Add(Book book)
        {
            _context.Books.Add(book);
        }

        public void Update(Book book)
        {
            _context.Books.Update(book);
        }

        public void Remove(Book book)
        {
            _context.Books.Remove(book);
        }
    }
}
