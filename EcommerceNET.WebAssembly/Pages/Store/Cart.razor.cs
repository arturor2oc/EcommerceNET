using EcommerceNET.DTO;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace EcommerceNET.WebAssembly.Pages.Store
{
    public partial class Cart
    {
        private bool disabled = true;
        private List<CarritoDTO> list = new List<CarritoDTO>();
        private decimal? TotalPay = 0;
        private TargetaDTO card = new TargetaDTO();

        [CascadingParameter]
        private Task<AuthenticationState> autheticatonState { get; set; }
        protected override async Task OnInitializedAsync()
        {
            list = await cartService.ReturnCarrito();

            var authState = await autheticatonState;
            if (authState.User.Identity.IsAuthenticated)
                disabled = false;
        }

        private void Decreased(int id)
        {
            CarritoDTO item = list.First(p => p.Producto.IdProducto == id);

            if (item.Cantidad - 1 > 0)
            {
                decimal? priceFinal = (item.Producto.PrecioOferta != 0 && item.Producto.PrecioOferta < item.Producto.Precio) ? item.Producto.PrecioOferta : item.Producto.Precio;
                item.Cantidad--;
                item.Total = item.Cantidad * priceFinal;
            }
        }

        private void Increase(int id)
        {
            CarritoDTO item = list.First(p => p.Producto.IdProducto == id);

            decimal? priceFinal = (item.Producto.PrecioOferta != 0 && item.Producto.PrecioOferta < item.Producto.Precio) ? item.Producto.PrecioOferta : item.Producto.Precio;
            item.Cantidad++;
            item.Total = item.Cantidad * priceFinal;
        }

        private async Task Delete(int id)
        {
            CarritoDTO product = list.First(p => p.Producto.IdProducto == id);

            if (product != null)
            {
                list.Remove(product);
                await cartService.DeleteCarrito(id);
            }
        }

        private async Task ProcesarPago()
        {
            if (list.Count == 0)
            {
                toastService.ShowWarning("No se encontraron prouctos");
                return;
            }
            List<DetalleVentaDTO> detail = new List<DetalleVentaDTO>();

            foreach (var item in list)
            {
                detail.Add(new DetalleVentaDTO()
                {
                    IdProducto = item.Producto.IdProducto,
                    Cantidad = item.Cantidad,
                    Total = item.Total
                });
            }

            var authState = await autheticatonState;
            string idUser = authState.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).Single();
            VentaDTO model = new VentaDTO()
            {
                IdUsuario = Convert.ToInt32(idUser),
                Total = list.Sum(i => i.Total),
                DetalleVenta = detail
            };

            var response = await ventService.Create(model);
            if (response.EsCorrecto)
            {
                await cartService.ClearCarrito();
                toastService.ShowSuccess("Venta registrada");
                _navService.NavigateTo("/catalog");
            }
            else
            {
                toastService.ShowError(response.Mensaje);
            }
        }
    }
}
