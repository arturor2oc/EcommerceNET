using EcommerceNET.DTO;
using EcommerceNET.WebAssembly.Services.Contract;
using System.Net.Http.Json;


namespace EcommerceNET.WebAssembly.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO<SesionDTO>> Authorization(LoginDTO model)
        {
            var response = await _httpClient.PostAsJsonAsync("User/Authorization", model);
            var result = await response.Content.ReadFromJsonAsync<ResponseDTO<SesionDTO>>();
            return result!;
        }

        public async Task<ResponseDTO<bool>> Delete(int id)
        {
            return await _httpClient.DeleteFromJsonAsync<ResponseDTO<bool>>($"User/Delete/{id}");
        }

        public async Task<ResponseDTO<UserDTO>> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<UserDTO>>($"User/Get/{id}");
        }

        public async Task<ResponseDTO<UserDTO>> Insert(UserDTO model)
        {
            var response = await _httpClient.PostAsJsonAsync("User/Insert", model);
            var result = await response.Content.ReadFromJsonAsync<ResponseDTO<UserDTO>>();
            return result!;
        }

        public async Task<ResponseDTO<List<UserDTO>>> ListUser(string rol, string searh)
        {
            return await _httpClient.GetFromJsonAsync<ResponseDTO<List<UserDTO>>>($"User/List/{rol}/{searh}");
        }

        public async Task<ResponseDTO<bool>> Update(UserDTO model)
        {
            var response = await _httpClient.PostAsJsonAsync("User/Update", model);
            var result = await response.Content.ReadFromJsonAsync<ResponseDTO<bool>>();
            return result!;
        }
    }
}
