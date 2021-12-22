using BookApi.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookApi.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class BookController:ControllerBase {
        private readonly ApiDbContext _DbContext;
        public BookController(ApiDbContext dbCont) {
            _DbContext = dbCont;
        }

        [HttpGet]
        public string Get() {

            try {
                BookApi.DB.InitializeWithEntity.AddTestData(_DbContext);
                BookApi.DB.Initialization.AddStoredProcedures(_DbContext.Database.GetDbConnection().ConnectionString);
                return "DB Initialized!\nBook API Running!" +
                    "\n\nQuick Reference URL List:\n" +
                    ".../book/newsort/<sortInt> => (sorting by single endpoint) - Returns JSON string of books sorted in the order depending on the query string parameter <sortInt>, (1 = sort1, 2 = sort2, etc).\n" +
                    ".../book/sort1 => Returns JSON string of sorted books by Publisher, Author Last Name, Author First Name and then Title.\n" +
                    ".../book/sort2 => Returns JSON string of sorted books by Author Last Name, Author First Name and then Title.\n" +
                    ".../book/sort3 => Returns JSON string of sorted books by Publisher, Author Last Name, Author First Name and then Title, (Same results as sort1 but retrieved by Stored Procedure).\n" +
                    ".../book/sort4 => Returns JSON string of sorted books by Author Last Name, Author First Name and then Title,(Same results as sort2 but retrieved by Stored Procedure).\n" +
                    ".../book/total => Returns total price of all books in database.";
            } catch(Exception ex) {
                return $"Error on startup: {ex.ToString()}";
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Models.Book book) {
            _DbContext.Book.Add(book);
            _DbContext.SaveChanges();
            return StatusCode(StatusCodes.Status200OK, $"{book.Title} successfully added!");
        }

        [HttpPut("{title}")]
        public IActionResult Put(string title, Models.Book updatedBook) {
            var currentBook = _DbContext.Book.FirstOrDefault<Models.Book>(b => b.Title == title);
            if(currentBook == null) {
                return StatusCode(StatusCodes.Status500InternalServerError, $"'{title}' not found. Please indicate an existing title.");
            }

            currentBook.Title = updatedBook.Title;
            currentBook.Publisher = updatedBook.Publisher;
            currentBook.AuthorLastName = updatedBook.AuthorLastName;
            currentBook.AuthorFirstName = updatedBook.AuthorFirstName;
            currentBook.Price = updatedBook.Price;
            currentBook.PublicationYear = updatedBook.PublicationYear;
            currentBook.Url = updatedBook.Url;
            _DbContext.SaveChanges();

            return StatusCode(StatusCodes.Status200OK, $"{title} successfully updated!");
        }

        [HttpDelete("{title}")]
        public IActionResult Delete(string title) {
            var currentBook = _DbContext.Book.FirstOrDefault<Models.Book>(b => b.Title == title);
            if(currentBook == null) {
                return StatusCode(StatusCodes.Status200OK, $"{title} does not exist.");
            }
            _DbContext.Book.Remove(currentBook);
            _DbContext.SaveChanges();
            return StatusCode(StatusCodes.Status200OK, $"{title} successfully removed!");
        }
    }
}
