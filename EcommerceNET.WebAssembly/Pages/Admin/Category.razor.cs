using EcommerceNET.DTO;
using Microsoft.AspNetCore.Components;

namespace EcommerceNET.WebAssembly.Pages.Admin
{
    public partial class Category
    {
        [Parameter]
        public int id { get; set; }
        private string title = "Nueva Categoria";
        private string btn = "Crear";
        private CategoriaDTO model = new CategoriaDTO();

        protected override async Task OnParametersSetAsync()
        {
            if (id != 0)
            {
                title = "Editar Categoria";
                btn = "Actualizar";

                var respose = await categoryService.Get(id);
                if (respose.EsCorrecto)
                {
                    model = (CategoriaDTO)respose.Resultado!;
                }
                else
                {
                    toastService.ShowWarning(respose.Mensaje);
                }
            }
        }

        private async Task SaveChanges()
        {
            bool res = true;
            string mjs = string.Empty;

            if (id != 0)
            {
                var respose = await categoryService.Update(model);
                if (respose.EsCorrecto)
                {
                    mjs = "Categoria modificada";
                }
                else
                {
                    res = false;
                    mjs = "No pudo editar";
                }
            }
            else
            {
                var respose = await categoryService.Insert(model);

                if (respose.EsCorrecto)
                {
                    mjs = "Categoria creada";
                }
                else
                {
                    res = false;
                    mjs = "No pudo crear";
                }
            }

            if (res)
            {
                toastService.ShowSuccess(mjs);
                _navService.NavigateTo("/categories");
            }
            else
            {
                toastService.ShowWarning(mjs);
            }
        }
    }
}
