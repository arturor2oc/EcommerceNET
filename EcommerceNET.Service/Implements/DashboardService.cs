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
using EcommerceNET.Repository.Implements;

namespace EcommerceNET.Service.Implements
{
    public class DashboardService : IDashboardService
    {
        private readonly IVentRepository _ventRepository;
        private readonly IGenerictRepository<Usuario> _userRepository;
        private readonly IGenerictRepository<Producto> _productRepository;
        public DashboardService(
            IVentRepository ventRepository,
            IGenerictRepository<Usuario> userRepository,
            IGenerictRepository<Producto> productRepository
            )
        {
            _ventRepository = ventRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        private string Ingresos()
        {
            var query = _ventRepository.GetAll();
            decimal? ingresos = query.Sum(x => x.Total);
            return Convert.ToString(ingresos);
        }
        private int Vents()
        {
            var query = _ventRepository.GetAll();
            int total = query.Count();
            return total;
        }
        private int Clients()
        {
            var query = _userRepository.GetAll(user => user.Rol.ToLower() == "cliente");
            int total = query.Count();
            return total;
        }
        private int Products()
        {
            var query = _productRepository.GetAll();
            int total = query.Count();
            return total;
        }
        public DashboardDTO Resum()
        {
            try
            {
                DashboardDTO dto = new DashboardDTO()
                {
                    TotalIngreso = Ingresos(),
                    TotalVenta = Vents(),
                    TotalCliente = Clients(),
                    TotalProductos = Products(),
                };
                return dto;
            }catch ( Exception ex )
            {
                throw ex;
            }
        }
         
    }
   
}
