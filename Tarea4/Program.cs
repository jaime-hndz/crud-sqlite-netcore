
using Tarea4;

bool continuar = true;

while (continuar)
{
    Console.Clear();
    Console.WriteLine(@"
        -- PERSONAS QUE SE ROBAN LA LUZ --
    ");


    Console.Write(@"
        Que desea hacer?
        1. Agregar.
        2. Modificar.
        3. Exportar caso.
        99. Salir

        Eliga una opcion: ");
    string opt;
    opt = Console.ReadLine();

    switch (opt)
    {
        case "1":
            Operaciones.Insertar();
            break;
        case "2":
            Operaciones.Modificar();
            break;
        case "3":
            break;
        case "99":
            Console.WriteLine("Saliendo...");
            continuar = false;
            break;
        default:
            Console.WriteLine("Escriba una opcion de las disponibles");
            Console.ReadKey();
            break;
    }
}


