using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace RestfulApi.Entities
{
    //https://www.mockaroo.com/480b5dc0
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }

        
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}