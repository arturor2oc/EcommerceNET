using CurrieTechnologies.Razor.SweetAlert2;
using EcommerceNET.DTO;

namespace EcommerceNET.WebAssembly.Pages.Admin
{
    public partial class Products
    {
        private List<ProductoDTO>? list = null;
        private string searh = "";

        private async Task GetProducts(string value = "")
        {
            var respose = await productService.ListProduct(value);
            if (respose.EsCorrecto)
            {
                list = (List<ProductoDTO>)respose.Resultado!;
            }
            else
            {
                list = new List<ProductoDTO>();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await GetProducts();
        }

        private async Task Delete(ProductoDTO model)
        {
            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = "Estas Seguro?",
                Text = $"Eliminar Producto {model.Nombre}",
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true,
                ConfirmButtonText = "Si, eliminar",
                CancelButtonText = "No, volver",
            });

            if (result.IsConfirmed)
            {
                var respose = await productService.Delete(model.IdProducto);
                if (respose.EsCorrecto)
                {
                    await GetProducts();
                    toastService.ShowSuccess("Producto Eliminado");
                }
                else
                {
                    toastService.ShowSuccess(respose.Mensaje);
                }
            }
        }
    }
}
