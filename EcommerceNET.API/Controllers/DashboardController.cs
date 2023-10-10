using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using EcommerceNET.Service.Contract;
using EcommerceNET.DTO;
using EcommerceNET.Service.Implements;

namespace EcommerceNET.API.Controllers
{
    /// <summary>
    /// Controlador del Dashboard (Panel de Control)
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        /// <summary>
        /// Constructor de la clase DashboardController.
        /// </summary>
        /// <param name="dashboardService">Una instancia de IDashboardService que proporciona funcionalidad relacionada con el panel de control.</param>
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        /// <summary>
        /// Obtiene un resumen de información del panel de control.
        /// </summary>
        /// <returns>Un IActionResult que contiene una respuesta JSON con un resumen de información del panel de control o un mensaje de error en caso de excepción.</returns>
        [HttpGet("Resum")]
        public IActionResult Resum()
        {
            // Crear una instancia de ResponseDTO para almacenar la respuesta.
            var response = new ResponseDTO<DashboardDTO>();

            try
            {
                response.EsCorrecto = true;
                // Llamar al método Resum del servicio de panel de control para obtener un resumen de información.
                response.Resultado = _dashboardService.Resum();

            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }
            // Devolver una respuesta HTTP 200 (OK) que contiene la respuesta serializada en formato JSON.
            return Ok(response);
        }
    }
}
