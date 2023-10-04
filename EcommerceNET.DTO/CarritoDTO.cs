using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceNET.DTO
{
    public class CarritoDTO
    {
        public ProductoDTO? Producto { get; set; }
        public int? Cantidad { get; set; }  
        public decimal? precio { get; set; }  
        public decimal? Total { get; set; }  
    }
}
