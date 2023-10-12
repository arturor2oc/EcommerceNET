using CurrieTechnologies.Razor.SweetAlert2;
using EcommerceNET.DTO;

namespace EcommerceNET.WebAssembly.Pages.Admin
{
    public partial class Users
    {
        private List<UserDTO>? list = null;
        private string searh = "";

        private async Task GetUsers(string value = "")
        {
            var respose = await usuarioService.ListUser("Administrador", value);
            if (respose.EsCorrecto)
            {
                list = (List<UserDTO>)respose.Resultado!;
            }
            else
            {
                list = new List<UserDTO>();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await GetUsers();
        }

        private async Task Delete(UserDTO model)
        {
            SweetAlertResult result = await Swal.FireAsync(new SweetAlertOptions
            {
                Title = "Estas Seguro?",
                Text = $"Eliminar usuario {model.NombreCompleto}",
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true,
                ConfirmButtonText = "Si, eliminar",
                CancelButtonText = "No, volver",
            });

            if (result.IsConfirmed)
            {
                var respose = await usuarioService.Delete(model.IdUsuario);
                if (respose.EsCorrecto)
                {
                    await GetUsers();
                    toastService.ShowSuccess("Usuario Eliminado");
                }
                else
                {
                    toastService.ShowSuccess(respose.Mensaje!);
                }
            }
        }
    }
}
