using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceNET.DTO
{
    public class DashboardDTO
    {
        public string? TotalIngreso { get; set; }
        public int TotalVenta{ get; set; }
        public int TotalCliente{ get; set; }
        public int TotalProductos{ get; set; }
    }
}
