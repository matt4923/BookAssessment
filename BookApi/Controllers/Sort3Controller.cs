using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
/// <summary>
/// Sort 3 - Stored procedure by Publisher, Author (last, first), then title
/// Note: The different sort methods could have been reduced to a single method with the client passing in a parameter to denote which sort operation it needs. 
/// The instructions were to create separate api methods, (my interpretation is separate api endpoints).  Since multiple http Get requests with the same parameters can't be in the same controller, I broke these out into separate controllers.
/// </summary>
namespace BookApi.Controllers {
    [ApiController]
    [Route("book/[controller]")]
    public class Sort3Controller:Controller {
        private readonly IConfiguration _Configuration;
        public Sort3Controller(IConfiguration config) {
            _Configuration = config;
        }

        [HttpGet]
        public List<Models.Book> Get() {
            var connectionString = _Configuration.GetConnectionString("CascadeBooks");
            var sorter = new DB.SortByPubStoredProcedure(connectionString);
            var sortedByPub = sorter.DoSort();
            return sortedByPub;
        }
    }
}
