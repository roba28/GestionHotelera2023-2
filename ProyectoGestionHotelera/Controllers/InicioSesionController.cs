// Espacios de nombres necesarios
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoGestionHotelera.Models;
using System.Data.SqlClient;

// Espacio de nombres del controlador
namespace ProyectoGestionHotelera.Controllers
{
    // Definición de la clase InicioSesionController que hereda de Controller
    public class InicioSesionController : Controller
    {
        // Acción para mostrar la vista de inicio de sesión
        public ActionResult Index()
        {
            return View();
        }

        // Acción para mostrar detalles (no implementada)
        public ActionResult Details(int id)
        {
            return View();
        }

        // Acción para mostrar la vista de creación (no implementada)
        public ActionResult Create()
        {
            return View();
        }

        // Acción para procesar la creación (no implementada)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // Acción para mostrar la vista de edición (no implementada)
        public ActionResult Edit(int id)
        {
            return View();
        }

        // Acción para procesar la edición (no implementada)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // Acción para mostrar la vista de eliminación (no implementada)
        public ActionResult Delete(int id)
        {
            return View();
        }

        // Acción para procesar la eliminación (no implementada)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // Acción para validar el inicio de sesión
        [HttpPost]
        public IActionResult ValidarInicioSesion(string usuario, string contrasena, string rol)
        {
            // Cadena de conexión a la base de datos
            string connectionString = "Server=ADSP-13207\\MSSQLSERVER01;Database=GestionHotelera;Trusted_Connection=True;TrustServerCertificate=true;";

            // Uso de la conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Consulta para validar el inicio de sesión
                string Query = "SELECT rol, Nombre, Cedula, PrimerApellido, SegundoApellido, Nacionalidad, Telefono, CorreoElectronico, Tipo FROM Usuarios " +
                               "WHERE cedula = @usuario AND Contrasena = @contrasena AND rol = @rol";

                // Verificación de campos no nulos
                if (usuario != null && contrasena != null && rol != null)
                {
                    using (SqlCommand cmd = new SqlCommand(Query, connection))
                    {
                        // Asignación de parámetros
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@contrasena", contrasena);
                        cmd.Parameters.AddWithValue("@rol", rol);

                        using (SqlDataReader primeraHabitacionReader = cmd.ExecuteReader())
                        {
                            // Verificación de resultados y almacenamiento de datos de usuario
                            if (primeraHabitacionReader.Read())
                            {
                                TempData["Nombre"] = primeraHabitacionReader["Nombre"].ToString();
                                TempData["Cedula"] = primeraHabitacionReader["Cedula"].ToString();
                                TempData["rol"] = Convert.ToInt32(primeraHabitacionReader["rol"]);
                                Usuario.Cedula = primeraHabitacionReader["Cedula"].ToString();
                                Usuario.Nombre = primeraHabitacionReader["Nombre"].ToString();
                                Usuario.PrimerApellido = primeraHabitacionReader["PrimerApellido"].ToString();
                                Usuario.SegundoApellido = primeraHabitacionReader["SegundoApellido"].ToString();
                                Usuario.Nacionalidad = primeraHabitacionReader["Nacionalidad"].ToString();
                                Usuario.Telefono = primeraHabitacionReader["Telefono"].ToString();
                                Usuario.CorreoElectronico = primeraHabitacionReader["CorreoElectronico"].ToString();
                                Usuario.Rol = Convert.ToBoolean(primeraHabitacionReader["rol"]);
                                Usuario.Tipo = Convert.ToInt32(primeraHabitacionReader["Tipo"]);
                                primeraHabitacionReader.Close();
                            }
                        }

                        // Verificación de usuario encontrado
                        if (TempData["Nombre"] != null)
                        {
                            if (Convert.ToInt32(TempData["rol"]) == 0 || Convert.ToInt32(TempData["rol"]) == 1)
                            {
                                TempData["Mensaje"] = "";
                                TempData["Tipo"] = "Ok";
                                // Redirección según el rol del usuario
                                return RedirectToAction("IndexAdmin", "Home");
                            }
                            else if (Convert.ToInt32(TempData["rol"]) == 1)
                            {
                                TempData["Mensaje"] = "";
                                TempData["Tipo"] = "Ok";
                                // Redirección según el rol del usuario
                                return RedirectToAction("Index", "Home");
                            }
                        }
                        else
                        {
                            // La habitación no está disponible, muestra una alerta
                            TempData["Mensaje"] = "No se encuentra un usuario con esas características de inicio de sesión ";
                            TempData["Tipo"] = "error";
                            return RedirectToAction("Index");
                        }
                    }
                    return null;
                }
                else
                {
                    TempData["Mensaje"] = "Debe completar todos los campos";
                    TempData["Tipo"] = "error";
                    return RedirectToAction("Index");
                }

            }
        }
    }
}
