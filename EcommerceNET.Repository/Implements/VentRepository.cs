using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceNET.Model;
using EcommerceNET.Repository.Contract;
using EcommerceNET.Repository.DBContext;

namespace EcommerceNET.Repository.Implements
{
    public class VentRepository : GenerictRepository<Venta>, IVentRepository
    {
        private readonly DbecommerceContext _dbContext;

        public VentRepository(DbecommerceContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Venta> Create(Venta model)
        {
            Venta venta = new Venta();
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (DetalleVenta dv in model.DetalleVenta)
                    {
                        Producto product = _dbContext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();
                        product.Cantidad = product.Cantidad - dv.Cantidad;
                        _dbContext.Productos.Update(product);
                    }
                    await _dbContext.SaveChangesAsync();

                    await _dbContext.Venta.AddAsync(model);
                    await _dbContext.SaveChangesAsync();
                    venta = model;
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return venta;
        }
    }
}
