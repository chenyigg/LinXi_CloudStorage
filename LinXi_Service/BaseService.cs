using LinXi_IRepository;
using LinXi_IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinXi_Service
{
    public abstract class BaseService<TEntity> where TEntity : class, new()
    {
        public BaseService(IBaseRepository<TEntity> PiBaseDAL)
        {
            iBaseDAL = PiBaseDAL;
        }

        public IBaseRepository<TEntity> iBaseDAL { get; set; }

        public async Task<int> Add(TEntity entities)
        {
            await iBaseDAL.Add(entities);
            return await SaveChanges();
        }

        public async Task<int> Delete(TEntity entities)
        {
            await iBaseDAL.Delete(entities);
            return await SaveChanges();
        }

        public async Task<int> Edit(TEntity entities)
        {
            await iBaseDAL.Edit(entities);
            return await SaveChanges();
        }

        public async Task<IQueryable<TEntity>> Search(Expression<Func<TEntity, bool>> wherelamda)
        {
            return await iBaseDAL.Search(wherelamda);
        }

        public async Task<TEntity> FindAsyncByName(string name)
        {
            return await iBaseDAL.FindAsyncByName(name);
        }

        public async Task<TEntity> FindAsyncById(int id)
        {
            return await iBaseDAL.FindAsyncById(id);
        }

        public IQueryable<TEntity> SearchByPage<D>(int pageIndex, int pageSize, out int count, Expression<Func<TEntity, bool>> wherelamda, Expression<Func<TEntity, D>> orderlamda, bool isAsc)
        {
            return iBaseDAL.SearchByPage(pageIndex, pageSize, out count, wherelamda, orderlamda, isAsc);
        }

        public async Task<IQueryable<TEntity>> SearchByPageAsync<D>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> wherelamda, Func<TEntity, D> orderlamda, bool isAsc)
        {
            return await iBaseDAL.SearchByPageAsync(pageIndex, pageSize, wherelamda, orderlamda, isAsc);
        }

        private async Task<int> SaveChanges()
        {
            return await iBaseDAL.SaveChanges();
        }
    }
}