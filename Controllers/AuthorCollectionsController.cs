using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestfulApi.Entities;
using RestfulApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using HarrierGroup.Common;


namespace RestfulApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthorCollectionsController : Controller
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
        
            var authorCollectionToReturn = Mapper.Map<IEnumerable<AuthorDto>>(authorEntities);
            var idsAsString = string.Join( ",", authorEntities.Select(a => a.Id.ToString()));
            return CreatedAtRoute("GetAuthorCollectionX", new { ids = idsAsString }, authorCollectionToReturn);   // Puts Location http://localhost:5000/api/Authors/53 in header
        }


        // (key1, key2, ...)
        [HttpGet("({ids})", Name = "GetAuthorCollectionX")]
        public IActionResult GetAuthorCollection(string ids)
        // public IActionResult GetAuthorCollection(IEnumerable<int> ids)         // Custom Model Binder - Nah -- very complicated, see Demo - Working with Array Keys
        {
            var idList = StringHelper.StringToIntegerList(ids);
            var authorList = Respository.GetAuthors(idList);

            if (idList.Count() != authorList.Count())
                return NotFound();
 
            return Ok(Mapper.Map<IEnumerable<AuthorDto>>(authorList));
        } 
    }
}
