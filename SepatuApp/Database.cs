using MySql.Data.MySqlClient;

namespace SepatuApp
{
    public class Database
    {
        private string connectionString = "server=localhost;database=sepatu_db;uid=root;pwd=;";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
