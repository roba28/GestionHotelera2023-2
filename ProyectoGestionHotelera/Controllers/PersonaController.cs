// Espacios de nombres necesarios
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;


namespace ProyectoGestionHotelera.Controllers
{
    // Definición de la clase PersonaController que hereda de Controller
    public class PersonaController : Controller
    {
        // Propiedad para almacenar las reservaciones (modelo)
        public List<string> Reservaciones { get; set; }

        // Acción para mostrar la vista de búsqueda de persona
        public IActionResult BuscarPersona()
        {
            return View();
        }

        // Acción de búsqueda de reservaciones por persona (POST)
        [HttpPost]
        public IActionResult BuscarPorPersona(string cedulaIdentidad)
        {
            // Validación de la longitud de la cédula
            if (!ValidarCedula(cedulaIdentidad))
            {
                ModelState.AddModelError(string.Empty, "La cédula de identidad no cumple con los requisitos.");
                // Carga todas las reservaciones
                CargarReservaciones();
                return View("BuscarPersona");
            }

            // Carga las reservaciones según la cédula proporcionada
            CargarReservaciones(cedulaIdentidad);
            return View("BuscarPersona", Reservaciones);
        }

        // Acción para mostrar todas las reservaciones
        public IActionResult FiltrarTodos()
        {
            // Carga todas las reservaciones
            CargarReservaciones();
            return View("BuscarPersona");
        }

        // Método para cargar todas las reservaciones
        public IActionResult CargarReservaciones()
        {
            // Inicializa la lista de reservaciones
            Reservaciones = new List<string>();

            // Cadena de conexión a la base de datos
            string connectionString = "Server=ADSP-13207\\MSSQLSERVER01;Database=GestionHotelera;Trusted_Connection=True;TrustServerCertificate=true;";

            // Consulta para seleccionar todas las reservaciones
            string queryString = "SELECT Nombre, PrimerApellido, SegundoApellido, CedulaIdentidad, Nacionalidad, Telefono, CorreoElectronico, NombreHotel, Torre, Piso, NumeroHabitacion FROM Reservaciones";

            // Uso de la conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Lectura de resultados y almacenamiento en la lista de reservaciones
                while (reader.Read())
                {
                    string reservacion = ObtenerDatosReservacion(reader);
                    Reservaciones.Add(reservacion);
                }

                reader.Close();
            }

            // Devuelve la vista con el modelo de reservaciones
            return View("BuscarPersona", Reservaciones);
        }

        // Método para cargar reservaciones según la cédula proporcionada
        private void CargarReservaciones(string cedulaIdentidad)
        {
            // Inicializa la lista de reservaciones
            Reservaciones = new List<string>();

            // Cadena de conexión a la base de datos
            string connectionString = "Server=ADSP-13207\\MSSQLSERVER01;Database=GestionHotelera;Trusted_Connection=True;TrustServerCertificate=true;";

            // Consulta para seleccionar reservaciones según la cédula proporcionada
            string queryString = "SELECT Nombre, PrimerApellido, SegundoApellido, CedulaIdentidad, Nacionalidad, Telefono, CorreoElectronico, NombreHotel, Torre, Piso, NumeroHabitacion FROM Reservaciones WHERE CedulaIdentidad = @CedulaIdentidad";

            // Uso de la conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@CedulaIdentidad", cedulaIdentidad);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Lectura de resultados y almacenamiento en la lista de reservaciones
                while (reader.Read())
                {
                    string reservacion = ObtenerDatosReservacion(reader);
                    Reservaciones.Add(reservacion);
                }

                reader.Close();
            }
        }

        // Método para obtener datos de una reservación en formato de cadena
        private string ObtenerDatosReservacion(SqlDataReader reader)
        {
            string nombre = reader["Nombre"].ToString();
            string primerApellido = reader["PrimerApellido"].ToString();
            string segundoApellido = reader["SegundoApellido"].ToString();
            string cedulaId = reader["CedulaIdentidad"].ToString();
            string nacionalidad = reader["Nacionalidad"].ToString();
            string telefono = reader["Telefono"].ToString();
            string correoElectronico = reader["CorreoElectronico"].ToString();
            string nombreHotel = reader["NombreHotel"].ToString();
            string torre = reader["Torre"].ToString();
            string piso = reader["Piso"].ToString();
            string numeroHabitacion = reader["NumeroHabitacion"].ToString();

            return $"{nombre}, {primerApellido}, {segundoApellido}, {cedulaId}, {nacionalidad}, {telefono}, {correoElectronico}, {nombreHotel}, {torre}, {piso}, {numeroHabitacion}";
        }

        // Método para validar la longitud de la cédula
        private bool ValidarCedula(string cedula)
        {
            return cedula.Length == 11;
        }
    }
}
