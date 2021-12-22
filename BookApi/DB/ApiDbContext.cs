using BookApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookApi.DB {
    public class ApiDbContext:DbContext {
        public ApiDbContext(DbContextOptions<ApiDbContext> option) : base(option) {

        }

        public DbSet<Book> Book { get; set; }
    }
}
