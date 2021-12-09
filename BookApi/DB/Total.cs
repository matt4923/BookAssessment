using System;
using System.Data.SqlClient;

namespace BookApi.DB {
    public class Total {
        public static decimal GetTotalPriceOfBooks(string connectionString) {
            using(var conn = new SqlConnection(connectionString)) {
                using(SqlCommand cmd = conn.CreateCommand()) {
                    cmd.CommandText = Sql.SumBooks;
                    conn.Open();
                    var total = cmd.ExecuteScalar();
                    return (decimal)total;
                }
            }
        }
    }
}
