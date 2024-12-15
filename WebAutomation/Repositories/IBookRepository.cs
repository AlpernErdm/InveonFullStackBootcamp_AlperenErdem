using WebAutomation.Models;

namespace WebAutomation.Repositories
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(int id);
        void Add(Book book);
        void Update(Book book);
        void Remove(Book book);
    }
}
