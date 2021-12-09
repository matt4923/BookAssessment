using BookApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BookApi.DB {
    /// <summary>
    /// Creating multiple classes in this particular situation may have been over-engineered. It would have been easier and more readable to just create one method and pass in the appropriate sort query.  
    /// However, I wanted to demo clean OOP practices that showcase inheritance, abstraction and reusable code.
    /// </summary>
    public abstract class SortBase {
        protected string _ConnString;
        protected abstract string SortQuery { get; }
        public SortBase(string connectionString) {
            _ConnString = connectionString;
        }

        internal List<Book> DoSort() {
            var returnList = new List<Book>();
            using(var conn = new SqlConnection(_ConnString)) {
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = SortQuery;
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while(reader.Read()) {
                        returnList.Add(new Book() {
                            ID = (Guid)reader["ID"],
                            Publisher = reader["Publisher"].ToString(),
                            Title = reader["Title"].ToString(),
                            AuthorLastName = reader["AuthorLastName"].ToString(),
                            AuthorFirstName = reader["AuthorFirstName"].ToString(),
                            Price = (decimal)reader["Price"],
                            PublicationYear = (int)reader["PublicationYear"],
                            Url = reader["URL"].ToString()
                        });
                    }
                }
            }

            return returnList;
        }
    }

    public class SortByPub:SortBase {
        //Sort 1- by Publisher, Author (last, first), then title
        public SortByPub(string connectionString) : base(connectionString) { }
        protected override string SortQuery => Sql.SortByPub;

    }

    public class SortByAuth:SortBase {
        //Sort 2- by Author (last, first) then title
        public SortByAuth(string connectionString) : base(connectionString) { }

        protected override string SortQuery => Sql.SortByAuth;
    }

    public class SortByPubStoredProcedure:SortBase{
        //Sort 3 - Stored procedure by Publisher, Author (last, first), then title
        public SortByPubStoredProcedure(string connectionString) : base(connectionString) { }

        protected override string SortQuery => Sql.ExecuteProcedureSortByPub;

    }

    public class SortByAuthStoredProcedure:SortBase {
        //Sort 4 - Stored Procedure by Author (last, first) then title
        public SortByAuthStoredProcedure(string connectionString) : base(connectionString) { }

        protected override string SortQuery => Sql.ExecuteProcedureSortByAuth;

        
    }
}
