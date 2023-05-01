using CitizenFX.Core;
using CitizenFX.Core.Native;
using MySql.Data.MySqlClient;

namespace Server.Utils
{
    class Database
    {
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(GetConnectionString());
        }

        private static string GetConnectionString ()
        {
            string MYSQL_HOST = API.GetConvar("MYSQL_HOST", null);
            string MYSQL_PORT = API.GetConvar("MYSQL_PORT", null);
            string MYSQL_USER = API.GetConvar("MYSQL_USER", null);
            string MYSQL_PASS = API.GetConvar("MYSQL_PASS", null);
            string MYSQL_DB = API.GetConvar("MYSQL_DB", null);
            if (MYSQL_HOST == null)
            {
                Debug.WriteLine("Convars in configruation are not set!");
            }
            return $"Server={MYSQL_HOST}; Port={MYSQL_PORT}; User ID={MYSQL_USER}; Password={MYSQL_PASS}; Database={MYSQL_DB}";
        }
    }
}
