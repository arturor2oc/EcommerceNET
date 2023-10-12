using EcommerceNET.DTO;

namespace EcommerceNET.WebAssembly.Services.Contract
{
    public interface ICategoryService
    {
        Task<ResponseDTO<List<CategoriaDTO>>> ListCategory(string searh);
        Task<ResponseDTO<CategoriaDTO>> Get(int id);
        Task<ResponseDTO<CategoriaDTO>> Insert(CategoriaDTO model);
        Task<ResponseDTO<bool>> Update(CategoriaDTO model);
        Task<ResponseDTO<bool>> Delete(int id);
    }
}
