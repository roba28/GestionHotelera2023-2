
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProyectoGestionHotelera.Models;
using System.Data.SqlClient;

// Espacio de nombres del controlador
namespace ProyectoGestionHotelera.Controllers
{
    // Definición de la clase HotelesController que hereda de Controller
    public class HotelesController : Controller
    {
        // Acción para mostrar la lista de hoteles y habitaciones
        public IActionResult ListaHoteles()
        {
            // Se obtiene el nombre del usuario
            string v = Usuario.Nombre;
            // Lista para almacenar las habitaciones disponibles
            List<Habitacion> HabitacionesDisponibles = new List<Habitacion>();

            // Cadena de conexión a la base de datos
            string connectionString = "Server=ADSP-13207\\MSSQLSERVER01;Database=GestionHotelera;Trusted_Connection=True;TrustServerCertificate=true;";
            // Consulta para seleccionar todas las habitaciones
            string queryString = "SELECT * FROM Habitaciones";

            // Uso de la conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Lectura de resultados y almacenamiento en la lista de habitaciones
                while (reader.Read())
                {
                    HabitacionesDisponibles.Add(new Habitacion
                    {
                        Nombre = reader["Nombre"].ToString(),
                        Torre = reader["Torre"].ToString(),
                        Piso = reader["Piso"].ToString(),
                        NumeroHabitacion = reader["NumeroHabitacion"].ToString(),
                        Disponibilidad = Convert.ToBoolean(reader["Disponibilidad"])
                    });
                }

                reader.Close();
            }

            // Devuelve la vista con el modelo de habitaciones disponibles
            return View("Hoteles", HabitacionesDisponibles);
        }

        // Acción para mostrar la vista de hotel
        public IActionResult hotel()
        {
            // Se obtiene el nombre del usuario
            string v = Usuario.Nombre;
            // Retorna la vista correspondiente
            return View();
        }

        // Acción para mostrar las habitaciones del Hotel Continental de Marruecos
        public IActionResult HabitacionesMarruecos()
        {
            // Lista para almacenar las habitaciones disponibles
            List<Habitacion> HabitacionesDisponibles = new List<Habitacion>();

            // Cadena de conexión a la base de datos
            string connectionString = "Server=ADSP-13207\\MSSQLSERVER01;Database=GestionHotelera;Trusted_Connection=True;TrustServerCertificate=true;";
            // Consulta para seleccionar las habitaciones del Hotel Continental de Marruecos
            string queryString = "SELECT * FROM Habitaciones WHERE Nombre = 'Hotel Continental de Marruecos'";

            // Uso de la conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Lectura de resultados y almacenamiento en la lista de habitaciones
                while (reader.Read())
                {
                    HabitacionesDisponibles.Add(new Habitacion
                    {
                        Nombre = reader["Nombre"].ToString(),
                        Torre = reader["Torre"].ToString(),
                        Piso = reader["Piso"].ToString(),
                        NumeroHabitacion = reader["NumeroHabitacion"].ToString(),
                        Disponibilidad = Convert.ToBoolean(reader["Disponibilidad"])
                    });
                }

                reader.Close();
            }

