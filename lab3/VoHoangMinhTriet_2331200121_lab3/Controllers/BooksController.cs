using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VoHoangMinhTriet_2331200121_lab3.Model;

namespace VoHoangMinhTriet_2331200121_lab3.Controllers;
[ApiController]
[Route("api/[controller]")]


public class BooksController : ControllerBase
{
    List<Book> books = new List<Book>
    {
        new Book(1, "this sucks", "trie", 2015, "horror"),
        new Book(2, "this still sucks", "trie2", 2016, "comedy"),
        new Book(3, "this sucks so much", "trie3", 2017, "horror"),
    };

    [HttpGet]
    public ActionResult<IEnumerable<Book>> GetAllBooks()
    {
        return Ok(books);
    }

    [HttpGet("{id}")]
    public ActionResult<Book> GetBookById(int id)
    {
        Book b = books.FirstOrDefault(u => u.Id == id);
        if (b == null)
        {
            return NotFound();
        }
        return Ok(b);
    }

    [HttpPost]
    public ActionResult<Book> AddBook([FromBody]Book newBook)
    {
        if (books.FirstOrDefault(u => u.Id == newBook.Id) != null) return BadRequest();
        books.Add(newBook);
        return Ok(newBook);
    }

    [HttpPut("{id}")]
    public ActionResult<Book> UpdateBook(int id, [FromBody]Book updatedBook)
    {
        int index = books.FindIndex(u => u.Id == updatedBook.Id);
        if (index == -1) return NotFound();
        books[index] = updatedBook;
        return updatedBook;
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteBook(int id)
    {
        int index = books.FindIndex(u => u.Id == id);
        if (index == -1) return NotFound();
        books.Remove(books[index]);
        return NoContent();
    }
}