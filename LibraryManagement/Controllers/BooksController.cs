using LibraryManagement.Context;
using LibraryManagement.ExceptionHandling;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConnectionMultiplexer _redis;

        public BooksController(ApplicationDbContext context, IConnectionMultiplexer redis)
        {
            _context = context;
            _redis = redis;
        }

        // GET: /getAll
        [HttpGet("/getAll")]
        public async Task<ActionResult<ServiceResult<IEnumerable<Book>>>> GetAllBooks()
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
                    return NotFound(ServiceResult<IEnumerable<Book>>.Fail("Books not found"));
                }

                var serializedBooks = JsonConvert.SerializeObject(books);
                await cache.StringSetAsync("books", serializedBooks);
            }

            return Ok(ServiceResult<IEnumerable<Book>>.Success(books));
        }


        // GET: getById/{id}
        [HttpGet("/getById/{id}")]
        public async Task<ActionResult<ServiceResult<Book>>> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound(ServiceResult<Book>.Fail("Books not found"));
            }
            return Ok(ServiceResult<Book>.Success(book));
        }

        // POST: /create
        [HttpPost("/create")]
        public async Task<ActionResult<ServiceResult<Book>>> AddBook([FromBody] Book newBook)
        {
            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, ServiceResult<Book>.Success(newBook));
        }

        // PUT: update/{id}
        [HttpPut("/update/{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book updatedBook)
        {
            if (id != updatedBook.Id)
            {
                return BadRequest(ServiceResult<Book>.Fail("ID uyuşmuyor"));
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
                    return NotFound(ServiceResult<Book>.Fail("Books not found"));
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: /delete/{id}
        [HttpDelete("/delete/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound(ServiceResult<Book>.Fail("Books not found"));
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: /paged
        [HttpGet("paged")]
        public async Task<ActionResult<ServiceResult<object>>> GetPagedBooks([FromHeader] int pageNumber, [FromHeader] int pageSize)
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

            return Ok(ServiceResult<object>.Success(response));
        }
    }
}
