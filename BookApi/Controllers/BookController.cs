using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace BookApi.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class BookController:ControllerBase {
        private readonly IConfiguration _Configuration;
        public BookController(IConfiguration config) {
            _Configuration = config;
        }

        // GET: BookController
        [HttpGet]
        public string Get() {
            var connectionString = _Configuration.GetConnectionString("CascadeBooks");
            try {
                BookApi.DB.Initialization.InitializeDb(connectionString);

                return "DB Initialized!\nBook API Running!" +
                    "\n\nQuick Reference URL List:\n" +
                    ".../book/sort1 => Returns JSON string of sorted books by Publisher, Author Last Name, Author First Name and then Title.\n" +
                    ".../book/sort2 => Returns JSON string of sorted books by Author Last Name, Author First Name and then Title.\n" +
                    ".../book/sort3 => Returns JSON string of sorted books by Publisher, Author Last Name, Author First Name and then Title, (Same results as sort1 but retrieved by Stored Procedure).\n" +
                    ".../book/sort4 => Returns JSON string of sorted books by Author Last Name, Author First Name and then Title,(Same results as sort2 but retrieved by Stored Procedure).\n" +
                    ".../book/total => Returns total price of all books in database.";
            } catch(Exception ex) {
                return $"Error on startup: {ex.ToString()}";
            }
        }
    }
}
