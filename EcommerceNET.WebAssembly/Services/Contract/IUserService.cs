using EcommerceNET.DTO;
namespace EcommerceNET.WebAssembly.Services.Contract
{
    public interface IUserService
    {
        Task<ResponseDTO<List<UserDTO>>> ListUser(string rol, string searh);
        Task<ResponseDTO<UserDTO>> Get(int id);
        Task<ResponseDTO<SesionDTO>> Authorization(LoginDTO model);
        Task<ResponseDTO<UserDTO>> Insert(UserDTO model);
        Task<ResponseDTO<bool>> Update(UserDTO model);
        Task<ResponseDTO<bool>> Delete(int id);
    }
}
