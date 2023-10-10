using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using EcommerceNET.Service.Contract;
using EcommerceNET.DTO;
using EcommerceNET.Service.Implements;

namespace EcommerceNET.API.Controllers
{
    /// <summary>
    /// Controlador de categoria 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        /// <summary>
        /// Constructor de la clase CategoryController.
        /// </summary>
        /// <param name="categoryService">Una instancia de ICategoryService que proporciona funcionalidad relacionada con categorías.</param>
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Obtiene una lista de categorías con la posibilidad de aplicar un filtro de búsqueda.
        /// </summary>
        /// <param name="searh">Un parámetro opcional que permite filtrar las categorías por un término de búsqueda.</param>
        /// <returns>Un IActionResult que contiene una respuesta JSON con una lista de categorías o un mensaje de error en caso de excepción.</returns>
        [HttpGet("List/{searh?}")]
        public async Task<IActionResult> List(string searh = "NA")
        {
            // Crear una instancia de ResponseDTO para almacenar la respuesta.
            var response = new ResponseDTO<List<CategoriaDTO>>();

            try
            {
                // Si el parámetro de búsqueda es "NA", establecerlo como una cadena vacía.
                if (searh == "NA") searh = "";

                response.EsCorrecto = true;
                // Llamar al método ListCategory del servicio de categorías para obtener la lista de categorías.
                response.Resultado = await _categoryService.ListCategory(searh);

            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }
            // Devolver una respuesta HTTP 200 (OK) que contiene la respuesta serializada en formato JSON.
            return Ok(response);
        }
        /// <summary>
        /// Obtiene una categoría por su identificador.
        /// </summary>
        /// <param name="id">Identificador único de la categoría que se desea obtener.</param>
        /// <returns>Un IActionResult que contiene una respuesta JSON con una categoría o un mensaje de error en caso de excepción.</returns>
        [HttpGet("Get/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            // Crear una instancia de ResponseDTO para almacenar la respuesta.
            var response = new ResponseDTO<CategoriaDTO>();

            try
            {
                response.EsCorrecto = true;
                // Llamar al método Get del servicio de categorías para obtener la categoría por su identificador.
                response.Resultado = await _categoryService.Get(id);

            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }
            // Devolver una respuesta HTTP 200 (OK) que contiene la respuesta serializada en formato JSON.
            return Ok(response);
        }
        /// <summary>
        /// Inserta una nueva categoría en el sistema.
        /// </summary>
        /// <param name="model">Un objeto CategoriaDTO que representa la nueva categoría a insertar.</param>
        /// <returns>Un IActionResult que contiene una respuesta JSON con la categoría recién insertada o un mensaje de error en caso de excepción.</returns>
        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] CategoriaDTO model)
        {
            // Crear una instancia de ResponseDTO para almacenar la respuesta.
            var response = new ResponseDTO<CategoriaDTO>();

            try
            {
                response.EsCorrecto = true;
                // Llamar al método Insert del servicio de categorías para insertar la nueva categoría.
                response.Resultado = await _categoryService.Insert(model);

            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }
            // Devolver una respuesta HTTP 200 (OK) que contiene la respuesta serializada en formato JSON.
            return Ok(response);
        }
        /// <summary>
        /// Actualiza una categoría existente en el sistema.
        /// </summary>
        /// <param name="model">Un objeto CategoriaDTO que representa la categoría a actualizar.</param>
        /// <returns>Un IActionResult que contiene una respuesta JSON con un valor booleano que indica si la actualización se realizó con éxito o un mensaje de error en caso de excepción.</returns>
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] CategoriaDTO model)
        {
            // Crear una instancia de ResponseDTO para almacenar la respuesta.
            var response = new ResponseDTO<bool>();

            try
            {
                response.EsCorrecto = true;
                // Llamar al método Update del servicio de categorías para actualizar la categoría.
                response.Resultado = await _categoryService.Update(model);

            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }
            // Devolver una respuesta HTTP 200 (OK) que contiene la respuesta serializada en formato JSON.
            return Ok(response);
        }
        /// <summary>
        /// Elimina una categoría existente en el sistema por su identificador.
        /// </summary>
        /// <param name="id">Identificador único de la categoría que se desea eliminar.</param>
        /// <returns>Un IActionResult que contiene una respuesta JSON con un valor booleano que indica si la eliminación se realizó con éxito o un mensaje de error en caso de excepción.</returns>
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Crear una instancia de ResponseDTO para almacenar la respuesta.
            var response = new ResponseDTO<bool>();

            try
            {
                response.EsCorrecto = true;
                // Llamar al método Delete del servicio de categorías para eliminar la categoría por su identificador.
                response.Resultado = await _categoryService.Delete(id);

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
