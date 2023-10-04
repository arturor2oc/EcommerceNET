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
    public class ProductService: IProductService
    {
        private readonly IGenerictRepository<Producto> _modelRepository;
        private readonly IMapper _mapper;
        public ProductService(IGenerictRepository<Producto> modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductoDTO>> Catalog(string category, string searh)
        {
            try
            {
                var query = _modelRepository.GetAll(p =>
                p.Nombre.ToLower().Contains(searh.ToLower()) &&
                p.IdCategoriaNavigation.Nombre.ToLower().Contains(category.ToLower())
                );

                List<ProductoDTO> list = _mapper.Map<List<ProductoDTO>>(await query.ToListAsync());
                return list;
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
                var query = _modelRepository.GetAll(p => p.IdProducto == id);
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

        public async Task<ProductoDTO> Get(int id)
        {
            try
            {
                var query = _modelRepository.GetAll(p => p.IdProducto == id);
                var fromDbModel = await query.FirstOrDefaultAsync();

                if (fromDbModel != null)
                {
                    return _mapper.Map<ProductoDTO>(fromDbModel);
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

        public async Task<ProductoDTO> Insert(ProductoDTO model)
        {
            try
            {
                var dbModel = _mapper.Map<Producto>(model);
                var resModel = await _modelRepository.Insert(dbModel);

                if (resModel.IdProducto != 0)
                {
                    return _mapper.Map<ProductoDTO>(resModel);
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

        public async Task<List<ProductoDTO>> ListProduct(string searh)
        {
            try
            {
                var query = _modelRepository.GetAll(p =>
                p.Nombre.ToLower().Contains(searh.ToLower())
                );

                query = query.Include(c => c.IdCategoriaNavigation);

                List<ProductoDTO> list = _mapper.Map<List<ProductoDTO>>(await query.ToListAsync());
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(ProductoDTO model)
        {
            try
            {
                var query = _modelRepository.GetAll(p => p.IdProducto == model.IdProducto);
                var fromDbModel = await query.FirstOrDefaultAsync();

                if (fromDbModel != null)
                {
                    fromDbModel.Nombre = model.Nombre;
                    fromDbModel.Descripcion = model.Descripcion;
                    fromDbModel.IdCategoria = model.IdCategoria;
                    fromDbModel.Precio = model.Precio;
                    fromDbModel.PrecioOferta = model.PrecioOferta;
                    fromDbModel.Cantidad = model.Cantidad;
                    fromDbModel.Imagen = model.Imagen;

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
