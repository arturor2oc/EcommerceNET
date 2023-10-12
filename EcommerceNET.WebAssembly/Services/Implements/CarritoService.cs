using Blazored.LocalStorage;
using Blazored.Toast.Services;
using EcommerceNET.DTO;
using EcommerceNET.WebAssembly.Services.Contract;

namespace EcommerceNET.WebAssembly.Services.Implements
{
    public class CarritoService : ICarritoService
    {
        private ILocalStorageService _localStorageService;
        private ISyncLocalStorageService _syncLocalStorageService;
        private IToastService _toastService;
        public CarritoService(
            ILocalStorageService localStorageService,
            ISyncLocalStorageService syncLocalStorageService,
            IToastService toastService
            )
        {
            _localStorageService = localStorageService;
            _syncLocalStorageService = syncLocalStorageService;
            _toastService = toastService;

        }

        public event Action ShowItems;

        public async Task AddCarrito(CarritoDTO model)
        {
            try
            {
                var carrito = await _localStorageService.GetItemAsync<List<CarritoDTO>>("carrito");
                if (carrito == null)
                    carrito = new List<CarritoDTO>();

                var encontrado = carrito.FirstOrDefault(c => c.Producto.IdProducto == model.Producto.IdProducto);
                if (encontrado != null)
                    carrito.Remove(encontrado);
                carrito.Add(model);
                await _localStorageService.SetItemAsync("carrito", carrito);

                if (encontrado != null)
                    _toastService.ShowSuccess("Producto fue actualizado en carrito");

                else
                    _toastService.ShowSuccess("Producto fue agregado al carrito");

                ShowItems.Invoke();
            }
            catch (Exception ex)
            {
                _toastService.ShowError("No se pudo agregar al carrito");
            }
        }

        public async Task ClearCarrito()
        {
            await _localStorageService.RemoveItemAsync("carrito");
            ShowItems.Invoke();
        }

        public int ContProducts()
        {
            var carrito = _syncLocalStorageService.GetItem<List<CarritoDTO>>("carrito");
            return carrito == null ? 0 : carrito.Count();
        }

        public async Task DeleteCarrito(int idProducto)
        {
            try
            {
                var carrito = await _localStorageService.GetItemAsync<List<CarritoDTO>>("carrito");
                if (carrito != null)
                {
                    var element = carrito.FirstOrDefault(c => c.Producto.IdProducto == idProducto);
                    if (element != null)
                    {
                        carrito.Remove(element);
                        await _localStorageService.SetItemAsync("carrito", carrito);
                        ShowItems.Invoke();
                    }

                }
            }
            catch
            {

            }
        }

        public async Task<List<CarritoDTO>> ReturnCarrito()
        {
            var carrito = await _localStorageService.GetItemAsync<List<CarritoDTO>>("carrito");
            if (carrito == null)
                carrito = new List<CarritoDTO>();

            return carrito;
        }
    }
}
