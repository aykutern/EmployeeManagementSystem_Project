using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement
{
    internal class DatabaseHandler
    {
        // Connection string to connect to the SQL Server database
        private static string connectionString = "Server=localhost\\SQLEXPRESS;Database=EmployeeManagement;Trusted_Connection=True;TrustServerCertificate=True;";

        // Method to get a new SQL connection with connection string
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        // Method to execute a SELECT query and return the result as a DataTable
        public static DataTable ExecuteSelect(string query)
        {
            using (SqlConnection conn = GetConnection())
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
        // Method to execute a non-query (INSERT, UPDATE, DELETE) and return true if successful
        public static bool ExecuteNonQuery(string query)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
