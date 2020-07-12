using LinXi_IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinXi_IService
{
    public interface IBaseService<TEntity> where TEntity : class, new()
    {
        IBaseRepository<TEntity> iBaseDAL { get; set; }

        Task<int> Add(TEntity entities);

        Task<int> Delete(TEntity entities);

        Task<int> Edit(TEntity entities);

        /// <summary>
        /// 姓名主键
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<TEntity> FindAsyncByName(string name);

        /// <summary>
        /// ID主键
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<TEntity> FindAsyncById(int name);

        Task<IQueryable<TEntity>> Search(Expression<Func<TEntity, bool>> wherelamda);

        IQueryable<TEntity> SearchByPage<TOrder>(int pageIndex, int pageSize, out int count, Expression<Func<TEntity, bool>> wherelamda, Expression<Func<TEntity, TOrder>> orderlamda, bool isAsc);

        Task<IQueryable<TEntity>> SearchByPageAsync<TOrder>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> wherelamda, Func<TEntity, TOrder> orderlamda, bool isAsc);
    }
}