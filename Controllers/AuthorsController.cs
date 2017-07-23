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
            var authors = Mapper.Map<IEnumerable<AuthorDto>>(Context.AuthorList);
            return Ok(authors);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        // public string Get(int id)
        // {
        //     return "value";
        // }
        public IActionResult GetAuthor(int id)
        {
            var author = Context.AuthorList.FirstOrDefault(a => a.Id == id);
            if (author == null)
                return NotFound();  // 404

            var authorDto = Mapper.Map<AuthorDto>(author);
            return Ok(authorDto);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

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