            // Devuelve la vista con el modelo de habitaciones disponibles
            return View("HabitacionesMarruecos", HabitacionesDisponibles);
        }

        // Acción para mostrar las habitaciones del Hotel Continental de New York
        public IActionResult HabitacionesNewYork()
        {
            // Lista para almacenar las habitaciones disponibles
            List<Habitacion> HabitacionesDisponibles = new List<Habitacion>();

            // Cadena de conexión a la base de datos
            string connectionString = "Server=ADSP-13207\\MSSQLSERVER01;Database=GestionHotelera;Trusted_Connection=True;TrustServerCertificate=true;";
            // Consulta para seleccionar las habitaciones del Hotel Continental de New York
            string queryString = "SELECT * FROM Habitaciones WHERE Nombre = 'Hotel Continental de New York'";

            // Uso de la conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Lectura de resultados y almacenamiento en la lista de habitaciones
                while (reader.Read())
                {
                    HabitacionesDisponibles.Add(new Habitacion
                    {
                        Nombre = reader["Nombre"].ToString(),
                        Torre = reader["Torre"].ToString(),
                        Piso = reader["Piso"].ToString(),
                        NumeroHabitacion = reader["NumeroHabitacion"].ToString(),
                        Disponibilidad = Convert.ToBoolean(reader["Disponibilidad"])
                    });
                }

                reader.Close();
            }

            // Devuelve la vista con el modelo de habitaciones disponibles
            return View(HabitacionesDisponibles);
        }

        // Acción para mostrar las habitaciones del Hotel Continental de Roma
        public IActionResult HabitacionesRoma()
        {
            // Lista para almacenar las habitaciones disponibles
            List<Habitacion> HabitacionesDisponibles = new List<Habitacion>();

            // Cadena de conexión a la base de datos
            string connectionString = "Server=ADSP-13207\\MSSQLSERVER01;Database=GestionHotelera;Trusted_Connection=True;TrustServerCertificate=true;";
            // Consulta para seleccionar las habitaciones del Hotel Continental de Roma
            string queryString = "SELECT * FROM Habitaciones WHERE Nombre = 'Hotel Continental de Roma'";

            // Uso de la conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Lectura de resultados y almacenamiento en la lista de habitaciones
                while (reader.Read())
                {
                    HabitacionesDisponibles.Add(new Habitacion
                    {
                        Nombre = reader["Nombre"].ToString(),
                        Torre = reader["Torre"].ToString(),
                        Piso = reader["Piso"].ToString(),
                        NumeroHabitacion = reader["NumeroHabitacion"].ToString(),
                        Disponibilidad = Convert.ToBoolean(reader["Disponibilidad"])
                    });
                }

                reader.Close();
            }

            // Devuelve la vista con el modelo de habitaciones disponibles
            return View(HabitacionesDisponibles);
        }

        // Acción para mostrar las habitaciones del Hotel Continental de Osaka Tokyo
        public IActionResult HabitacionesTokyo()
        {
            // Lista para almacenar las habitaciones disponibles
            List<Habitacion> HabitacionesDisponibles = new List<Habitacion>();

            // Cadena de conexión a la base de datos
            string connectionString = "Server=ADSP-13207\\MSSQLSERVER01;Database=GestionHotelera;Trusted_Connection=True;TrustServerCertificate=true;";
            // Consulta para seleccionar las habitaciones del Hotel Continental de Osaka Tokyo
            string queryString = "SELECT * FROM Habitaciones WHERE Nombre = 'Hotel Continental de Osaka Tokyo'";

            // Uso de la conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Lectura de resultados y almacenamiento en la lista de habitaciones
                while (reader.Read())
                {
                    HabitacionesDisponibles.Add(new Habitacion
                    {
                        Nombre = reader["Nombre"].ToString(),
                        Torre = reader["Torre"].ToString(),
                        Piso = reader["Piso"].ToString(),
                        NumeroHabitacion = reader["NumeroHabitacion"].ToString(),
                        Disponibilidad = Convert.ToBoolean(reader["Disponibilidad"])
                    });
                }

                reader.Close();
            }

            // Devuelve la vista con el modelo de habitaciones disponibles
            return View(HabitacionesDisponibles);
        }

        // Lista de habitaciones (propiedad)
        public List<string> Habitaciones { get; set; }

        // Acción para mostrar la vista de búsqueda de habitación disponible
        public IActionResult BuscarHabitacionDisponible()
        {
            return View();
        }

        // Acción de búsqueda de habitaciones (POST)
        [HttpPost]
        public IActionResult BuscarHabitaciones(string torre, string piso, string numeroHabitacion)
        {
            // Carga las habitaciones según los parámetros de búsqueda
            CargarHabitaciones(torre, piso, numeroHabitacion);
            // Devuelve la vista con el modelo de habitaciones disponibles
            return View("BuscarHabitacionDisponible", Habitaciones);
        }

        // Acción para mostrar todas las habitaciones
        public IActionResult FiltrarTodos()
        {
            // Carga todas las habitaciones
            CargarHabitaciones();
            // Devuelve la vista con el modelo de habitaciones disponibles
            return View("BuscarHabitacionDisponible", Habitaciones);
        }

        // Método para cargar todas las habitaciones
        public IActionResult CargarHabitaciones()
        {
            // Inicializa la lista de habitaciones
            Habitaciones = new List<string>();

            // Cadena de conexión a la base de datos
            string connectionString = "Server=ADSP-13207\\MSSQLSERVER01;Database=GestionHotelera;Trusted_Connection=True;TrustServerCertificate=true;";

            // Consulta para seleccionar todas las habitaciones
            string queryString = "SELECT Nombre, Torre, Piso, NumeroHabitacion, Disponibilidad FROM Habitaciones";

            // Uso de la conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Lectura de resultados y almacenamiento en la lista de habitaciones
                while (reader.Read())
                {
                    string habitacion = ObtenerDatosHabitacion(reader);
                    Habitaciones.Add(habitacion);
                }

                reader.Close();
            }

            // Devuelve la vista con el modelo de habitaciones disponibles
            return View("BuscarHabitacionDisponible", Habitaciones);
        }

        // Método para cargar habitaciones según parámetros de búsqueda
        private void CargarHabitaciones(string torre, string piso, string numeroHabitacion)
        {
            // Inicializa la lista de habitaciones
            Habitaciones = new List<string>();

            // Cadena de conexión a la base de datos
            string connectionString = "Server=ADSP-13207\\MSSQLSERVER01;Database=GestionHotelera;Trusted_Connection=True;TrustServerCertificate=true;";

            // Consulta para seleccionar habitaciones según parámetros de búsqueda
            string queryString = "SELECT Nombre, Torre, Piso, NumeroHabitacion, Disponibilidad FROM Habitaciones WHERE Torre = @Torre AND Piso = @Piso AND NumeroHabitacion = @NumeroHabitacion";

            // Uso de la conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@Torre", torre);
                command.Parameters.AddWithValue("@Piso", piso);
                command.Parameters.AddWithValue("@NumeroHabitacion", numeroHabitacion);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Lectura de resultados y almacenamiento en la lista de habitaciones
                while (reader.Read())
                {
                    string habitacion = ObtenerDatosHabitacion(reader);
                    Habitaciones.Add(habitacion);
                }

                reader.Close();
            }
        }

        // Método para obtener datos de una habitación en formato de cadena
        private string ObtenerDatosHabitacion(SqlDataReader reader)
        {
            string nombre = reader["Nombre"].ToString();
            string torre = reader["Torre"].ToString();
            string piso = reader["Piso"].ToString();
            string numeroHabitacion = reader["NumeroHabitacion"].ToString();
            int disponibilidad = Convert.ToInt32(reader["Disponibilidad"]);

            string estado = disponibilidad == 1 ? "Disponible" : "No Disponible";

            return $"{nombre}, {torre}, {piso}, {numeroHabitacion}, Disponibilidad: {estado}";
        }
    }
}
