using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly EmployeeManagement_DBContext context;
        private DbSet<TEntity> dbSet;
        public Repository(EmployeeManagement_DBContext _context)
        {
            context = _context;
            this.dbSet = context.Set<TEntity>();
        }
        #region Get
        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbSet;
            foreach (var includeProperty in includes)
            {
                query = query.Include(includeProperty);
            }
            return query.Where(expression);
        }
        public IQueryable<TEntity> GetAll()
        {
            return dbSet.AsQueryable();
        }
        public TEntity GetByID(int id)
        {
            return context.Set<TEntity>().Find(id);
        }
        public TEntity FindAsNoTracking(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbSet;

            foreach (var includeProperty in includes)
            {
                query = query.Include(includeProperty);
            }
            var QueryEntity = query.AsNoTracking().Where(expression).FirstOrDefault();
            if(QueryEntity is not null)
            {
                context.Entry(QueryEntity).State = EntityState.Detached;
            }
            return QueryEntity;
        }
        #endregion
        #region Post
        public bool Add(TEntity entity)
        {
            if (entity == null)
            {
                return false;
            }
            dbSet.Add(entity);
            return true;
        }
        public bool Update(TEntity entity)
        {
            if (entity == null)
            {
                return false;
            }
            context.Update(entity);
            return true;
        }
        public bool Delete(TEntity entity)
        {
            if (entity == null)
            {
                return false;
            }
            dbSet.Remove(entity);
            return true;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() >= 0 ? true : false;
        }

      
        #endregion
    }
}
