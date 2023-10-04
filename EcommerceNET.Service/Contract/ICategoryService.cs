using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EcommerceNET.DTO;

namespace EcommerceNET.Service.Contract
{
    public interface ICategoryService
    {
        Task<List<CategoriaDTO>> ListCategory(string searh);
        Task<CategoriaDTO> Get(int id);
        Task<CategoriaDTO> Insert(CategoriaDTO model);
        Task<bool> Update(CategoriaDTO model);
        Task<bool> Delete(int id);
    }
}
