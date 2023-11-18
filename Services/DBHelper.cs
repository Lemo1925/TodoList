using System;
using System.Data;
using MySqlConnector;

namespace AvaloniaTodoListApp.Services
{
    public class DBHelper
    {
        private static readonly MySqlConnectionStringBuilder build = new()
        {
            Server = "127.0.0.1",
            Port = 3306,
            Database = "todolist",
            UserID = "root",
            Password = "Lemo+1925",
            CharacterSet = "utf8mb4"
        };
        
        private static readonly MySqlConnection conn = new(build.ConnectionString);

        private static MySqlCommand Command(string MySqlCommand)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            var command = new MySqlCommand
            {
                CommandText = MySqlCommand,
                Connection = conn
            };
            return command;
        }

        public static int ExcuteNoneQuery(String SqlCommand)
        {
            int i = 0;
            var command = Command(SqlCommand);
            try{ i = command.ExecuteNonQuery();}
            catch (Exception ex) { Console.WriteLine(ex); }
            finally { conn.Close(); }
            return i;
        }

        public static DataTable ExcuteQuery(String SqlCommand)
        {
            DataTable data = new();
            var command = Command(SqlCommand);
            try {new MySqlDataAdapter(command).Fill(data);}
            catch (Exception ex) { Console.WriteLine(ex); }
            finally { conn.Close(); }
            return data;
        }
    }
}
