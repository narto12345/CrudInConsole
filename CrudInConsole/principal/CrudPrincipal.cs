using System.Data.SqlClient;

namespace principal
{
    class CrudPrincipal
    {
        static void Main(string[] args)
        {

            string? accion = "";

            do
            {
                Console.WriteLine("CRUD in Console\n");

                Console.WriteLine("Nombre de la base de datos: PilotTestDB\nTabla:person\n");

                CrudPrincipal.MostrarPersonas();

                Console.WriteLine("\nEscriba la acción deseada...");
                Console.WriteLine("1. Crear usuario\n2. Editar usuario\n3. Eliminar usuario\n4. Salir\n");

                accion = Console.ReadLine();
                switch (accion)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine(CrudPrincipal.CrearPersona() >= 1 ? "Persona creada\n" : "No fue posible crear la persona\n");

                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine(CrudPrincipal.EditarPersona() >= 1 ? "Persona editada\n" : "No existe la persona que desea editar\n");
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine(CrudPrincipal.EliminarPersona() >= 1 ? "Persona eliminada\n" : "No existe la persona que desea eliminar\n");
                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("Programa finalizado");
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            } while (accion != "4");

        }

        private static void MostrarPersonas()
        {

            SqlConnection conexion = Conexion.GetConnection();
            conexion.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM person", conexion);
            SqlDataReader reader = command.ExecuteReader();

            Console.WriteLine($"*****************************");
            Console.WriteLine($"*    Id     |     Nombre    *");
            Console.WriteLine($"*****************************");

            while (reader.Read())
            {
                int id = (int)reader["id"];
                string nombre = (string)reader["nombre"];
                if (id >= 10)
                {
                    Console.Write($"      {id}");
                    Console.WriteLine($"      {nombre}");
                }
                else
                {
                    Console.Write($"      {id}");
                    Console.WriteLine($"       {nombre}");
                }

            }

            reader.Close();
            conexion.Close();

            Console.WriteLine($"*****************************");

        }

        private static int CrearPersona()
        {

            Console.Write("Digite el nombre de la persona:\n");

            string? nombre = Console.ReadLine();

            SqlConnection conexion = Conexion.GetConnection();
            conexion.Open();
            SqlCommand command = new SqlCommand("INSERT INTO person(nombre) VALUES(@nombre)", conexion);

            command.Parameters.AddWithValue("@nombre", nombre);

            return command.ExecuteNonQuery();

        }

        private static int EliminarPersona()
        {

            try
            {

                CrudPrincipal.MostrarPersonas();
                Console.WriteLine();

                Console.Write("Digite el 'id' de la persona que desea eliminar:");
                int? id = Int32.Parse(Console.ReadLine() ?? "0");

                SqlConnection conexion = Conexion.GetConnection();
                conexion.Open();
                SqlCommand command = new SqlCommand("DELETE FROM person WHERE id = @id", conexion);

                command.Parameters.AddWithValue("@id", id);
                Console.Clear();
                return command.ExecuteNonQuery();

            }
            catch (FormatException) { }

            return 0;

        }

        private static int EditarPersona()
        {

            try
            {

                CrudPrincipal.MostrarPersonas();
                Console.WriteLine();

                Console.Write("Digite el 'id' de la persona que desea editar:");
                int? id = Int32.Parse(Console.ReadLine() ?? "0");

                Console.Write("Digite el nuevo nombre de la persona:");
                string? nombre = Console.ReadLine();

                SqlConnection conexion = Conexion.GetConnection();
                conexion.Open();
                SqlCommand command = new SqlCommand("UPDATE person SET nombre = @nombre WHERE id = @id", conexion);

                command.Parameters.AddWithValue("@nombre", nombre);
                command.Parameters.AddWithValue("@id", id);
                Console.Clear();
                return command.ExecuteNonQuery();

            }
            catch (FormatException) { }

            return 0;

        }

    }
}