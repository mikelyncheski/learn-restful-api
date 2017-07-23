using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace RestfulApi.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Genre { get; set; }
    }
}