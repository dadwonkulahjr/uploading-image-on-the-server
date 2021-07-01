using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace UploadingFileDemo.UI.Data.Access.IRepository
{
    public interface IDefaultRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> expressionFilter = null,
            string navigationProperties = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>
            orderEntitiesByType = null);
        void Add(TEntity entity);
        TEntity Get(int id);
        TEntity Get(TEntity entity);
    }
}
