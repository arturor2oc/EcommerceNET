using EcommerceNET.DTO;
using Microsoft.AspNetCore.Components;

namespace EcommerceNET.WebAssembly.Pages.Admin
{
    public partial class User
    {
        [Parameter]
        public int id { get; set; }
        private string title = "Nuevo Usuario";
        private string btn = "Crear";
        private UserDTO model = new UserDTO();

        protected override async Task OnParametersSetAsync()
        {
            if (id != 0)
            {
                title = "Editar Usuario";
                btn = "Actualizar";

                var respose = await usuarioService.Get(id);
                if (respose.EsCorrecto)
                {
                    model = (UserDTO)respose.Resultado!;
                    model.ConfirmarClave = model.Clave;
                }
                else
                {
                    toastService.ShowWarning(respose.Mensaje!);
                }
            }
        }

        private async Task SaveChanges()
        {
            if (model.Clave != model.ConfirmarClave)
            {
                toastService.ShowWarning("La contraseñas no coinciden");
                return;
            }

            bool res = true;
            string mjs = string.Empty;

            if (id != 0)
            {
                var respose = await usuarioService.Update(model);
                if (respose.EsCorrecto)
                {
                    mjs = "Usuario modificado";
                }
                else
                {
                    res = false;
                    mjs = "No se pudo editar";
                }
            }
            else
            {
                model.Rol = "Administrador";
                var respose = await usuarioService.Insert(model);

                if (respose.EsCorrecto)
                {
                    mjs = "Usuario creado";
                }
                else
                {
                    res = false;
                    mjs = "No se pudo crear";
                }
            }

            if (res)
            {
                toastService.ShowSuccess(mjs);
                _navService.NavigateTo("/users");
            }
            else
            {
                toastService.ShowWarning(mjs);
            }
        }
    }
}
