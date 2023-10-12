using Blazored.LocalStorage;
using EcommerceNET.DTO;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace EcommerceNET.WebAssembly.Extensions
{
    public class AuthenticationExtension : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private ClaimsPrincipal _sinInformation = new ClaimsPrincipal(new ClaimsIdentity());

        public AuthenticationExtension(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var sesionUser = await _localStorage.GetItemAsync<SesionDTO>("sesionUsuario");
            if(sesionUser == null)
            {
                return await Task.FromResult(new AuthenticationState(_sinInformation));
            }

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, sesionUser.IdUsuario.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, sesionUser.NombreCompleto),
                    new Claim(ClaimTypes.NameIdentifier, sesionUser.Correo),
                    new Claim(ClaimTypes.NameIdentifier, sesionUser.Rol)
                }, "JwtAuth"));

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        public async Task UpdateStateAuthentication(SesionDTO? sesionUser)
        {
            ClaimsPrincipal claimsPrincipal;
            if (sesionUser != null)
            {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, sesionUser.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Name, sesionUser.NombreCompleto),
                    new Claim(ClaimTypes.Email, sesionUser.Correo),
                    new Claim(ClaimTypes.Role, sesionUser.Rol)
                }, "JwtAuth"));

                await _localStorage.SetItemAsync("sesionUsuario", sesionUser);
            }
            else
            {
                claimsPrincipal = _sinInformation;
                await _localStorage.RemoveItemAsync("sesionUsuario");
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));

        }

    }
}
