using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosLayer
{
    public class CustomerRepository
    {
        // Método para obtener todos los clientes de la base de datos
        public List<Customers> ObtenerTodos()
        {
            // Se abre una conexión a la base de datos usando el método estático GetSqlConnection de la clase DataBase
            using (var conexion = DataBase.GetSqlConnection())
            {
                // Se construye la consulta SQL para seleccionar todos los campos de la tabla Customers
                String selectFrom = "";
                selectFrom = selectFrom + "SELECT [CustomerID] " + "\n";
                selectFrom = selectFrom + "      ,[CompanyName] " + "\n";
                selectFrom = selectFrom + "      ,[ContactName] " + "\n";
                selectFrom = selectFrom + "      ,[ContactTitle] " + "\n";
                selectFrom = selectFrom + "      ,[Address] " + "\n";
                selectFrom = selectFrom + "      ,[City] " + "\n";
                selectFrom = selectFrom + "      ,[Region] " + "\n";
                selectFrom = selectFrom + "      ,[PostalCode] " + "\n";
                selectFrom = selectFrom + "      ,[Country] " + "\n";
                selectFrom = selectFrom + "      ,[Phone] " + "\n";
                selectFrom = selectFrom + "      ,[Fax] " + "\n";
                selectFrom = selectFrom + "  FROM [dbo].[Customers]";

                // Se crea un comando SQL con la consulta anterior y se ejecuta
                using (SqlCommand comando = new SqlCommand(selectFrom, conexion))
                {
                    SqlDataReader reader = comando.ExecuteReader(); // Lee los resultados de la consulta
                    List<Customers> Customers = new List<Customers>(); // Lista para almacenar los clientes

                    // Se recorren los resultados de la consulta y se leen los datos
                    while (reader.Read())
                    {
                        var customers = LeerDelDataReader(reader); 
                        Customers.Add(customers); // Se añade el cliente a la lista
                    }
                    return Customers; // Se devuelve la lista de clientes
                }
            }
        }

        // Método para obtener un cliente específico por su ID
        public Customers ObtenerPorID(string id)
        {
            // Se abre una conexión a la base de datos usando el método estático GetSqlConnection de la clase DataBase
            using (var conexion = DataBase.GetSqlConnection())
            {

                // Se construye la consulta SQL para seleccionar los campos de un cliente específico usando el ID
                String selectForID = "";
                selectForID = selectForID + "SELECT [CustomerID] " + "\n";
                selectForID = selectForID + "      ,[CompanyName] " + "\n";
                selectForID = selectForID + "      ,[ContactName] " + "\n";
                selectForID = selectForID + "      ,[ContactTitle] " + "\n";
                selectForID = selectForID + "      ,[Address] " + "\n";
                selectForID = selectForID + "      ,[City] " + "\n";
                selectForID = selectForID + "      ,[Region] " + "\n";
                selectForID = selectForID + "      ,[PostalCode] " + "\n";
                selectForID = selectForID + "      ,[Country] " + "\n";
                selectForID = selectForID + "      ,[Phone] " + "\n";
                selectForID = selectForID + "      ,[Fax] " + "\n";
                selectForID = selectForID + "  FROM [dbo].[Customers] " + "\n";
                selectForID = selectForID + $"  Where CustomerID = @customerId";

                
                using (SqlCommand comando = new SqlCommand(selectForID, conexion))
                {
                    comando.Parameters.AddWithValue("customerId", id); // Se añaden los parámetros para la consulta
                    var reader = comando.ExecuteReader(); // Lee los resultados de la consulta
                    Customers customers = null;
                    // Se verifica si se encontró algún resultado
                    if (reader.Read())
                    {
                        customers = LeerDelDataReader(reader); // Método que convierte los datos del reader a un objeto Customers
                    }
                    return customers; // Se devuelve el cliente encontrado
                }
            }
        }

        // Método que convierte los datos de un SqlDataReader a un objeto Customers
        public Customers LeerDelDataReader(SqlDataReader reader)
        {
            Customers customers = new Customers();
            customers.CustomerID = reader["CustomerID"] == DBNull.Value ? " " : (String)reader["CustomerID"];
            customers.CompanyName = reader["CompanyName"] == DBNull.Value ? "" : (String)reader["CompanyName"];
            customers.ContactName = reader["ContactName"] == DBNull.Value ? "" : (String)reader["ContactName"];
            customers.ContactTitle = reader["ContactTitle"] == DBNull.Value ? "" : (String)reader["ContactTitle"];
            customers.Address = reader["Address"] == DBNull.Value ? "" : (String)reader["Address"];
            customers.City = reader["City"] == DBNull.Value ? "" : (String)reader["City"];
            customers.Region = reader["Region"] == DBNull.Value ? "" : (String)reader["Region"];
            customers.PostalCode = reader["PostalCode"] == DBNull.Value ? "" : (String)reader["PostalCode"];
            customers.Country = reader["Country"] == DBNull.Value ? "" : (String)reader["Country"];
            customers.Phone = reader["Phone"] == DBNull.Value ? "" : (String)reader["Phone"];
            customers.Fax = reader["Fax"] == DBNull.Value ? "" : (String)reader["Fax"];
            return customers; // Se devuelve el objeto Customers con los datos leídos
        }

        // Método para insertar un nuevo cliente en la base de datos
        public int InsertarCliente(Customers customer)
        {
            // Se abre una conexión a la base de datos usando el método estático GetSqlConnection de la clase DataBase
            using (var conexion = DataBase.GetSqlConnection())
            {
                // Se construye la consulta SQL para insertar un nuevo cliente en la tabla Customers
                String insertInto = "";
                insertInto = insertInto + "INSERT INTO [dbo].[Customers] " + "\n";
                insertInto = insertInto + "           ([CustomerID] " + "\n";
                insertInto = insertInto + "           ,[CompanyName] " + "\n";
                insertInto = insertInto + "           ,[ContactName] " + "\n";
                insertInto = insertInto + "           ,[ContactTitle] " + "\n";
                insertInto = insertInto + "           ,[Address] " + "\n";
                insertInto = insertInto + "           ,[City]) " + "\n";
                insertInto = insertInto + "     VALUES " + "\n";
                insertInto = insertInto + "           (@CustomerID " + "\n";
                insertInto = insertInto + "           ,@CompanyName " + "\n";
                insertInto = insertInto + "           ,@ContactName " + "\n";
                insertInto = insertInto + "           ,@ContactTitle " + "\n";
                insertInto = insertInto + "           ,@Address " + "\n";
                insertInto = insertInto + "           ,@City)";

                // Se crea un comando SQL con la consulta anterior y se ejecuta
                using (var comando = new SqlCommand(insertInto, conexion))
                {
                    int insertados = parametrosCliente(customer, comando); // Se añaden los parámetros al comando y se ejecuta
                    return insertados; // Se devuelve el número de registros afectados
                }
            }
        }

        // Método para actualizar los datos de un cliente en la base de datos
        public int ActualizarCliente(Customers customer)
        {
            // Se abre una conexión a la base de datos usando el método estático GetSqlConnection de la clase DataBase
            using (var conexion = DataBase.GetSqlConnection())
            {
                // Se construye la consulta SQL para actualizar los datos de un cliente específico usando su ID
                String ActualizarCustomerPorID = "";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "UPDATE [dbo].[Customers] " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "   SET [CustomerID] = @CustomerID " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[CompanyName] = @CompanyName " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[ContactName] = @ContactName " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[ContactTitle] = @ContactTitle " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[Address] = @Address " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + "      ,[City] = @City " + "\n";
                ActualizarCustomerPorID = ActualizarCustomerPorID + " WHERE CustomerID= @CustomerID";
                // Se crea un comando SQL con la consulta anterior y se ejecuta
                using (var comando = new SqlCommand(ActualizarCustomerPorID, conexion))
                {
                    int actualizados = parametrosCliente(customer, comando); // Se añaden los parámetros al comando y se ejecuta
                    return actualizados; // Se devuelve el número de registros afectados
                }
            }
        }

        // Método que añade los parámetros necesarios al comando SQL y ejecuta la consulta
        public int parametrosCliente(Customers customer, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("CustomerID", customer.CustomerID);
            comando.Parameters.AddWithValue("CompanyName", customer.CompanyName);
            comando.Parameters.AddWithValue("ContactName", customer.ContactName);
            comando.Parameters.AddWithValue("ContactTitle", customer.ContactName); 
            comando.Parameters.AddWithValue("Address", customer.Address);
            comando.Parameters.AddWithValue("City", customer.City);
            var insertados = comando.ExecuteNonQuery(); // Ejecuta la consulta SQL y devuelve el número de registros afectados
            return insertados;
        }

        // Método para eliminar un cliente de la base de datos usando su ID
        public int EliminarCliente(string id)
        {
            // Se abre una conexión a la base de datos usando el método estático GetSqlConnection de la clase DataBase
            using (var conexion = DataBase.GetSqlConnection())
            {
                // Se construye la consulta SQL para eliminar un cliente específico usando su ID
                String EliminarCliente = "";
                EliminarCliente = EliminarCliente + "DELETE FROM [dbo].[Customers] " + "\n";
                EliminarCliente = EliminarCliente + "      WHERE CustomerID = @CustomerID";
                // Se crea un comando SQL con la consulta anterior y se ejecuta
                using (SqlCommand comando = new SqlCommand(EliminarCliente, conexion))
                {
                    comando.Parameters.AddWithValue("@CustomerID", id); // Se añade el parámetro necesario para la consulta
                    int elimindos = comando.ExecuteNonQuery(); // Ejecuta la consulta SQL y devuelve el número de registros afectados
                    return elimindos;
                }
            }
        }
    }
}
