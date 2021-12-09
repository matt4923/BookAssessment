using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace BookApi.Controllers {
    [ApiController]
    [Route("book/[controller]")]
    public class TotalController:ControllerBase {
        private readonly IConfiguration _Configuration;
        public TotalController(IConfiguration config) {
            _Configuration = config;
        }
        //Returns total price of all books
        [HttpGet]
        public string Get() {
            var connectionString = _Configuration.GetConnectionString("CascadeBooks");
            try {
                var total = BookApi.DB.Total.GetTotalPriceOfBooks(connectionString);
                return total.ToString();
            } catch(Exception ex) {
                return $"Error getting total price: {ex.ToString()}";
            }
        }
    }
}

