using EcommerceNET.DTO;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace EcommerceNET.WebAssembly.Pages.Admin
{
    public partial class Product
    {
        [Parameter]
        public int id { get; set; }
        private string title = "Nuevo Producto";
        private string btn = "Crear";
        private ProductoDTO model = new ProductoDTO();
        private List<CategoriaDTO> listCategory = new List<CategoriaDTO>();

        protected override async Task OnInitializedAsync()
        {
            var respose = await categoryService.ListCategory("");
            if (respose.EsCorrecto)
            {
                listCategory = (List<CategoriaDTO>)respose.Resultado!;
                if (listCategory.Any() && id == 0)
                {
                    model.IdCategoria = listCategory.First().IdCategoria;
                }
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            if (id != 0)
            {
                title = "Editar Producto";
                btn = "Actualizar";

                var respose = await productService.Get(id);
                if (respose.EsCorrecto)
                {
                    model = (ProductoDTO)respose.Resultado!;
                }
                else
                {
                    toastService.ShowWarning(respose.Mensaje);
                }
            }
        }

        void ChangeCategory(ChangeEventArgs e)
        {
            model.IdCategoria = Convert.ToInt32(e.Value.ToString());
        }

        async Task OnFileChange(InputFileChangeEventArgs e)
        {
            FileInfo fileInfo = new FileInfo(e.File.Name);

            if (fileInfo.Extension.ToLower().Contains(".jpg") || fileInfo.Extension.ToLower().Contains(".png"))
            {
                var format = $"image/{fileInfo.Extension.Replace(".", "")}";
                var resizeImage = await e.File.RequestImageFileAsync(format, 450, 300);
                var buffer = new byte[resizeImage.Size];
                await resizeImage.OpenReadStream().ReadAsync(buffer);
                var imageData = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
                model.Imagen = imageData;
            }
        }

        private async Task SaveChanges()
        {
            bool res = true;
            string mjs = string.Empty;

            if (id != 0)
            {
                var respose = await productService.Update(model);
                if (respose.EsCorrecto)
                {
                    mjs = "Producto modificado";
                }
                else
                {
                    res = false;
                    mjs = "No pudo editar";
                }
            }
            else
            {
                var respose = await productService.Insert(model);

                if (respose.EsCorrecto)
                {
                    mjs = "Producto creado";
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
                _navService.NavigateTo("/products");
            }
            else
            {
                toastService.ShowWarning(mjs);
            }
        }
    }
}
