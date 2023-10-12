using EcommerceNET.DTO;
using EcommerceNET.WebAssembly.Extensions;

namespace EcommerceNET.WebAssembly.Pages.Authorization
{
    public partial class Login
    {
        private LoginDTO model = new LoginDTO();

        private async Task Start()
        {
            var respose = await _userService.Authorization(model);
            if (respose.EsCorrecto)
            {
                SesionDTO sesion = (SesionDTO)respose.Resultado!;

                var authExterna = (AuthenticationExtension)authProvider;
                await authExterna.UpdateStateAuthentication(sesion);

                if (sesion.Rol.ToLower() == "client")
                {
                    _navService.NavigateTo("/catalog");
                }
                else
                {
                    _navService.NavigateTo("/dashboard");
                }
            }
            else
            {
                toastService.ShowWarning(respose.Mensaje!);
            }
        }
    }
}
