using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UploadingFileDemo.UI.Data.Access.IRepository;

namespace UploadingFileDemo.UI.Data.Access.Repository
{
    public class DefaultRepository<T> : IDefaultRepository<T> where T : class
    {
        protected internal DbContext _dbContext;
        internal DbSet<T> _dbSet;

        public DefaultRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity); 
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }
        public T Get(T entity)
        {
            return _dbSet.Find(entity);
        }
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> expressionFilter = null, string navigationProperties = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderEntitiesByType = null)
        {
            IQueryable<T> customQuery = _dbSet;

            if(expressionFilter != null)
            {
                customQuery = customQuery.Where(expressionFilter);
            }

            if(navigationProperties != null)
            {
                foreach (var item in navigationProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    customQuery = customQuery.Include(navigationProperties);
                }
            }

            if(orderEntitiesByType != null)
            {
                return orderEntitiesByType(customQuery).ToList();
            }
            return customQuery.ToList();
        }
    }
}
