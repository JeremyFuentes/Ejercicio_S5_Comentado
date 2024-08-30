using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace DatosLayer
{
    // Clase que maneja la conexión a la base de datos
    public class DataBase
    {
        // Propiedad estática que define el tiempo de espera para la conexión
        public static int ConnetionTimeout { get; set; }

        // Propiedad estática que define el nombre de la aplicación que se conecta a la base de datos
        public static string ApplicationName { get; set; }

        // Propiedad que devuelve la cadena de conexión de la base de datos
        public static String ConnectionString
        {
            get
            {
                // Obtiene la cadena de conexión desde el archivo de configuración (app.config)
                String CadenaConexion = ConfigurationManager
                   .ConnectionStrings["NWConnection"]
                   .ConnectionString;

                // Utiliza SqlConnectionStringBuilder para construir la cadena de conexión
                SqlConnectionStringBuilder conexionBuilder =
                    new SqlConnectionStringBuilder(CadenaConexion);

                // Asigna el nombre de la aplicación si se ha definido
                conexionBuilder.ApplicationName =
                ApplicationName ?? conexionBuilder.ApplicationName;

                // Asigna el tiempo de espera de conexión si se ha definido, de lo contrario usa el predeterminado
                conexionBuilder.ConnectTimeout = (ConnetionTimeout > 0)
                    ? ConnetionTimeout : conexionBuilder.ConnectTimeout;

                // Retorna la cadena de conexión construida
                return conexionBuilder.ToString();
            }

        }

        // Método para obtener una conexión SQL ya abierta a la base de datos
        public static SqlConnection GetSql()
        {
            // Crea una nueva conexión usando la cadena de conexión construida
            SqlConnection conexion = new SqlConnection(ConnectionString);

            // Abre la conexión a la base de datos
            conexion.Open();

            // Retorna la conexión abierta
            return conexion;
        }

    }
}
