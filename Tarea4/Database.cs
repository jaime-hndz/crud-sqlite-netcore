using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea4
{
    internal class Database
    {
        public SQLiteConnection conexion;

        public Database()
        {
            conexion = new SQLiteConnection("Data Source=database.sqlite3");
            if (!File.Exists("./database.sqlite3"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                Console.WriteLine("Base de datos creada");
            }
        }

        public void OpenConnection()
        {
            if (conexion.State != System.Data.ConnectionState.Open)
            {
                conexion.Open();
            }
        }
        public void CloseConnection()
        {
            if (conexion.State != System.Data.ConnectionState.Closed)
            {
                conexion.Close();
            }
        }

    }
}
