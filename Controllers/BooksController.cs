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
            if (!Context.AuthorList.Exists(a => a.Id == authorId))
                return NotFound();

            var booksFromRepo = Context.BookList.Where(b => b.AuthorId == authorId);

            var booksForAuthor = Mapper.Map<IEnumerable<BookDto>>(booksFromRepo);

            return Ok(booksForAuthor);
        }


        [HttpGet("{id}")]
        public IActionResult GetBook(int authorId, int id)
        {
            if (!Context.AuthorList.Exists(a => a.Id == authorId))
                return NotFound();

            var book = Context.BookList.FirstOrDefault(b => b.Id == id && b.AuthorId == authorId);
            if (book == null)
                return NotFound();  // 404

            var bookDto = Mapper.Map<BookDto>(book);
            return Ok(bookDto);
        }
    }
}
