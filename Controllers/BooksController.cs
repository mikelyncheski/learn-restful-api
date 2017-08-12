using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestfulApi.Entities;
using RestfulApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;


namespace RestfulApi.Controllers
{
    [Route("api/authors/{authorId}/books")]
    public class BooksController : Controller
    {
        // GET api/values
        [HttpGet()]
        public IActionResult GetBooksForAuthor(int authorId)
        {
            if (!Respository.AuthorExists(authorId))
                return NotFound();

            var booksFromRepo = Respository.GetBookList(authorId);

            var booksForAuthor = Mapper.Map<IEnumerable<BookDto>>(booksFromRepo);

            return Ok(booksForAuthor);
        }


        [HttpGet("{id}", Name = "GetBookForAuthor")]
        public IActionResult GetBookForAuthor(int authorId, int id)
        {
            if (!Respository.AuthorExists(authorId))
                return NotFound();

            var book = Respository.GetBook(authorId, id);
            if (book == null)
                return NotFound();  // 404

            var bookDto = Mapper.Map<BookDto>(book);
            return Ok(bookDto);
        }


        [HttpPost()]
        public IActionResult CreateBookForAuthor(int authorId, [FromBody] BookForAddDto bookForAdd)  // FromBody signals that it should be deserialized from the request body
        {
            if (bookForAdd == null)
                return BadRequest();

            if (!Respository.AuthorExists(authorId))
                return NotFound();
            
            var bookEntity = Mapper.Map<Book>(bookForAdd);
            Respository.AddBookForAuthor(authorId, bookEntity);

            if (!Respository.Save())
                throw new Exception("Failed to create the book.");  // Throw exception so the middleware handler does all of the error handling.
        
            var bookToReturn = Mapper.Map<BookDto>(bookEntity);
            return CreatedAtRoute("GetBookForAuthor",  new {authorId, id = bookToReturn.Id }, bookToReturn);   // Puts Location http://localhost:5000/api/Authors/53 in header

        }
    }
}
