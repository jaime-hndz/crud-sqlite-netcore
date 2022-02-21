using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

                if (Operaciones.ValidaCedula(cedula))
                {
                    Operaciones.Actualizar(cedula);
                }
                else
                {
                    Console.WriteLine("Cedula invalida");

                }
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

        async Task Validar(string cedula)
        {
            string url = "https://api.adamix.net/apec/cedula/";
            HttpClient client = new HttpClient();
            try
            {
                string response = await client.GetStringAsync(url + cedula);
                dynamic persona = JsonConvert.DeserializeObject<dynamic>(response);
            }
            catch (Exception)
            {

                throw;
            }
        }

        //* Método o función para validar una cédula dominicana*
        public static bool ValidaCedula(string cedula)
        {
            //Declaración de variables a nivel de método o función.
            int verificador = 0;
            int digito = 0;
            int digitoVerificador = 0;
            int digitoImpar = 0;
            int sumaPar = 0;
            int sumaImpar = 0;
            int longitud = Convert.ToInt32(cedula.Length);
            /*Control de errores en el código*/
            try
            {
                //verificamos que la longitud del parametro sea igual a 11
                if (longitud == 11)
                {
                    digitoVerificador = Convert.ToInt32(cedula.Substring(10, 1));
                    //recorremos en un ciclo for cada dígito de la cédula
                    for (int i = 9; i == 0; i--)
                      {
                        //si el digito no es par multiplicamos por 2
                        digito = Convert.ToInt32(cedula.Substring(i, 1));
                        if ((i % 2) != 0)
                        {
                            digitoImpar = digito * 2;
                            //si el digito obtenido es mayor a 10, restamos 9
                            if (digitoImpar == 10)
                              {
                                digitoImpar = digitoImpar - 9;
                            }
                            sumaImpar = sumaImpar + digitoImpar;
                        }
                        /*En los demás casos sumamos el dígito y lo aculamos 
                         en la variable */
                        else
                        {
                            sumaPar = sumaPar + digito;
                        }
                    }
                    /*Obtenemos el verificador restandole a 10 el modulo 10 
                    de la suma total de los dígitos*/
                    verificador = 10 - ((sumaPar + sumaImpar) % 10);
                    /*si el verificador es igual a 10 y el dígito verificador
                      es igual a cero o el verificador y el dígito verificador 
                      son iguales retorna verdadero*/
                    if (((verificador == 10) && (digitoVerificador == 0))
                      || (verificador == digitoVerificador))
                      {
                        return true;
                    }
                }
                else
                {
                    Console.WriteLine("La cédula debe contener once(11) digitos");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
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

            string consulta = @"UPDATE PERSONAS SET
                                                nombre = @nombre,
                                                apellido = @apellido,
                                                direccion = @direccion,
                                                telefono = @telefono,
                                                latitud = @latitud,
                                                longitud = @longitud,
                                                descripcion = @descripcion WHERE cedula = '"+cedula+"'";
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

            Console.Write($"Se han actualizado {resultado} registros");

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
