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

        public async Task<ServiceResult<List<Book>>> GetAllBooksAsync()
        {
            var cache = _redis.GetDatabase();
            var cachedBooks = await cache.StringGetAsync("books");

            List<Book> books;
            if (!string.IsNullOrEmpty(cachedBooks))
            {
                books = JsonConvert.DeserializeObject<List<Book>>(cachedBooks)!;
            }
            else
            {
                books = await _context.Books.ToListAsync();
                if (books == null)
                {
                    return ServiceResult<List<Book>>.Fail("Books not found");
                }

                var serializedBooks = JsonConvert.SerializeObject(books);
                await cache.StringSetAsync("books", serializedBooks);
            }

            return ServiceResult<List<Book>>.Success(books);
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
        public async Task<ServiceResult<Book>> UpdateBookAsync(int id, UpdateBookDto updatedBookDto)
        {
            var existingBook = await _context.Books.FindAsync(id);
            if (existingBook == null)
            {
                return ServiceResult<Book>.Fail("Book not found");
            }

            existingBook.Title = updatedBookDto.Title;
            existingBook.Author = updatedBookDto.Author;
            existingBook.Year = updatedBookDto.Year;

            _context.Books.Update(existingBook);
            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                return ServiceResult<Book>.Fail("Failed to update the book");
            }

            return ServiceResult<Book>.Success(existingBook);
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
