using BookApi.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookApi.Controllers {
    [Route("book/[controller]")]
    [ApiController]
    public class NewSortController:ControllerBase {
        private ApiDbContext _DbContext;
        public NewSortController(ApiDbContext dbCont) {
            _DbContext = dbCont;
        }

        [HttpGet("{sortInt}")]
        public IActionResult Get(int sortInt) {
            //var currentBooks = _DbContext.Book ;
            IEnumerable<Models.Book> returnBookList = null;
            switch(sortInt) {
                case 1:
                    // Sort 1 - by Publisher, Author (last, first), then title
                    returnBookList = _DbContext.Set<Models.Book>()
                        .OrderBy(s => s.Publisher)
                        .ThenBy(s => s.AuthorLastName)
                        .ThenBy(s => s.AuthorFirstName)
                        .ThenBy(s => s.Title);
                    break;
                case 2:
                    // Sort 2- by Author (last, first) then title
                    returnBookList = _DbContext.Set<Models.Book>()
                        .OrderBy(s => s.AuthorLastName)
                        .ThenBy(s => s.AuthorFirstName)
                        .ThenBy(s => s.Title);
                    break;
                case 3:
                    // Sort 3 - Stored procedure by Publisher, Author (last, first), then title
                    var pubSorter = new DB.SortByPubStoredProcedure(_DbContext.Database.GetDbConnection().ConnectionString);
                    returnBookList = pubSorter.DoSort();
                    break;
                case 4:
                    // Sort 4 - Stored Procedure by Author (last, first) then title
                    var authSorter = new DB.SortByAuthStoredProcedure(_DbContext.Database.GetDbConnection().ConnectionString);
                    returnBookList = authSorter.DoSort();
                    break;
                default:
                    //sort id specified is not implemented
                    return StatusCode(StatusCodes.Status501NotImplemented);
            }

            return StatusCode(StatusCodes.Status200OK, returnBookList);
        }
    }
}
