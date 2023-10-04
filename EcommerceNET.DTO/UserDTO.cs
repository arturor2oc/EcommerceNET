using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceNET.DTO
{
    public class UserDTO
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage ="Ingrese nombre completo")]
        public string? NombreCompleto { get; set; }

        [Required(ErrorMessage = "Ingrese correo")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "Ingrese Contraseña")]
        public string? Clave { get; set; }

        [Required(ErrorMessage = "Ingrese confirmar contraseña")]
        public int? ConfirmarClave { get; set; }

        public string? Rol { get; set; }

    }
}
