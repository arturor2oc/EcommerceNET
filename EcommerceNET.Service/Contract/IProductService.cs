using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EcommerceNET.DTO;

namespace EcommerceNET.Service.Contract
{
    public interface IProductService
    {
        Task<List<ProductoDTO>> ListProduct(string searh);
        Task<List<ProductoDTO>> Catalog(string category,string searh);
        Task<ProductoDTO> Get(int id);
        Task<ProductoDTO> Insert(ProductoDTO model);
        Task<bool> Update(ProductoDTO model);
        Task<bool> Delete(int id);
    }
}
