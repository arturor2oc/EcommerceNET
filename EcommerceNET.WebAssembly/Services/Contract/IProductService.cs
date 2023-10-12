using EcommerceNET.DTO;

namespace EcommerceNET.WebAssembly.Services.Contract
{
    public interface IProductService
    {
        Task<ResponseDTO<List<ProductoDTO>>> ListProduct(string searh);
        Task<ResponseDTO<List<ProductoDTO>>> Catalog(string category, string searh);
        Task<ResponseDTO<ProductoDTO>> Get(int id);
        Task<ResponseDTO<ProductoDTO>> Insert(ProductoDTO model);
        Task<ResponseDTO<bool>> Update(ProductoDTO model);
        Task<ResponseDTO<bool>> Delete(int id);
    }
}
