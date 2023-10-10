using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using EcommerceNET.Service.Contract;
using EcommerceNET.DTO;

namespace EcommerceNET.API.Controllers
{
    /// <summary>
    /// Controlador de Usuarios
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        /// <summary>
        /// Constructor de la clase UserController.
        /// </summary>
        /// <param name="userService">Una instancia de IUserService que proporciona funcionalidad relacionada con usuarios.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Obtiene una lista de usuarios por su rol con la posibilidad de aplicar un filtro de búsqueda alfabético opcional.
        /// </summary>
        /// <param name="rol">El rol de usuario por el que se desea filtrar.</param>
        /// <param name="searh">Un parámetro opcional que permite filtrar los usuarios por un término de búsqueda alfabético.</param>
        /// <returns>Un IActionResult que contiene una respuesta JSON con una lista de usuarios que coinciden con el rol y el filtro de búsqueda o un mensaje de error en caso de excepción.</returns>
        [HttpGet("List/{rol:alpha}/{searh:alpha?}")]
        public async Task<IActionResult> List(string rol, string searh = "NA")
        {
            var response = new ResponseDTO<List<UserDTO>>();

            try
            {
                if (searh == "NA") searh = "";

                response.EsCorrecto = true;
                // Llamar al método ListUser del servicio de usuarios para obtener la lista de usuarios por rol y filtro de búsqueda.
                response.Resultado = await _userService.ListUser(rol, searh);

            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un usuario por su identificador.
        /// </summary>
        /// <param name="id">Identificador único del usuario que se desea obtener.</param>
        /// <returns>Un IActionResult que contiene una respuesta JSON con un usuario o un mensaje de error en caso de excepción.</returns>
        [HttpGet("Get/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = new ResponseDTO<UserDTO>();

            try
            {
                response.EsCorrecto = true;
                response.Resultado = await _userService.Get(id);

            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }
            return Ok(response);
        }

        /// <summary>
        /// Inserta un nuevo usuario en el sistema.
        /// </summary>
        /// <param name="model">Un objeto UserDTO que representa el usuario a insertar.</param>
        /// <returns>Un IActionResult que contiene una respuesta JSON con el usuario insertado o un mensaje de error en caso de excepción.</returns>
        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] UserDTO model)
        {
            var response = new ResponseDTO<UserDTO>();

            try
            {
                response.EsCorrecto = true;
                response.Resultado = await _userService.Insert(model);

            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }
            return Ok(response);
        }

        /// <summary>
        /// Realiza la autorización de un usuario mediante el proceso de inicio de sesión.
        /// </summary>
        /// <param name="model">Un objeto LoginDTO que contiene las credenciales del usuario para la autorización.</param>
        /// <returns>Un IActionResult que contiene una respuesta JSON con los datos de sesión del usuario autorizado o un mensaje de error en caso de excepción.</returns>
        [HttpPost("Authorization")]
        public async Task<IActionResult> Authorization([FromBody] LoginDTO model)
        {
            var response = new ResponseDTO<SesionDTO>();

            try
            {
                response.EsCorrecto = true;
                response.Resultado = await _userService.Authorization(model);

            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }
            return Ok(response);
        }

        /// <summary>
        /// Actualiza la información de un usuario existente en el sistema.
        /// </summary>
        /// <param name="model">Un objeto UserDTO que representa los datos actualizados del usuario.</param>
        /// <returns>Un IActionResult que contiene una respuesta JSON con un valor booleano que indica si la actualización se realizó con éxito o un mensaje de error en caso de excepción.</returns>
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UserDTO model)
        {
            var response = new ResponseDTO<bool>();

            try
            {
                response.EsCorrecto = true;
                response.Resultado = await _userService.Update(model);

            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }
            return Ok(response);
        }

        /// <summary>
        /// Elimina un usuario existente del sistema por su identificador.
        /// </summary>
        /// <param name="id">Identificador único del usuario que se desea eliminar.</param>
        /// <returns>Un IActionResult que contiene una respuesta JSON con un valor booleano que indica si la eliminación se realizó con éxito o un mensaje de error en caso de excepción.</returns>
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = new ResponseDTO<bool>();

            try
            {
                response.EsCorrecto = true;
                response.Resultado = await _userService.Delete(id);

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
