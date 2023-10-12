using EcommerceNET.DTO;
using EcommerceNET.WebAssembly.Services.Contract;
using System.Net.Http.Json;

namespace EcommerceNET.WebAssembly.Services.Implements
{
    public class VentService : IVentService
    {
        private readonly HttpClient _httpClient;

        public VentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO<VentaDTO>> Create(VentaDTO model)
        {
            var response = await _httpClient.PostAsJsonAsync("Vent/Create", model);
            var result = await response.Content.ReadFromJsonAsync<ResponseDTO<VentaDTO>>();
            return result!;
        }
    }
}
