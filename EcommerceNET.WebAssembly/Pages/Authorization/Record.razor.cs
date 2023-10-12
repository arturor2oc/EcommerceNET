using EcommerceNET.DTO;

namespace EcommerceNET.WebAssembly.Pages.Authorization
{
    public partial class Record
    {
        private UserDTO model = new UserDTO();

        private async Task SaveChanges()
        {
            if (model.Clave != model.ConfirmarClave)
            {
                toastService.ShowWarning("Las contraseñas no coinciden");
                return;
            }
            model.Rol = "Client";
            var respose = await _userService.Insert(model);
            if (respose.EsCorrecto)
            {
                toastService.ShowSuccess("Su cuenta ha sido creada");
                _navService.NavigateTo("/login");
            }
            else
            {
                toastService.ShowError("No se pudo crear su cuenta, intente mas tarde");
            }
        }
    }
}
