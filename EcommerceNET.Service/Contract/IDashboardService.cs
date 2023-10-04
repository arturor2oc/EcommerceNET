using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EcommerceNET.DTO;

namespace EcommerceNET.Service.Contract
{
    public interface IDashboardService
    {
        DashboardDTO Resum();
    }
}
