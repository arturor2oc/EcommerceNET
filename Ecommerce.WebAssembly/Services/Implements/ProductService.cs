
using EcommerceNET.DTO;
using EcommerceNET.WebAssembly.Services.Contract;
using System.Net.Http.Json;

namespace EcommerceNET.WebAssembly.Services.Implements
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO<List<ProductoDTO>>> Catalog(string category, string searh)
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<List<ProductoDTO>>>($"Product/Catalog/{category}/{searh}");
        }

        public async Task<ResponseDTO<bool>> Delete(int id)
        {
            return await _httpClient.DeleteFromJsonAsync<ResponseDTO<bool>>($"Product/Delete/{id}");
        }

        public async Task<ResponseDTO<ProductoDTO>> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<ProductoDTO>>($"Product/Get/{id}");
        }

        public async Task<ResponseDTO<ProductoDTO>> Insert(ProductoDTO model)
        {
            var response = await _httpClient.PostAsJsonAsync("Product/Insert", model);
            var result = await response.Content.ReadFromJsonAsync<ResponseDTO<ProductoDTO>>();
            return result!;
        }

        public async Task<ResponseDTO<List<ProductoDTO>>> ListProduct(string searh)
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<List<ProductoDTO>>>($"Product/List/{searh}");
        }

        public async Task<ResponseDTO<bool>> Update(ProductoDTO model)
        {
            var response = await _httpClient.PutAsJsonAsync("Product/Update", model);
            var result = await response.Content.ReadFromJsonAsync<ResponseDTO<bool>>();
            return result!;
        }
    }
}
