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


        [HttpGet("{id}")]
        public IActionResult GetBook(int authorId, int id)
        {
            if (!Respository.AuthorExists(authorId))
                return NotFound();

            var book = Respository.GetBook(authorId, id);
            if (book == null)
                return NotFound();  // 404

            var bookDto = Mapper.Map<BookDto>(book);
            return Ok(bookDto);
        }
    }
}
