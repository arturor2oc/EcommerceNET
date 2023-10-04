using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceNET.DTO
{
    public class TargetaDTO
    {
        [Required(ErrorMessage = "Ingrese Titular")]
        public string? Titular { get; set; }
        [Required(ErrorMessage = "Ingrese Numero Tarjeta")]
        public string? Numero { get; set; }
        [Required(ErrorMessage = "Ingrese Vigencia")]
        public string? Vigencia { get; set; }
        [Required(ErrorMessage = "Ingrese CVV")]
        public string? CVV { get; set; }

    }
}
