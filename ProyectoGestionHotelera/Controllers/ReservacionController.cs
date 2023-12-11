
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;


namespace ProyectoGestionHotelera.Controllers
{
    // Definición de la clase ReservacionController que hereda de Controller
    public class ReservacionController : Controller
    {
        // Acción para mostrar la vista principal de reservaciones
        public IActionResult Reservacion()
        {
            return View();
        }

        // Acción de reserva automática (POST)
        [HttpPost]
        public IActionResult ReservaAutomatica(string nombre, string primerApellido, string segundoApellido, string cedulaIdentidad, string nacionalidad, string telefono, string correoElectronico, string nombreHotel)
        {
            // Cadena de conexión a la base de datos
            string connectionString = "Server=ADSP-13207\\MSSQLSERVER01;Database=GestionHotelera;Trusted_Connection=True;TrustServerCertificate=true;";

            if (!string.IsNullOrEmpty(nombreHotel))
            {
                // Uso de la conexión a la base de datos
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Consulta para obtener la primera habitación disponible en el hotel especificado
                    string primeraHabitacionDisponibleQuery = "SELECT TOP 1 Torre, Piso, NumeroHabitacion FROM Habitaciones " +
                                                               "WHERE Nombre = @NombreHotel AND Disponibilidad = 1 " +
                                                               "ORDER BY Torre, Piso, NumeroHabitacion";

                    // Uso de un comando para ejecutar la consulta
                    using (SqlCommand primeraHabitacionCommand = new SqlCommand(primeraHabitacionDisponibleQuery, connection))
                    {
                        primeraHabitacionCommand.Parameters.AddWithValue("@NombreHotel", nombreHotel);

                        // Uso de un lector de datos para obtener resultados de la consulta
                        using (SqlDataReader primeraHabitacionReader = primeraHabitacionCommand.ExecuteReader())
                        {
                            if (primeraHabitacionReader.Read())
                            {
                                // Obtener detalles de la habitación disponible
                                string torre = primeraHabitacionReader["Torre"].ToString();
                                string piso = primeraHabitacionReader["Piso"].ToString();
                                string numeroHabitacion = primeraHabitacionReader["NumeroHabitacion"].ToString();

                                // Consulta para insertar la reserva en la tabla Reservaciones
                                string insertQuery = "INSERT INTO Reservaciones (Nombre, PrimerApellido, SegundoApellido, CedulaIdentidad, Nacionalidad, Telefono, CorreoElectronico, NombreHotel, Torre, Piso, NumeroHabitacion) " +
                                                    "VALUES (@Nombre, @PrimerApellido, @SegundoApellido, @CedulaIdentidad, @Nacionalidad, @Telefono, @CorreoElectronico, @NombreHotel, @Torre, @Piso, @NumeroHabitacion)";

                                // Uso de otro comando para ejecutar la inserción
                                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                                {
                                    // Parámetros para la inserción
                                    command.Parameters.AddWithValue("@Nombre", nombre);
                                    command.Parameters.AddWithValue("@PrimerApellido", primerApellido);
                                    command.Parameters.AddWithValue("@SegundoApellido", segundoApellido);
                                    command.Parameters.AddWithValue("@CedulaIdentidad", cedulaIdentidad);
                                    command.Parameters.AddWithValue("@Nacionalidad", nacionalidad);
                                    command.Parameters.AddWithValue("@Telefono", telefono);
                                    command.Parameters.AddWithValue("@CorreoElectronico", correoElectronico);
                                    command.Parameters.AddWithValue("@NombreHotel", nombreHotel);
                                    command.Parameters.AddWithValue("@Torre", torre);
                                    command.Parameters.AddWithValue("@Piso", piso);
                                    command.Parameters.AddWithValue("@NumeroHabitacion", numeroHabitacion);

                                    // Ejecutar la inserción
                                    command.ExecuteNonQuery();
                                }

                                // Consulta para actualizar la disponibilidad de la habitación a no disponible (0)
                                string updateDisponibilidadQuery = "UPDATE Habitaciones " +
                                                                   "SET Disponibilidad = 0 " +
                                                                   "WHERE Nombre = @NombreHotel AND Torre = @Torre AND Piso = @Piso AND NumeroHabitacion = @NumeroHabitacion";

                                // Otro comando para ejecutar la actualización de disponibilidad
                                using (SqlCommand updateCommand = new SqlCommand(updateDisponibilidadQuery, connection))
                                {
                                    // Parámetros para la actualización
                                    updateCommand.Parameters.AddWithValue("@NombreHotel", nombreHotel);
                                    updateCommand.Parameters.AddWithValue("@Torre", torre);
                                    updateCommand.Parameters.AddWithValue("@Piso", piso);
                                    updateCommand.Parameters.AddWithValue("@NumeroHabitacion", numeroHabitacion);

                                    // Ejecutar la actualización
                                    updateCommand.ExecuteNonQuery();
                                }

                                // Mensaje de éxito
                                TempData["Mensaje"] = "Reserva guardada con éxito";
                                TempData["Tipo"] = "Ok";
                            }
                            else
                            {
                                // No hay habitaciones disponibles en el hotel especificado
                                TempData["Mensaje"] = "Lo sentimos, no hay habitaciones disponibles en este momento en el hotel " + nombreHotel;
                                TempData["Tipo"] = "error";

                                // Redireccionar a la acción de reserva automática
                                return RedirectToAction("ReservacionAutomatica");
                            }
                        }
                    }
                }
            }

            // Redireccionar a la acción de reserva automática
            return RedirectToAction("ReservacionAutomatica");
        }

        // Acción para mostrar la vista de reserva automática
        public IActionResult ReservacionAutomatica()
        {
            return View();
        }

