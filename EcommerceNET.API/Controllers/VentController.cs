using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using EcommerceNET.Service.Contract;
using EcommerceNET.DTO;
using EcommerceNET.Service.Implements;

namespace EcommerceNET.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentController : ControllerBase
    {
        private readonly IVentService _ventService;

        public VentController(IVentService ventService)
        {
            _ventService = ventService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] VentaDTO model)
        {
            var response = new ResponseDTO<VentaDTO>();

            try
            {
                response.EsCorrecto = true;
                response.Resultado = await _ventService.Create(model);

            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }
            return Ok(response);
        }
    }
}
