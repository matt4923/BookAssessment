using System;
using System.Collections.Generic;

namespace BookApi.DB {
    public class InitializeWithEntity {
        internal static void AddTestData(ApiDbContext dbContext) {
            var sampleData = TestData.GetSampleBooks();
            var currentBooks = GetExistingBookDictionary(dbContext);
            foreach(var item in sampleData) {
                if(!currentBooks.TryGetValue(item.Title.Trim().ToUpper(), out var b)) {
                    dbContext.Book.Add(item);
                    dbContext.SaveChanges();
                }
            }
        }

        private static Dictionary<string, Models.Book> GetExistingBookDictionary(ApiDbContext dbContext) {
            var existingBooks = dbContext.Book;
            var booksDictionary = new Dictionary<string, Models.Book>();
            foreach(var book in existingBooks) { 
                booksDictionary[book.Title.Trim().ToUpper()] = book;
            }
            return booksDictionary;
        }
    }
}
