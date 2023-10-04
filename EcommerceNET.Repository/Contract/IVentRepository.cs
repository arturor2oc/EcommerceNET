using EcommerceNET.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceNET.Repository.Contract
{
    public interface IVentRepository: IGenerictRepository<Venta>
    {
        Task<Venta> Create(Venta model);
    }
}
