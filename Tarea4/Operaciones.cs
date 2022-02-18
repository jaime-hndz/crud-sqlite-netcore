using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea4
{
    public class Operaciones
    {
        public static void Insertar()
        {
            bool continuar = true;
            while (continuar)
            {
                string cedula;
                string nombre;
                string apellido;
                string direccion;
                string telefono;
                string latitud;
                string longitud;
                string descripcion;


                Console.Clear();
                Console.WriteLine(@"
                    -- INSERTAR --
                ");

                Console.Write("Cedula: ");
                cedula = Console.ReadLine();
                Console.Write("Nombre: ");
                nombre = Console.ReadLine();
                Console.Write("Apellido: ");
                apellido = Console.ReadLine();
                Console.Write("Direccion: ");
                direccion = Console.ReadLine();
                Console.Write("Telefono: ");
                telefono = Console.ReadLine();
                Console.Write("Latitud: ");
                latitud = Console.ReadLine();
                Console.Write("Longitud: ");
                longitud = Console.ReadLine();
                Console.Write("Descripcion: ");
                descripcion = Console.ReadLine();

                Database DataObjeto = new Database();

                string consulta = @"INSERT INTO PERSONAS VALUES (@cedula,@nombre,@apellido,@direccion,@telefono,@latitud,@longitud,@descripcion)";
                SQLiteCommand comando = new SQLiteCommand(consulta, DataObjeto.conexion);
                DataObjeto.OpenConnection();
                comando.Parameters.AddWithValue("@cedula", cedula);
                comando.Parameters.AddWithValue("@nombre", nombre);
                comando.Parameters.AddWithValue("@apellido", apellido);
                comando.Parameters.AddWithValue("@direccion", direccion);
                comando.Parameters.AddWithValue("@telefono", telefono);
                comando.Parameters.AddWithValue("@latitud", latitud);
                comando.Parameters.AddWithValue("@longitud", longitud);
                comando.Parameters.AddWithValue("@descripcion", descripcion);
                var resultado = comando.ExecuteNonQuery();

                Console.Write($"Se han agregado {resultado} registros");

                DataObjeto.CloseConnection();

                continuar = Operaciones.MenuContinuar();
            }
        }

        public static void Modificar()
        {
            bool continuar = true;
            while (continuar)
            {
                Console.Clear();
                Console.WriteLine(@"
                    -- MODIFICAR --
                ");

                Database DataObjeto = new Database();

                string consulta = "SELECT * FROM PERSONAS";
                SQLiteCommand comando = new SQLiteCommand(consulta, DataObjeto.conexion);
                DataObjeto.OpenConnection();
                SQLiteDataReader resultado = comando.ExecuteReader();

                if (resultado.HasRows)
                {
                    while (resultado.Read())
                    {
                        Console.WriteLine($"{resultado["CEDULA"]} - {resultado["NOMBRE"]} {resultado["APELLIDO"]} ");
                    }
                }

                Console.WriteLine("\n\nEscriba la cedula de pa persona que desa modificar: ");
                string cedula = Console.ReadLine();

                Operaciones.Actualizar(cedula);

                DataObjeto.CloseConnection();

                continuar = Operaciones.MenuContinuar();
            }
        }

        public static void Actualizar(string cedula)
        {

            string nombre;
            string apellido;
            string direccion;
            string telefono;
            string latitud;
            string longitud;
            string descripcion;

            Console.Write("Nombre: ");
            nombre = Console.ReadLine();
            Console.Write("Apellido: ");
            apellido = Console.ReadLine();
            Console.Write("Direccion: ");
            direccion = Console.ReadLine();
            Console.Write("Telefono: ");
            telefono = Console.ReadLine();
            Console.Write("Latitud: ");
            latitud = Console.ReadLine();
            Console.Write("Longitud: ");
            longitud = Console.ReadLine();
            Console.Write("Descripcion: ");
            descripcion = Console.ReadLine();

            Database DataObjeto = new Database();

            string consulta = @"INSERT INTO PERSONAS VALUES (@cedula,@nombre,@apellido,@direccion,@telefono,@latitud,@longitud,@descripcion)";
            SQLiteCommand comando = new SQLiteCommand(consulta, DataObjeto.conexion);
            DataObjeto.OpenConnection();
            comando.Parameters.AddWithValue("@cedula", cedula);
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@apellido", apellido);
            comando.Parameters.AddWithValue("@direccion", direccion);
            comando.Parameters.AddWithValue("@telefono", telefono);
            comando.Parameters.AddWithValue("@latitud", latitud);
            comando.Parameters.AddWithValue("@longitud", longitud);
            comando.Parameters.AddWithValue("@descripcion", descripcion);
            var resultado = comando.ExecuteNonQuery();

            Console.Write($"Se han agregado {resultado} registros");

            DataObjeto.CloseConnection();


        }
        public static bool MenuContinuar()
        {
            Console.Write(@"
                    Contiunar?
                    1- Si.
                    Otro- No.

                    Eliga una opcion: ");
            string opt = Console.ReadLine();

            if (opt != "1")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
