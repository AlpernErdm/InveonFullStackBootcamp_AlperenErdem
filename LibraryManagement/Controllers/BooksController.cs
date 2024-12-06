using LibraryManagement.Business;
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
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("/getAll")]
        public async Task<ActionResult<ServiceResult<IEnumerable<Book>>>> GetAllBooks()
        {
            var result = await _bookService.GetAllBooksAsync();
            if (result.Status == ServiceResultStatus.Error && result.ProblemDetails != null)
            {
                return StatusCode(result.ProblemDetails.Status, result.ProblemDetails);
            }
            return Ok(result);
        }

        [HttpGet("/getById/{id}")]
        public async Task<ActionResult<ServiceResult<Book>>> GetBookById(int id)
        {
            var result = await _bookService.GetBookByIdAsync(id);
            if (result.Status == ServiceResultStatus.Error && result.ProblemDetails != null)
            {
                return StatusCode(result.ProblemDetails.Status, result.ProblemDetails);
            }
            return Ok(result);
        }

        [HttpPost("/create")]
        public async Task<ActionResult<ServiceResult<Book>>> AddBook([FromBody] Book newBook)
        {
            var result = await _bookService.AddBookAsync(newBook);
            if (result.Status == ServiceResultStatus.Error && result.ProblemDetails != null)
            {
                return StatusCode(result.ProblemDetails.Status, result.ProblemDetails);
            }
            return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, result);
        }

        [HttpPut("/update/{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var result = await _bookService.UpdateBookAsync(id, updatedBook);
            if (result.Status == ServiceResultStatus.Error && result.ProblemDetails != null)
            {
                return StatusCode(result.ProblemDetails.Status, result.ProblemDetails);
            }
            return NoContent();
        }

        [HttpDelete("/delete/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (result.Status == ServiceResultStatus.Error && result.ProblemDetails != null)
            {
                return StatusCode(result.ProblemDetails.Status, result.ProblemDetails);
            }
            return NoContent();
        }

        [HttpGet("paged")]
        public async Task<ActionResult<ServiceResult<object>>> GetPagedBooks([FromHeader] int pageNumber, [FromHeader] int pageSize)
        {
            var result = await _bookService.GetPagedBooksAsync(pageNumber, pageSize);
            if (result.Status == ServiceResultStatus.Error && result.ProblemDetails != null)
            {
                return StatusCode(result.ProblemDetails.Status, result.ProblemDetails);
            }
            return Ok(result);
        }
    }
}
