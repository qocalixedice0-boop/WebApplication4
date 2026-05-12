using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private static List<Book> books = new List<Book>();

        [HttpGet]
        public IActionResult GetBooks()
        {
            return Ok(books);
        }
        [HttpPost]
        public IActionResult Create(Book book)
        {
            book.Id = Guid.NewGuid();
            books.Add(book);
            return Ok(book);
        }
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Book updateBook)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            book.Title = updateBook.Title;
            book.Author = updateBook.Author;
            return Ok(book);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            books.Remove(book);
            return NoContent();
        }
    }
}
