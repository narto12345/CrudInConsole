using System.Data.SqlClient;

namespace principal
{
    /// <summary>
    /// La función de esta clase es crear la conexión a la base datos SQL Server llamada "PilotTestDB".
    /// </summary>
    class Conexion
    {
        private const string CONNECTION_STRING = "Data Source=PC_Nicolas;Initial Catalog=PilotTestDB;User ID=sa;Password=12345;";

        public static SqlConnection GetConnection() => new SqlConnection(Conexion.CONNECTION_STRING);

    }
}