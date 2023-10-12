using EcommerceNET.DTO;
using Microsoft.AspNetCore.Components;

namespace EcommerceNET.WebAssembly.Pages.Store
{
    public partial class Detail
    {
        [Parameter]
        public int id { get; set; }
        private ProductoDTO? model = null;
        private int cont = 1;

        protected override async Task OnParametersSetAsync()
        {
            var respose = await productService.Get(id);
            if (respose.EsCorrecto)
                model = (ProductoDTO)respose.Resultado!;
        }

        private async Task AddCart(ProductoDTO model)
        {
            decimal? priceFinal = (model.PrecioOferta != 0 && model.PrecioOferta < model.Precio) ? model.PrecioOferta : model.Precio;
            CarritoDTO cart = new CarritoDTO()
            {
                Producto = model,
                Cantidad = cont,
                precio = priceFinal,
                Total = Convert.ToDecimal(cont) * priceFinal
            };
            await cartService.AddCarrito(cart);
        }
    }
}
