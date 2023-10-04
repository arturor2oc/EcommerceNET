using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceNET.Repository.Contract
{
    public interface IGenerictRepository<TModel> where TModel : class
    {
        IQueryable<TModel> GetAll(Expression<Func<TModel,bool>>? filtro = null);
        Task<TModel> Insert(TModel model);
        Task<bool> Update(TModel model);
        Task<bool> Delete(TModel model);
    }
}
