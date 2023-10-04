using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using EcommerceNET.Model;
using EcommerceNET.DTO;
using EcommerceNET.Repository.Contract;
using EcommerceNET.Service.Contract;
using AutoMapper;

namespace EcommerceNET.Service.Implements
{
    public class UserService : IUserService
    {
        private readonly IGenerictRepository<Usuario> _modelRepository;
        private readonly IMapper _mapper;
        public UserService(IGenerictRepository<Usuario> modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }

        public async Task<SesionDTO> Authorization(LoginDTO model)
        {
            try
            {
                var query = _modelRepository.GetAll(p => p.Correo == model.Correo && p.Clave == model.Clave);
                var fromDbModel = await query.FirstOrDefaultAsync();
                if (fromDbModel != null)
                {
                    return _mapper.Map<SesionDTO>(fromDbModel);
                }
                else
                {
                    throw new TaskCanceledException("No se encontraron coincidencias");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var query = _modelRepository.GetAll(p => p.IdUsuario == id);
                var fromDbModel = await query.FirstOrDefaultAsync();

                if (fromDbModel != null)
                {
                    var res = await _modelRepository.Delete(fromDbModel);
                    if (!res)
                    {
                        throw new TaskCanceledException("No se pudo eliminar");
                    }
                    return res;
                }
                else
                {
                    throw new TaskCanceledException("No se encontraron resultados");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserDTO> Get(int id)
        {
            try
            {
                var query = _modelRepository.GetAll(p => p.IdUsuario == id);
                var fromDbModel = await query.FirstOrDefaultAsync();
                if (fromDbModel != null)
                {
                    return _mapper.Map<UserDTO>(fromDbModel);
                }
                else
                {
                    throw new TaskCanceledException("No se encontraron coincidencias");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserDTO> Insert(UserDTO model)
        {
            try
            {
                var dbModel = _mapper.Map<Usuario>(model);
                var resModel = await _modelRepository.Insert(dbModel);

                if (resModel.IdUsuario != 0)
                {
                    return _mapper.Map<UserDTO>(resModel);
                }
                else
                {
                    throw new TaskCanceledException("No se pudo crear");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UserDTO>> ListUser(string rol, string searh)
        {
            try
            {
                var query = _modelRepository.GetAll(p =>
                p.Rol == rol &&
                string.Concat(p.NombreCompleto.ToLower(), p.Correo.ToLower()).Contains(searh.ToLower())
                );

                List<UserDTO> list = _mapper.Map<List<UserDTO>>(await query.ToListAsync());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(UserDTO model)
        {
            try
            {
                var query = _modelRepository.GetAll(p => p.IdUsuario == model.IdUsuario);
                var fromDbModel = await query.FirstOrDefaultAsync();

                if (fromDbModel != null)
                {
                    fromDbModel.NombreCompleto = model.NombreCompleto;
                    fromDbModel.Correo = model.Correo;
                    fromDbModel.Clave = model.Clave;
                    var res = await _modelRepository.Update(fromDbModel);
                    if (!res)
                    {
                        throw new TaskCanceledException("No se pudo editar");

                    }
                    return res;
                }
                else
                {
                    throw new TaskCanceledException("No se encontraron resultados");

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
