using EcommerceNET.DTO;

namespace EcommerceNET.WebAssembly.Services.Contract
{
    public interface IDashboardService
    {
        Task<ResponseDTO<DashboardDTO>> Resum();

    }
}
