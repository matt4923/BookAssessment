using System;
using System.Collections.Generic;

namespace BookApi.DB {
    public static class TestData {
        //sample data modified from: https://gist.github.com/nanotaboada/6396437
        public static List<Models.Book> GetSampleBooks() {
            var returnList = new List<Models.Book>(){
                new Models.Book {ID= Guid.NewGuid(), Title = "Eloquent JavaScript, Third Edition", AuthorLastName = "Haverbeke", AuthorFirstName = "Marijn", Price = 47.99m, Publisher = "No Starch Press", PublicationYear = 2018, Url = "http://eloquentjavascript.net/" },
                new Models.Book {ID= Guid.NewGuid(), Title = "Practical Modern JavaScript", AuthorLastName = "Bevacqua", AuthorFirstName = "Nicolás", Price = 33.99m, Publisher = "O'Reilly Media", PublicationYear = 2017, Url = "https://github.com/mjavascript/practical-modern-javascript" },
                new Models.Book {ID= Guid.NewGuid(), Title= "Understanding ECMAScript 6", AuthorLastName = "Zakas", AuthorFirstName = "Nicholas C.", Price = 35.99m, Publisher = "No Starch Press", PublicationYear = 2016, Url = "https://leanpub.com/understandinges6/read"},
                new Models.Book {ID= Guid.NewGuid(), Title = "Speaking JavaScript", AuthorLastName = "Osmani", AuthorFirstName = "Addy", Price = 25.99m, Publisher = "O'Reilly Media", PublicationYear = 2012, Url = "http://www.addyosmani.com/resources/essentialjsdesignpatterns/book/" },
                new Models.Book {ID= Guid.NewGuid(), Title = "Rethinking Productivity in Software Engineering", AuthorLastName = "Sadowski", AuthorFirstName = "Caitlin", Price = 19.99m, Publisher = "Apress", PublicationYear = 2019, Url = "https://doi.org/10.1007/978-1-4842-4221-6" },
                new Models.Book {ID= Guid.NewGuid(), Title = "Pro Git", AuthorLastName = "Chacon", AuthorFirstName = "Scott", Price =45.99m, Publisher = "Apress; 2nd edition", PublicationYear = 2014, Url = "https://git-scm.com/book/en/v2" },
                new Models.Book {ID= Guid.NewGuid(), Title = "You Don't Know JS Yet", AuthorLastName = "Simpson", AuthorFirstName = "Kyle", Price = 14.99m, Publisher = "Independently published", PublicationYear = 2020, Url = "https://github.com/getify/You-Dont-Know-JS/tree/2nd-ed/get-started" }
            };
            return returnList;
        }

    }
}
