using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BookApi.DB {
    /// <summary>
    /// If connection string is valid, this will create the database, schema objects, stored procedures and sample data.
    /// I would like to create a single class that handles db connection and building the connections.  
    /// For simplicity and time I just copied and pasted these everywhere I needed db calls (using blocks).
    /// </summary>
    public class Initialization {
        internal static void InitializeDb(string fullConnString) {
            var tempConnString = GetConnStringNoDb(fullConnString);
            if(!CascadeBookDbExists(tempConnString)) {
                //db doesn't exist, create it and add all demo schema and data
                CreateDb(tempConnString);
                CreateBookTableAndSampleData(fullConnString);
                AddStoredProcedures(fullConnString);
            }//else -> TO DO: should make more robust to verify individual tables/stored procedures exist even if the DB exists.  I can do this upon request.
        }

        public static void AddStoredProcedures(string fullConnString) {
            CreateStoredProcedure(fullConnString, Sql.CreateProcSortByAuth);
            CreateStoredProcedure(fullConnString, Sql.CreateProcSortByPub);
        }

        private static void CreateBookTableAndSampleData(string fullConnString) {
            using(var conn = new SqlConnection(fullConnString)) {
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = Sql.CreateBookTable;
                    conn.Open();
                    if(cmd.ExecuteNonQuery() == 0) {
                        throw new Exception("Book table could not be created.");
                    }
                }
                CreateInitialData(conn);
            }
        }

        private static void CreateInitialData(SqlConnection conn) {
            //add sample data - keep existing connection open
            var sampleData = TestData.GetSampleBooks();
            using (SqlCommand cmd = conn.CreateCommand()) {
                cmd.Connection = conn;
                cmd.CommandText = Sql.InsertBook;
                foreach(var sBook in sampleData) {
                    cmd.Parameters.AddWithValue("@ID", sBook.ID);   
                    cmd.Parameters.AddWithValue("@Publisher", sBook.Publisher);
                    cmd.Parameters.AddWithValue("@Title", sBook.Title);
                    cmd.Parameters.AddWithValue("@AuthorLastName", sBook.AuthorLastName);
                    cmd.Parameters.AddWithValue("@AuthorFirstName", sBook.AuthorFirstName);
                    cmd.Parameters.AddWithValue("@Price", sBook.Price);
                    cmd.Parameters.AddWithValue("@PublicationYear", sBook.PublicationYear);
                    cmd.Parameters.AddWithValue("@Url", sBook.Url);
                    if(cmd.ExecuteNonQuery() == 0) {
                        throw new Exception($"Book '{sBook.Title}' could not be added.");
                    } else {
                        cmd.Parameters.Clear();
                    }
                }

            }
        }

        private static void CreateDb(string tempConnString) {
            //could be modified to utilize specific name, for assessment, always use "CascadeBook" db
            using(var conn = new SqlConnection(tempConnString)) {
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = Sql.CreateCascadeBookDatabase;
                    conn.Open();
                    if(cmd.ExecuteNonQuery() == 0) {
                        throw new Exception("Database could not be created.");
                    }
                }
            }
        }

        private static bool CascadeBookDbExists(string tempConnString) {
            using(var conn = new SqlConnection(tempConnString)) {
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = Sql.DatabaseNames;
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while(reader.Read()) {
                        if(reader["Name"].ToString().ToUpper() == Constant.DBNAME.ToUpper()) {
                            return true;
                        }
                    }
                    return false;
                }
            }
        }

        private static string GetConnStringNoDb(string fullConnString) {
            //strip db from conn string to run system query
            var connStringBuilder = new SqlConnectionStringBuilder(fullConnString);
            connStringBuilder.Remove("Database");
            connStringBuilder.Remove("Initial Catalog");
            return connStringBuilder.ConnectionString;
        }

        public static void CreateStoredProcedure(string fullConnString, string createProcedureSql) {
            using(var conn = new SqlConnection(fullConnString)) {
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = createProcedureSql;
                    conn.Open();
                    if(cmd.ExecuteNonQuery() == 0) {
                        throw new Exception("Stored procedure could not be created.");
                    }
                }
            }
        }
    }
}
