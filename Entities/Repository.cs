using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;


namespace RestfulApi.Entities
{
    public class Respository
    {
        private static readonly Lazy<List<Author>> _authorList = new Lazy<List<Author>>(() => JsonConvert.DeserializeObject<List<Author>>(File.ReadAllText(@".\TestData\Authors.json")));

        private static List<Author> AuthorList => _authorList.Value;



        private static readonly Lazy<List<Book>> _bookList = new Lazy<List<Book>>(() => JsonConvert.DeserializeObject<List<Book>>(File.ReadAllText(@".\TestData\Books.json")));

        private static List<Book> BookList => _bookList.Value;


        public static List<Author> GetAuthorList()
        {
            return AuthorList;
        }


        public static Author GetAuthor(int id)
        {
            return AuthorList.FirstOrDefault(a => a.Id == id);
        }


        public static IEnumerable<Author> GetAuthors(IEnumerable<int> ids)
        {
            return AuthorList.Where(a => ids.Contains(a.Id));
        }
        

        public static bool AuthorExists(int id)
        {
            return AuthorList.Exists(a => a.Id == id);
        }


        public static Book GetBook(int authorId, int id)
        {
           // return BookList.FirstOrDefault(b => b.Id == id && b.AuthorId == authorId);
            return BookList.FirstOrDefault(b => b.AuthorId == authorId && b.Id == id);
        }


        public static List<Book> GetBookList(int authorId)
        {
            return BookList.Where(b => b.AuthorId == authorId).ToList<Book>();
        }


        public static void AddAuthor(Author author)
        {
            author.Id = AuthorList.Max(a => a.Id) + 1;
            AuthorList.Add(author);

            foreach (var book in author.Books)
                AddBookForAuthor(author.Id, book);
        }

    
        public static void AddBookForAuthor(int authorId, Book book)
        {
            book.Id = BookList.Max(b => b.Id) + 1;
            book.AuthorId = authorId;
            BookList.Add(book);
        }


        public static bool Save()
        {
            // Emulate EF repository save which can return a false if it fails.
            return true;
        }
    }
}