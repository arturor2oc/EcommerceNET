using EcommerceNET.DTO;
using EcommerceNET.WebAssembly.Services.Contract;
using System.Net.Http.Json;

namespace EcommerceNET.WebAssembly.Services.Implements
{
    public class DashboardService : IDashboardService
    {
        private readonly HttpClient _httpClient;

        public DashboardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO<DashboardDTO>> Resum()
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<DashboardDTO>>($"Dashboard/Resum");
        }
    }
}
