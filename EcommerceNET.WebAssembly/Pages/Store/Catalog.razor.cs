using EcommerceNET.DTO;
using Microsoft.AspNetCore.Components;

namespace EcommerceNET.WebAssembly.Pages.Store
{
    public partial class Catalog
    {
        private List<CategoriaDTO>? listCategory = null;
        private List<ProductoDTO>? listProduct = null;
        private string searh = "";
        private string categorySelection = "Todos";

        private async Task GetCategories(string value = "")
        {
            var respose = await categoryService.ListCategory(value);
            if (respose.EsCorrecto)
            {
                listCategory = (List<CategoriaDTO>)respose.Resultado!;
            }
            else
            {
                listCategory = new List<CategoriaDTO>();
            }
        }

        private async Task GetCatalog()
        {
            var respose = await productService.Catalog(categorySelection, searh);
            if (respose.EsCorrecto)
            {
                listProduct = (List<ProductoDTO>)respose.Resultado!;
            }
            else
            {
                listProduct = new List<ProductoDTO>();
            }
        }

        private async Task RadioSelection(ChangeEventArgs args)
        {
            categorySelection = args.Value.ToString();
            searh = "";
            await GetCatalog();
        }

        protected override async Task OnInitializedAsync()
        {
            await GetCategories();
            await GetCatalog();
        }
    }
}
