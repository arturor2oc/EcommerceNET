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
    public class VentService : IVentService
    {
        private readonly IVentRepository _modelRepository;
        private readonly IMapper _mapper;
        public VentService(IVentRepository modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }

        public async Task<VentaDTO> Create(VentaDTO model)
        {
            try
            {
                var dbModel = _mapper.Map<Venta>(model);
                var vent = await _modelRepository.Create(dbModel);

                if (vent.IdVenta == 0)
                {
                    throw new TaskCanceledException("No se pudo Registrar");

                }
                else
                {
                    return _mapper.Map<VentaDTO>(vent);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
