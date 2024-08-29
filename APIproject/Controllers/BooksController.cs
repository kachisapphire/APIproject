using APIproject.Data;
using APIproject.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;

namespace APIproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly MyBookShopContext _context;

        public BooksController(MyBookShopContext context)
        {
            _context = context;
        }

        [HttpGet("get-books")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }
        [HttpPost("add-book")]
        public async Task<ActionResult<Book>> AddBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }
        [HttpGet("Get-book/{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }
        [HttpPut("edit-book/{id}")]
        public async Task<IActionResult> EditBook(int id, [FromBody]Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Books.Any(b => b.Id != id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }
        [HttpDelete("delete-book/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
