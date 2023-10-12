using EcommerceNET.DTO;

namespace EcommerceNET.WebAssembly.Pages.Admin
{
    public partial class Dashboard
    {
        private DashboardDTO dashboard = new DashboardDTO();

        protected override async Task OnInitializedAsync()
        {
            var respose = await dashboardService.Resum();
            if (respose.EsCorrecto)
            {
                dashboard = (DashboardDTO)respose.Resultado!;
            }
        }
    }
}
