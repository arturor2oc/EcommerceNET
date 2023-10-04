using EcommerceNET.DTO;
namespace EcommerceNET.WebAssembly.Services.Contract
{
    public interface ICarritoService
    {
        event Action ShowItems;

        int ContProducts();
        Task AddCarrito(CarritoDTO model);
        Task DeleteCarrito(int id); 
        Task <List<CarritoDTO>> ReturnCarrito();
        Task ClearCarrito();
    }
}
