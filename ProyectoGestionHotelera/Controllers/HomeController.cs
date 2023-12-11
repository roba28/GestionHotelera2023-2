// Importación de espacios de nombres necesarios
using Microsoft.AspNetCore.Mvc;
using ProyectoGestionHotelera.Models;
using System.Diagnostics;

// Espacio de nombres del controlador
namespace ProyectoGestionHotelera.Controllers
{
    // Definición de la clase HomeController que hereda de Controller
    public class HomeController : Controller
    {
        // Instancia privada del logger para el controlador
        private readonly ILogger<HomeController> _logger;

        // Constructor de la clase HomeController que recibe un logger como parámetro
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Acción para la vista de inicio del administrador
        public IActionResult IndexAdmin()
        {
            // Retorna la vista correspondiente
            return View();
        }

        // Acción para la vista de inicio
        public IActionResult Index()
        {
            // Retorna la vista correspondiente
            return View();
        }

        // Acción para la vista de privacidad
        public IActionResult Privacy()
        {
            // Retorna la vista correspondiente
            return View();
        }

        // Acción para la vista de hotel
        public IActionResult hotel()
        {
            // Retorna la vista correspondiente
            return View();
        }

        // Acción para manejar errores, con caché de respuesta desactivada
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Retorna la vista de error con un modelo que contiene el ID de la solicitud
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