        // Acción para mostrar la vista de reserva manual
        public IActionResult ReservacionManual()
        {
            return View();
        }

        // Acción de reserva manual (POST)
        [HttpPost]
        public IActionResult ReservaManual(string nombre, string primerApellido, string segundoApellido, string cedulaIdentidad, string nacionalidad, string telefono, string correoElectronico, string nombreHotel, string torre, string piso, string numeroHabitacion)
        {
            // Cadena de conexión a la base de datos
            string connectionString = "Server=ADSP-13207\\MSSQLSERVER01;Database=GestionHotelera;Trusted_Connection=True;TrustServerCertificate=true;";

            if (!string.IsNullOrEmpty(nombreHotel))
            {
                // Uso de la conexión a la base de datos
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Consulta para verificar la disponibilidad de la habitación
                    string disponibilidadQuery = "SELECT Disponibilidad FROM Habitaciones " +
                                                 "WHERE Nombre = @NombreHotel AND Torre = @Torre AND Piso = @Piso AND NumeroHabitacion = @NumeroHabitacion";

                    // Uso de un comando para ejecutar la consulta
                    using (SqlCommand disponibilidadCommand = new SqlCommand(disponibilidadQuery, connection))
                    {
                        // Parámetros para la consulta
                        disponibilidadCommand.Parameters.AddWithValue("@NombreHotel", nombreHotel);
                        disponibilidadCommand.Parameters.AddWithValue("@Torre", int.Parse(torre));
                        disponibilidadCommand.Parameters.AddWithValue("@Piso", int.Parse(piso));
                        disponibilidadCommand.Parameters.AddWithValue("@NumeroHabitacion", int.Parse(numeroHabitacion));

                        // Obtener el resultado de la consulta
                        object disponibilidadResult = disponibilidadCommand.ExecuteScalar();

                        if (disponibilidadResult != null && disponibilidadResult != DBNull.Value)
                        {
                            int disponibilidad = Convert.ToInt32(disponibilidadResult);

                            if (disponibilidad == 1)
                            {
                                // La habitación está disponible, procede con la reservación

                                // Consulta para insertar la reserva en la tabla Reservaciones
                                string insertQuery = "INSERT INTO Reservaciones (Nombre, PrimerApellido, SegundoApellido, CedulaIdentidad, Nacionalidad, Telefono, CorreoElectronico, NombreHotel, Torre, Piso, NumeroHabitacion) " +
                                                    "VALUES (@Nombre, @PrimerApellido, @SegundoApellido, @CedulaIdentidad, @Nacionalidad, @Telefono, @CorreoElectronico, @NombreHotel, @Torre, @Piso, @NumeroHabitacion)";

                                // Uso de otro comando para ejecutar la inserción
                                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                                {
                                    // Parámetros para la inserción
                                    command.Parameters.AddWithValue("@Nombre", nombre);
                                    command.Parameters.AddWithValue("@PrimerApellido", primerApellido);
                                    command.Parameters.AddWithValue("@SegundoApellido", segundoApellido);
                                    command.Parameters.AddWithValue("@CedulaIdentidad", cedulaIdentidad);
                                    command.Parameters.AddWithValue("@Nacionalidad", nacionalidad);
                                    command.Parameters.AddWithValue("@Telefono", telefono);
                                    command.Parameters.AddWithValue("@CorreoElectronico", correoElectronico);
                                    command.Parameters.AddWithValue("@NombreHotel", nombreHotel);
                                    command.Parameters.AddWithValue("@Torre", int.Parse(torre));
                                    command.Parameters.AddWithValue("@Piso", int.Parse(piso));
                                    command.Parameters.AddWithValue("@NumeroHabitacion", int.Parse(numeroHabitacion));

                                    // Ejecutar la inserción
                                    command.ExecuteNonQuery();
                                }

                                // Consulta para actualizar la disponibilidad de la habitación a no disponible (0)
                                string updateDisponibilidadQuery = "UPDATE Habitaciones " +
                                                                   "SET Disponibilidad = 0 " +
                                                                   "WHERE Nombre = @NombreHotel AND Torre = @Torre AND Piso = @Piso AND NumeroHabitacion = @NumeroHabitacion";

                                // Otro comando para ejecutar la actualización de disponibilidad
                                using (SqlCommand updateCommand = new SqlCommand(updateDisponibilidadQuery, connection))
                                {
                                    // Parámetros para la actualización
                                    updateCommand.Parameters.AddWithValue("@NombreHotel", nombreHotel);
                                    updateCommand.Parameters.AddWithValue("@Torre", int.Parse(torre));
                                    updateCommand.Parameters.AddWithValue("@Piso", int.Parse(piso));
                                    updateCommand.Parameters.AddWithValue("@NumeroHabitacion", int.Parse(numeroHabitacion));

                                    // Ejecutar la actualización
                                    updateCommand.ExecuteNonQuery();
                                }

                                // Mensaje de éxito
                                TempData["Mensaje"] = "Reserva guardada con éxito";
                                TempData["Tipo"] = "Ok";
                            }
                            else
                            {
                                // La habitación no está disponible, muestra una alerta
                                TempData["Mensaje"] = "Lo sentimos, la habitación no está disponible en este momento.";
                                TempData["Tipo"] = "error";

                                // Redireccionar a la acción de reserva manual
                                return RedirectToAction("ReservacionManual");
                            }
                        }
                        else
                        {
                            // Error al obtener la disponibilidad
                        }
                    }
                }
            }

            // Redireccionar a la acción de reserva manual
            return RedirectToAction("ReservacionManual");
        }
    }
}
