using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using EcommerceNET.Service.Contract;
using EcommerceNET.DTO;
using EcommerceNET.Service.Implements;

namespace EcommerceNET.API.Controllers
{
    /// <summary>
    /// Controlador de Ventas
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class VentController : ControllerBase
    {
        private readonly IVentService _ventService;

        /// <summary>
        /// Constructor de la clase VentController.
        /// </summary>
        /// <param name="ventService">Una instancia de IVentService que proporciona funcionalidad relacionada con ventas.</param>
        public VentController(IVentService ventService)
        {
            _ventService = ventService;
        }

        /// <summary>
        /// Crea una nueva venta en el sistema.
        /// </summary>
        /// <param name="model">Un objeto VentaDTO que representa los datos de la venta a crear.</param>
        /// <returns>Un IActionResult que contiene una respuesta JSON con los detalles de la venta creada o un mensaje de error en caso de excepción.</returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] VentaDTO model)
        {
            // Crear una instancia de ResponseDTO para almacenar la respuesta.
            var response = new ResponseDTO<VentaDTO>();

            try
            {
                response.EsCorrecto = true;
                // Llamar al método Create del servicio de ventas para crear una nueva venta.
                response.Resultado = await _ventService.Create(model);

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
