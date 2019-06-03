using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace DncZeus.Api.DbConfig
{
    public class DBConfig
    {
        //ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        public static string DefaultSqlConnectionString = "";

        public static IDbConnection GetSqlConnection(string sqlConnectionString = null)
        {
            if (string.IsNullOrWhiteSpace(sqlConnectionString))
            {
                sqlConnectionString = DefaultSqlConnectionString;
            }
            IDbConnection conn = new MySqlConnection(sqlConnectionString);
            conn.Open();
            return conn;
        }
    }
}
