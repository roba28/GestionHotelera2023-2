using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProyectoGestionHotelera.Controllers
{
    public class ReservaEliminarController : Controller
    {
        // Lista para almacenar información de reservaciones
        public List<string> Reservaciones { get; set; }

        // Acción para mostrar la vista principal de eliminación de reservaciones
        public IActionResult EliminarReservacion()
        {
            return View();
        }

        // Acción para eliminar reservaciones de una persona por hotel (POST)
        [HttpPost]
        public IActionResult EliminarReservacionPersonaPorHotel(string cedulaIdentidad, string hotel)
        {
            // Validar la cédula
            if (!ValidarCedula(cedulaIdentidad))
            {
                ModelState.AddModelError(string.Empty, "La cédula de identidad no cumple con los requisitos.");
                CargarReservaciones();
                return View("EliminarReservacion");
            }

            // Cadena de conexión a la base de datos
            string connectionString = "Server=ADSP-13207\\MSSQLSERVER01;Database=GestionHotelera;Trusted_Connection=True;TrustServerCertificate=true;";

            // Consulta para eliminar la reservación
            string deleteQuery = "DELETE FROM Reservaciones WHERE CedulaIdentidad = @CedulaIdentidad AND NombreHotel = @NombreHotel";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@CedulaIdentidad", cedulaIdentidad);
                    command.Parameters.AddWithValue("@NombreHotel", hotel);

                    // Ejecutar la eliminación
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        TempData["Mensaje"] = "La reservación fue eliminada con éxito";
                        TempData["Tipo"] = "Ok";
                        CargarReservaciones();
                    }
                    else
                    {
                        TempData["Mensaje"] = "No se encontraron reservaciones para la cédula y hotel especificados.";
                        TempData["Tipo"] = "error";
                        ModelState.AddModelError(string.Empty, "No se encontraron reservaciones para la cédula y hotel especificados.");
                        CargarReservaciones();
                    }
                }
            }

            return View("EliminarReservacion");
        }

        // Acción para eliminar todas las reservaciones (por hotel o todas) (POST)
        [HttpPost]
        public IActionResult EliminarTodasReservaciones(string hotel)
        {
            // Cadena de conexión a la base de datos
            string connectionString = "Server=ADSP-13207\\MSSQLSERVER01;Database=GestionHotelera;Trusted_Connection=True;TrustServerCertificate=true;";

            // Consulta para eliminar todas las reservaciones (o por hotel específico)
            string deleteQuery = (hotel.ToLower() == "todos")
                ? "DELETE FROM Reservaciones"
                : "DELETE FROM Reservaciones WHERE NombreHotel = @NombreHotel";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    if (hotel.ToLower() != "todos")
                    {
                        command.Parameters.AddWithValue("@NombreHotel", hotel);
                    }

                    // Ejecutar la eliminación
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        TempData["Mensaje"] = "Las reservaciones fueron eliminadas con éxito";
                        TempData["Tipo"] = "Ok";
                        CargarReservaciones();
                    }
                    else
                    {
                        TempData["Mensaje"] = "No se encontraron reservaciones para este hotel";
                        TempData["Tipo"] = "error";
                        ModelState.AddModelError(string.Empty, "No se encontraron reservaciones para este hotel");
                        CargarReservaciones();
                    }
                }
            }

            return View("EliminarReservacion");
        }

        // Acción para eliminar todas las reservaciones de una persona (POST)
        [HttpPost]
        public IActionResult EliminarTodasReservacionesPorPersona(string cedulaIdentidad)
        {
            // Validar la cédula
            if (!ValidarCedula(cedulaIdentidad))
            {
                ModelState.AddModelError(string.Empty, "La cédula de identidad no cumple con los requisitos.");
                CargarReservaciones();
                return View("EliminarReservacion");
            }

            // Cadena de conexión a la base de datos
            string connectionString = "Server=ADSP-13207\\MSSQLSERVER01;Database=GestionHotelera;Trusted_Connection=True;TrustServerCertificate=true;";

            // Consulta para eliminar todas las reservaciones de una persona
            string deleteQuery = "DELETE FROM Reservaciones WHERE CedulaIdentidad = @CedulaIdentidad";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@CedulaIdentidad", cedulaIdentidad);

                    // Ejecutar la eliminación
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        TempData["Mensaje"] = "Las reservaciones fueron eliminadas con éxito";
                        TempData["Tipo"] = "Ok";
                        CargarReservaciones();
                    }
                    else
                    {
                        TempData["Mensaje"] = "No se encontraron reservaciones para la cédula especificada.";
                        TempData["Tipo"] = "error";
                        ModelState.AddModelError(string.Empty, "No se encontraron reservaciones para la cédula especificada.");
                        CargarReservaciones();
                    }
                }
            }

            return View("EliminarReservacion");
        }

        // Acción para cargar las reservaciones y mostrarlas en la vista
        public IActionResult CargarReservaciones()
        {
            Reservaciones = new List<string>();

            // Cadena de conexión a la base de datos
            string connectionString = "Server=ADSP-13207\\MSSQLSERVER01;Database=GestionHotelera;Trusted_Connection=True;TrustServerCertificate=true;";

            // Consulta para obtener todas las reservaciones
            string queryString = "SELECT Nombre, PrimerApellido, SegundoApellido, CedulaIdentidad, Nacionalidad, Telefono, CorreoElectronico, NombreHotel, Torre, Piso, NumeroHabitacion FROM Reservaciones";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Uso de un comando para ejecutar la consulta
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Leer los resultados y agregarlos a la lista de reservaciones
                while (reader.Read())
                {
                    string nombre = reader["Nombre"].ToString();
                    string primerApellido = reader["PrimerApellido"].ToString();
                    string segundoApellido = reader["SegundoApellido"].ToString();
                    string cedulaIdentidad = reader["CedulaIdentidad"].ToString();
                    string nacionalidad = reader["Nacionalidad"].ToString();
                    string telefono = reader["Telefono"].ToString();
                    string correoElectronico = reader["CorreoElectronico"].ToString();
                    string nombreHotel = reader["NombreHotel"].ToString();
                    string torre = reader["Torre"].ToString();
                    string piso = reader["Piso"].ToString();
                    string numeroHabitacion = reader["NumeroHabitacion"].ToString();

                    string reservacion = $"{nombre}, {primerApellido}, {segundoApellido}, {cedulaIdentidad}, {nacionalidad}, {telefono}, {correoElectronico}, {nombreHotel}, {torre}, {piso}, {numeroHabitacion}";
                    Reservaciones.Add(reservacion);
                }

                reader.Close();
            }

            return View("EliminarReservacion", Reservaciones);
        }

        // Función para validar la longitud de la cédula
        private bool ValidarCedula(string cedula)
        {
            return cedula.Length == 11;
        }
    }
}
