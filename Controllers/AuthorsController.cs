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
    [Route("api/[controller]")]
    public class AuthorsController : Controller
    {
        // GET api/values
        [HttpGet]
        public IActionResult GetAuthors()
        // public IEnumerable<Author> Get()
        {
            var authors = Mapper.Map<IEnumerable<AuthorDto>>(Respository.GetAuthorList());
            return Ok(authors);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetAuthor")]
        // public string Get(int id)
        // {
        //     return "value";
        // }
        public IActionResult GetAuthor(int id)
        {
            var author = Respository.GetAuthor(id);
            if (author == null)
                return NotFound();  // 404

            var authorDto = Mapper.Map<AuthorDto>(author);
            return Ok(authorDto);
        }

        // POST api/values
        [HttpPost]
        public IActionResult AddAuthor([FromBody]AuthorForAddDto authorForAdd)
        {
            if (authorForAdd == null)
                return BadRequest();  // Did not deserialize properly.

            var authorEntity = Mapper.Map<Author>(authorForAdd);
            Respository.AddAuthor(authorEntity);

            if (!Respository.Save())
                throw new Exception("Failed to create the author.");  // Throw exception so the middleware handler does all of the error handling.
        
            var authorToReturn = Mapper.Map<AuthorDto>(authorEntity);
            return CreatedAtRoute("GetAuthor", new { id = authorToReturn.Id }, authorToReturn);   // Puts Location http://localhost:5000/api/Authors/53 in header
        }

        /*
        {
            "firstName" : "James",
            "lastName" : "Ellroy",
            "gender": "Male",
            "eMail": "je@je.com",
            "birthDate" : "1948-03-04T00:00:00"
        } 
        */

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
