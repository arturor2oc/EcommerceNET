using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using EcommerceNET.Repository.Contract;
using EcommerceNET.Repository.DBContext;

namespace EcommerceNET.Repository.Implements
{
    public class GenerictRepository<TModel> : IGenerictRepository<TModel> where TModel : class
    {
        private readonly DbecommerceContext _dbContext;

        public GenerictRepository(DbecommerceContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TModel> GetAll(Expression<Func<TModel, bool>>? filtro = null)
        {
            IQueryable<TModel> query = (filtro == null) ? _dbContext.Set<TModel>() : _dbContext.Set<TModel>().Where(filtro);
            return query;
        }
        
        public async Task<TModel> Insert(TModel model)
        {
            try
            {
                _dbContext.Set<TModel>().Add(model);
                await _dbContext.SaveChangesAsync();
                return model;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Update(TModel model)
        {
            try
            {
                _dbContext.Set<TModel>().Update(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete(TModel model)
        {
            try
            {
                _dbContext.Set<TModel>().Remove(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
