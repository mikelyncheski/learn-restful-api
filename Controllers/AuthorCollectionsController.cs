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
    public class AuthorCollectionController : Controller
    {
        // POST api/values
        [HttpPost]
        public IActionResult AddAuthorCollection([FromBody] IEnumerable<AuthorForAddDto> authorCollection)
        {
            if (authorCollection == null)
                return BadRequest();  // Did not deserialize properly.

            var authorEntities = Mapper.Map<IEnumerable<Author>>(authorCollection);
            foreach (var author in authorEntities)
                Respository.AddAuthor(author);

            if (!Respository.Save())
                throw new Exception("Failed to create the author collection.");  // Throw exception so the middleware handler does all of the error handling.
        
          //  var authorToReturn = Mapper.Map<AuthorDto>(authorEntity);
        //    return CreatedAtRoute("GetAuthor", new { id = authorToReturn.Id }, authorToReturn);   // Puts Location http://localhost:5000/api/Authors/53 in header
            return Ok();
        }


        // (key1, key2, ...)
        // Custom Model Binder 
    }
}
