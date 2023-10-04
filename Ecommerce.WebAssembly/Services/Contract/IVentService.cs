using EcommerceNET.DTO;

namespace EcommerceNET.WebAssembly.Services.Contract
{
    public interface IVentService
    {
        Task<ResponseDTO<VentaDTO>> Create(VentaDTO model);
    }
}
