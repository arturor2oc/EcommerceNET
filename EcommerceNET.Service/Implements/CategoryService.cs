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
    public class CategoryService : ICategoryService
    {
        private readonly IGenerictRepository<Categoria> _modelRepository;
        private readonly IMapper _mapper;
        public CategoryService(IGenerictRepository<Categoria> modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var query = _modelRepository.GetAll(p => p.IdCategoria == id);
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

        public async Task<CategoriaDTO> Get(int id)
        {
            try
            {
                var query = _modelRepository.GetAll(p => p.IdCategoria == id);
                var fromDbModel = await query.FirstOrDefaultAsync();
                if (fromDbModel != null)
                {
                    return _mapper.Map<CategoriaDTO>(fromDbModel);
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

        public async Task<CategoriaDTO> Insert(CategoriaDTO model)
        {
            try
            {
                var dbModel = _mapper.Map<Categoria>(model);
                var resModel = await _modelRepository.Insert(dbModel);

                if (resModel.IdCategoria != 0)
                {
                    return _mapper.Map<CategoriaDTO>(resModel);
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

        public async Task<List<CategoriaDTO>> ListCategory(string searh)
        {
            try
            {
                var query = _modelRepository.GetAll(p =>
                p.Nombre!.ToLower().Contains(searh.ToLower())
                );

                List<CategoriaDTO> list = _mapper.Map<List<CategoriaDTO>>(await query.ToListAsync());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(CategoriaDTO model)
        {
            try
            {
                var query = _modelRepository.GetAll(p => p.IdCategoria == model.IdCategoria);
                var fromDbModel = await query.FirstOrDefaultAsync();

                if (fromDbModel != null)
                {
                    fromDbModel.Nombre = model.Nombre;
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
