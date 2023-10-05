using EcommerceNET.DTO;
using EcommerceNET.WebAssembly.Services.Contract;
using System.Net.Http.Json;

namespace EcommerceNET.WebAssembly.Services.Implements
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO<bool>> Delete(int id)
        {
            return await _httpClient.DeleteFromJsonAsync<ResponseDTO<bool>>($"Category/Delete/{id}");
        }

        public async Task<ResponseDTO<CategoriaDTO>> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<CategoriaDTO>>($"Category/Get/{id}");
        }

        public async Task<ResponseDTO<CategoriaDTO>> Insert(CategoriaDTO model)
        {
            var response = await _httpClient.PostAsJsonAsync("Category/Insert", model);
            var result = await response.Content.ReadFromJsonAsync<ResponseDTO<CategoriaDTO>>();
            return result!;
        }

        public async Task<ResponseDTO<List<CategoriaDTO>>> ListCategory(string searh)
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<List<CategoriaDTO>>>($"Category/List/{searh}");
        }

        public async Task<ResponseDTO<bool>> Update(CategoriaDTO model)
        {
            var response = await _httpClient.PutAsJsonAsync("Category/Update", model);
            var result = await response.Content.ReadFromJsonAsync<ResponseDTO<bool>>();
            return result!;
        }
    }
}
