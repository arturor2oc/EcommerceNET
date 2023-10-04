using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EcommerceNET.DTO;

namespace EcommerceNET.Service.Contract
{
    public interface IUserService
    {
        Task<List<UserDTO>> ListUser(string rol, string searh);
        Task<UserDTO> Get(int id);
        Task<SesionDTO> Authorization(LoginDTO model);
        Task<UserDTO> Insert(UserDTO model);
        Task<bool> Update(UserDTO model);
        Task<bool> Delete(int id);
    }
}
