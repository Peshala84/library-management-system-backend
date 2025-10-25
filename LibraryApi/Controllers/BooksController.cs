using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryApi.Data;
using LibraryApi.Models;
using LibraryApi.DTOs;

namespace LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ILogger<BooksController> _logger;

        public BooksController(AppDbContext db, ILogger<BooksController> logger)
        {
            _db = db;
            _logger = logger;
        }

        // GET: api/books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _db.Books.AsNoTracking().ToListAsync();
        }

        // GET: api/books/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _db.Books.FindAsync(id);
            if (book == null) return NotFound();
            return book;
        }

        // POST: api/books
        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook([FromBody] BookDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                Author = dto.Author,
                ISBN = dto.ISBN,
                PublishedYear = dto.PublishedYear,
                CopiesAvailable = dto.CopiesAvailable
            };

            _db.Books.Add(book);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        // PUT: api/books/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDto dto)
        {
            var book = await _db.Books.FindAsync(id);
            if (book == null) return NotFound();

            book.Title = dto.Title;
            book.Author = dto.Author;
            book.ISBN = dto.ISBN;
            book.PublishedYear = dto.PublishedYear;
            book.CopiesAvailable = dto.CopiesAvailable;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/books/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _db.Books.FindAsync(id);
            if (book == null) return NotFound();

            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
