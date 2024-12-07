using LibraryManagement.ExceptionHandling;
using LibraryManagement.Models;

namespace LibraryManagement.Business
{
    public interface IBookService
    {
        Task<ServiceResult<List<Book>>> GetAllBooksAsync();
        Task<ServiceResult<Book>> GetBookByIdAsync(int id);
        Task<ServiceResult<Book>> AddBookAsync(Book newBook);
        Task<ServiceResult<Book>> UpdateBookAsync(int id, UpdateBookDto updatedBookDto);
        Task<ServiceResult<bool>> DeleteBookAsync(int id);
        Task<ServiceResult<object>> GetPagedBooksAsync(int pageNumber, int pageSize);

    }
}
