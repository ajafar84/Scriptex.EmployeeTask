using Scriptex.EmployeeTask.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace Scriptex.EmployeeTask.Common.Repositories
{
    public class Repository<TContext, TEntity> : IRepository<TEntity>
        where TContext : DbContext, new()
        where TEntity : class
    {
        protected TContext _context;

        public Repository()
        {
            _context = new TContext();
        }

        public Repository(TContext context)
        {
            _context = context;
        }

        public void SetContext(TContext context)
        {
            _context = context;
        }

        #region Filters

        public TEntity Find(object id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            query = includes.Aggregate(query, (current, include) => current.Include(include));

            return filter != null ? query.SingleOrDefault(filter) : query.SingleOrDefault();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            query = includes.Aggregate(query, (current, include) => current.Include(include));

            return filter != null ? query.FirstOrDefault(filter) : query.FirstOrDefault();
        }

        public bool Any(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            return query.Any(filter);
        }

        #region Where
        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (filter != null)
                query = query.Where(filter);

            return query;
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return query;
        }

        //public IEnumerable<TReturnedVM> Where<TReturnedVM>(Expression<Func<TEntity, bool>> filter = null,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //    params Expression<Func<TEntity, object>>[] includes)
        //{
        //    IQueryable<TEntity> query = _context.Set<TEntity>();

        //    query = includes.Aggregate(query, (current, include) => current.Include(include));

        //    if (filter != null)
        //        query = query.Where(filter);

        //    if (orderBy != null)
        //        query = orderBy(query);

        //    return query.ProjectTo<TReturnedVM>(_mapConfig).ToList();
        //}

        #endregion

        #endregion

        #region Paging
        public IQueryable<TEntity> PageAll(int skip, int take)
        {
            return _context.Set<TEntity>().Skip(skip).Take(take);
        }
        public IQueryable<TEntity> PageAll(int skip, int take, Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).Skip(skip).Take(take);
        }
        public IQueryable<TEntity> PageAll(int skip, int take, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            query = includes.Aggregate(query, (current, include) => current.Include(include));

            return query.Skip(skip).Take(take);
        }
        public IQueryable<TEntity> PageAll(int skip, int take, Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (filter != null)
                query = query.Where(filter);

            return query.Skip(skip).Take(take);
        }
        #endregion

        #region Include
        public IQueryable<TEntity> Include(Expression<Func<TEntity, object>> predicate)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            return query.Include(predicate);
        }

        public IQueryable<TEntity> IncludeMultiple(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (includeProperties != null)
            {
                foreach (var item in includeProperties)
                {
                    query = query.Include(item);
                }

                return query;
            }

            return query;
        }

        public IQueryable<TEntity> IncludeMultiple(params string[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            if (includeProperties != null)
            {
                foreach (var item in includeProperties)
                {
                    query = query.Include(item);
                }

                return query;
            }

            return query;
        }
        #endregion

        #region Aggregation

        public int Max(Func<TEntity, int> selector)
        {
            if (_context.Set<TEntity>().Any())
            {
                return _context.Set<TEntity>().Max(selector);
            }
            else
            {
                return 0;
            }

        }

        public long Max(Func<TEntity, long> selector)
        {
            if (_context.Set<TEntity>().Any())
            {
                return _context.Set<TEntity>().Max(selector);
            }
            else
            {
                return 0;
            }
        }

        public int Count()
        {
            return _context.Set<TEntity>().Count();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Count(predicate);
        }

        #endregion

        #region Add

        public TEntity Add(TEntity entity)
        {
            return _context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        // Deprecated
        public void AddOrUpdate(TEntity entity)
        {
            _context.Set<TEntity>().AddOrUpdate(entity);
        }

        public void Update(TEntity entity, object newValue)
        {
            _context.Entry<TEntity>(entity).CurrentValues.SetValues(newValue);
        }

        #endregion

        #region Remove

        public void Remove(object id)
        {
            var entity = Find(id);
            if (entity != null)
                _context.Set<TEntity>().Remove(entity);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        #endregion

        public IQueryable<TEntity> GetQueryable()
        {
            return _context.Set<TEntity>().AsQueryable();
        }
    }
}
