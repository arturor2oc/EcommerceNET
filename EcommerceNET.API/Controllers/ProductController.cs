using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using EcommerceNET.Service.Contract;
using EcommerceNET.DTO;
using EcommerceNET.Service.Implements;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.SqlServer.Server;
using static System.Net.WebRequestMethods;

namespace EcommerceNET.API.Controllers
{
    /// <summary>
    /// Controlador de Productos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        /// <summary>
        /// Constructor de la clase ProductController.
        /// </summary>
        /// <param name="productService">Una instancia de IProductService que proporciona funcionalidad relacionada con productos.</param>
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Obtiene una lista de productos con la posibilidad de aplicar un filtro de búsqueda.
        /// </summary>
        /// <param name="search">Un parámetro opcional que permite filtrar los productos por un término de búsqueda alfabético.</param>
        /// <returns>Un IActionResult que contiene una respuesta JSON con una lista de productos o un mensaje de error en caso de excepción.</returns>
        [HttpGet("List/{searh:alpha?}")]
        public async Task<IActionResult> List(string searh = "NA")
        {
            // Crear una instancia de ResponseDTO para almacenar la respuesta.
            var response = new ResponseDTO<List<ProductoDTO>>();

            try
            {
                if (searh == "NA") searh = "";

                response.EsCorrecto = true;
                // Llamar al método ListProduct del servicio de productos para obtener la lista de productos.
                response.Resultado = await _productService.ListProduct(searh);

            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }
            // Devolver una respuesta HTTP 200(OK) que contiene la respuesta serializada en formato JSON.
            return Ok(response);
        }

        /// <summary>
        /// Obtiene una lista de productos de un catálogo específico con la posibilidad de aplicar un filtro de búsqueda.
        /// </summary>
        /// <param name="category">El nombre de la categoría del catálogo de productos. Use "todos" para obtener todos los productos sin filtrar por categoría.</param>
        /// <param name="search">Un parámetro opcional que permite filtrar los productos por un término de búsqueda.</param>
        /// <returns>Un IActionResult que contiene una respuesta JSON con una lista de productos del catálogo o un mensaje de error en caso de excepción.</returns>
        [HttpGet("Catalog/{category}/{searh?}")]
        public async Task<IActionResult> Catalog(string category, string searh = "NA")
        {
            // Crear una instancia de ResponseDTO para almacenar la respuesta.
            var response = new ResponseDTO<List<ProductoDTO>>();

            try
            {
                // Si la categoría es "todos", establecerla como una cadena vacía.
                if (category.ToLower() == "todos") category = "";
                if (searh == "NA") searh = "";

                response.EsCorrecto = true;
                // Llamar al método Catalog del servicio de productos para obtener la lista de productos del catálogo.
                response.Resultado = await _productService.Catalog(category, searh);

            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }
            // En caso de excepción, establecer la propiedad EsCorrecto en false y registrar el mensaje de error.
            return Ok(response);
        }

        /// <summary>
        /// Obtiene un producto por su identificador.
        /// </summary>
        /// <param name="id">Identificador único del producto que se desea obtener.</param>
        /// <returns>Un IActionResult que contiene una respuesta JSON con un producto o un mensaje de error en caso de excepción.</returns>
        [HttpGet("Get/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = new ResponseDTO<ProductoDTO>();

            try
            {
                response.EsCorrecto = true;
                response.Resultado = await _productService.Get(id);

            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }
            return Ok(response);
        }

        /// <summary>
        /// Inserta un nuevo producto en el sistema.
        /// </summary>
        /// <param name="model">Un objeto ProductoDTO que representa el producto a insertar.</param>
        /// <returns>Un IActionResult que contiene una respuesta JSON con el producto insertado o un mensaje de error en caso de excepción.</returns>
        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] ProductoDTO model)
        {
            var response = new ResponseDTO<ProductoDTO>();

            try
            {
                response.EsCorrecto = true;
                // Llamar al método Insert del servicio de productos para insertar el nuevo producto.
                response.Resultado = await _productService.Insert(model);

            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }
            return Ok(response);
        }

        /// <summary>
        /// Actualiza un producto existente en el sistema.
        /// </summary>
        /// <param name="model">Un objeto ProductoDTO que representa el producto a actualizar.</param>
        /// <returns>Un IActionResult que contiene una respuesta JSON con un valor booleano que indica si la actualización se realizó con éxito o un mensaje de error en caso de excepción.</returns>
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] ProductoDTO model)
        {
            var response = new ResponseDTO<bool>();

            try
            {
                response.EsCorrecto = true;
                // Llamar al método Update del servicio de productos para actualizar el producto.
                response.Resultado = await _productService.Update(model);

            }
            catch (Exception ex)
            {
                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }
            return Ok(response);
        }

        /// <summary>
        /// Elimina un producto existente del sistema por su identificador.
        /// </summary>
        /// <param name="id">Identificador único del producto que se desea eliminar.</param>
        /// <returns>Un IActionResult que contiene una respuesta JSON con un valor booleano que indica si la eliminación se realizó con éxito o un mensaje de error en caso de excepción.</returns>
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = new ResponseDTO<bool>();

            try
            {
                response.EsCorrecto = true;
                // Llamar al método Delete del servicio de productos para eliminar el producto por su identificador.
                response.Resultado = await _productService.Delete(id);

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
