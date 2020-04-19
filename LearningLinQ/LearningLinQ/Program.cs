using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;

namespace LearningLinQ
{
    /// <summary>
    /// loads xml data from file
    /// </summary>
    class MainClass
    {

        public static void Main()
        {
            string connectionString = @"Server=localhost;Database=Foods;User Id=sa;Password=1Secure*Password1;";

            CreateCommand(@"INSERT INTO dbo.Oranges (ID,Name) VALUES('1', 'colorabi'); ",connectionString);
            
           
        }

    private static void CreateCommand(string queryString, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
