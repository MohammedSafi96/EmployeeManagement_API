using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        bool Add(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        IQueryable<TEntity> GetAll();
        TEntity GetByID(int id);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes);
        TEntity FindAsNoTracking(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes);
        bool SaveChanges();
    }
}
