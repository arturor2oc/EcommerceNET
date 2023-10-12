using CurrieTechnologies.Razor.SweetAlert2;
using EcommerceNET.DTO;

namespace EcommerceNET.WebAssembly.Pages.Admin
{
    public partial class Categories
    {
        private List<CategoriaDTO>? list = null;
        private string searh = "";

        private async Task GetCategories(string value = "")
        {
            var respose = await categoryService.ListCategory(value);
            if (respose.EsCorrecto)
            {
                list = (List<CategoriaDTO>)respose.Resultado!;
            }
            else
            {
                list = new List<CategoriaDTO>();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await GetCategories();
        }

        private async Task Delete(CategoriaDTO model)
        {
            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = "Estas Seguro?",
                Text = $"Eliminar la categoria {model.Nombre}",
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true,
                ConfirmButtonText = "Si, eliminar",
                CancelButtonText = "No, volver",
            });

            if (result.IsConfirmed)
            {
                var respose = await categoryService.Delete(model.IdCategoria);
                if (respose.EsCorrecto)
                {
                    await GetCategories();
                    toastService.ShowSuccess("Categoria Eliminada");
                }
                else
                {
                    toastService.ShowSuccess(respose.Mensaje);
                }
            }
        }
    }
}
