using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;


namespace RestfulApi.Entities
{
    public class Context
    {
        private static readonly Lazy<List<Author>> _authorList = new Lazy<List<Author>>(() => JsonConvert.DeserializeObject<List<Author>>(File.ReadAllText(@".\TestData\Authors.json")));

        public static List<Author> AuthorList => _authorList.Value;


        
        private static readonly Lazy<List<Book>> _bookList = new Lazy<List<Book>>(() => JsonConvert.DeserializeObject<List<Book>>(File.ReadAllText(@".\TestData\Books.json")));

        public static List<Book> BookList => _bookList.Value;
    }
}