using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace RestfulApi.Models
{
    public class BookDto
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Genre { get; set; }
    }
}