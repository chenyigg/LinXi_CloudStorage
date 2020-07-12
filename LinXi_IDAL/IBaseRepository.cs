using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinXi_IRepository
{
    public interface IBaseRepository<TEntity> where TEntity : class, new()
    {
        Task Add(TEntity entities);

        Task Delete(TEntity entities);

        Task Edit(TEntity entities);

        Task<IQueryable<TEntity>> Search(Expression<Func<TEntity, bool>> wherelamda);

        Task<TEntity> FindAsyncByName(string name);

        Task<TEntity> FindAsyncById(int id);

        IQueryable<TEntity> SearchByPage<TOrder>(int pageSize, int pageCount, out int count, Expression<Func<TEntity, bool>> wherelamda, Expression<Func<TEntity, TOrder>> orderlamda, bool isAsc);

        Task<IQueryable<TEntity>> SearchByPageAsync<TOrder>(int pageSize, int pageCount, Expression<Func<TEntity, bool>> wherelamda, Func<TEntity, TOrder> orderlamda, bool isAsc);

        Task<int> SaveChanges();
    }
}