using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAutomation.Models;
using WebAutomation.Models.Dtos;
using WebAutomation.Repositories;

namespace WebAutomation.Controllers
{
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var books = _unitOfWork.Books.GetAllBooks();
            return View(books);
        }

        public IActionResult Details(int id)
        {
            var book = _unitOfWork.Books.GetBookById(id);
            if (book == null)
                return NotFound();

            return View(book);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookDto dto)
        {
            if (ModelState.IsValid)
            {
                var book = new Book
                {
                    Title = dto.Title,
                    Author = dto.Author,
                    PublicationYear = dto.PublicationYear,
                    ISBN = dto.ISBN,
                    Genre = dto.Genre,
                    Publisher = dto.Publisher,
                    PageCount = dto.PageCount,
                    Language = dto.Language,
                    Summary = dto.Summary,
                    AvailableCopies = dto.AvailableCopies
                };

                _unitOfWork.Books.Add(book);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }

        public IActionResult Update(int id)
        {
            var book = _unitOfWork.Books.GetBookById(id);
            if (book == null)
                return NotFound();

            var dto = new UpdateBookDto
            (
                book.Id,
                book.Title,
                book.Author,
                book.PublicationYear,
                book.ISBN,
                book.Genre,
                book.Publisher,
                book.PageCount,
                book.Language,
                book.Summary,
                book.AvailableCopies
            );

            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateBookDto dto)
        {
            if (ModelState.IsValid)
            {
                var book = _unitOfWork.Books.GetBookById(dto.Id);
                if (book == null)
                    return NotFound();

                book.Title = dto.Title;
                book.Author = dto.Author;
                book.PublicationYear = dto.PublicationYear;
                book.ISBN = dto.ISBN;
                book.Genre = dto.Genre;
                book.Publisher = dto.Publisher;
                book.PageCount = dto.PageCount;
                book.Language = dto.Language;
                book.Summary = dto.Summary;
                book.AvailableCopies = dto.AvailableCopies;

                _unitOfWork.Books.Update(book);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }

        public IActionResult Delete(int id)
        {
            var book = _unitOfWork.Books.GetBookById(id);
            if (book == null)
                return NotFound();

            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = _unitOfWork.Books.GetBookById(id);
            if (book == null)
                return NotFound();

            _unitOfWork.Books.Remove(book);
            await _unitOfWork.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}