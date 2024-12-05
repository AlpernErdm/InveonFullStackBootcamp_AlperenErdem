using LibraryManagement.Context;
using LibraryManagement.ExceptionHandling;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace LibraryManagement.Business
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConnectionMultiplexer _redis;

        public BookService(ApplicationDbContext context, IConnectionMultiplexer redis)
        {
            _context = context;
            _redis = redis;
        }

        public async Task<ServiceResult<IEnumerable<Book>>> GetAllBooksAsync()
        {
            var cache = _redis.GetDatabase();
            var cachedBooks = await cache.StringGetAsync("books");

            IEnumerable<Book> books;
            if (!string.IsNullOrEmpty(cachedBooks))
            {
                books = JsonConvert.DeserializeObject<IEnumerable<Book>>(cachedBooks) ?? new List<Book>();
            }
            else
            {
                books = await _context.Books.ToListAsync();
                if (books == null)
                {
                    return ServiceResult<IEnumerable<Book>>.Fail("Books not found");
                }

                var serializedBooks = JsonConvert.SerializeObject(books);
                await cache.StringSetAsync("books", serializedBooks);
            }

            return ServiceResult<IEnumerable<Book>>.Success(books);
        }

        public async Task<ServiceResult<Book>> GetBookByIdAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return ServiceResult<Book>.Fail("Books not found");
            }
            return ServiceResult<Book>.Success(book);
        }

        public async Task<ServiceResult<Book>> AddBookAsync(Book newBook)
        {
            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();
            return ServiceResult<Book>.Success(newBook);
        }

        public async Task<ServiceResult<Book>> UpdateBookAsync(int id, Book updatedBook)
        {
            if (id != updatedBook.Id)
            {
                return ServiceResult<Book>.Fail("ID mismatch");
            }
            _context.Entry(updatedBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Books.Any(b => b.Id == id))
                {
                    return ServiceResult<Book>.Fail("Book not found");
                }
                else
                {
                    throw;
                }
            }

            return ServiceResult<Book>.Success(updatedBook);
        }

        public async Task<ServiceResult<bool>> DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return ServiceResult<bool>.Fail("Book not found");
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return ServiceResult<bool>.Success(true);
        }

        public async Task<ServiceResult<object>> GetPagedBooksAsync(int pageNumber, int pageSize)
        {
            var totalRecords = await _context.Books.CountAsync();
            var pagedBooks = await _context.Books
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var response = new
            {
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)System.Math.Ceiling((double)totalRecords / pageSize),
                Books = pagedBooks
            };

            return ServiceResult<object>.Success(response);
        }
    }
}
