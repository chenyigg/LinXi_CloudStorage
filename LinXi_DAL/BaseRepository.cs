using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinXi_Repository
{
    public class BaseRepository<TEntity> where TEntity : class, new()
    {
        public BaseRepository(DbContext PdbContext)
        {
            dbContext = PdbContext;
        }

        private DbContext dbContext { get; }

        public async Task Add(TEntity entities)
        {
            await dbContext.Set<TEntity>().AddAsync(entities);
        }

        public async Task Delete(TEntity entities)
        {
            await Task.Run(() =>
            {
                dbContext.Set<TEntity>().Remove(entities);
            });
        }

        public async Task Edit(TEntity entities)
        {
            //dbContext.Entry<TEntity>(entities).State = EntityState.Modified;
            await Task.Run(() =>
            {
                dbContext.Set<TEntity>().Update(entities);
            });
        }

        public async Task<IQueryable<TEntity>> Search(Expression<Func<TEntity, bool>> wherelamda)
        {
            return await Task.Run(() => dbContext.Set<TEntity>().Where(wherelamda));
        }

        public async Task<bool> ExistEntityAsync(Expression<Func<TEntity, bool>> whereLamda)
        {
            return await dbContext.Set<TEntity>().AnyAsync(whereLamda);
        }

        public async Task<TEntity> FindAsyncById(int id)
        {
            return await dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> FindAsyncByName(string name)
        {
            return await dbContext.Set<TEntity>().FindAsync(name);
        }

        public IQueryable<TEntity> SearchByPage<TOrder>(int pageSize, int pageCount, out int count, Expression<Func<TEntity, bool>> wherelamda, Expression<Func<TEntity, TOrder>> orderlamda, bool isAsc)
        {
            var TSouce = dbContext.Set<TEntity>().Where(wherelamda);
            count = TSouce.Count();
            return isAsc ? TSouce.OrderBy(orderlamda).Skip((pageCount - 1) * pageSize).Take(pageCount) :
                                  TSouce.OrderByDescending(orderlamda).Skip((pageCount - 1) * pageSize).Take(pageCount);
        }

        public async Task<IQueryable<TEntity>> SearchByPageAsync<TOrder>(int pageSize, int pageCount, Expression<Func<TEntity, bool>> wherelamda, Func<TEntity, TOrder> orderlamda, bool isAsc)
        {
            var TSouce = await Search(wherelamda);
            return isAsc ? TSouce.OrderBy(orderlamda).Skip((pageCount - 1) * pageSize).Take(pageCount).AsQueryable() :
                                  TSouce.OrderByDescending(orderlamda).Skip((pageCount - 1) * pageSize).Take(pageCount).AsQueryable();
        }

        public async Task<int> SaveChanges()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}