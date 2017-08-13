using System;
using System.Linq;
using System.Collections.Generic;


namespace RestfulApi.Models
{
    public class AuthorForAddDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<BookForAddDto> Books { get; set; } = new List<BookForAddDto>();
    }
}
