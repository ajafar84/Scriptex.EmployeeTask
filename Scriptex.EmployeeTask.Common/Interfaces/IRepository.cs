using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Scriptex.EmployeeTask.Common.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        #region Filters

        TEntity Find(object id);
        //IPagedList<TViewModel> Search<TViewModel>(SearchModel searchModel, Expression<Func<TEntity, bool>> filter = null);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includes);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includes);
        bool Any(Expression<Func<TEntity, bool>> filter = null);

        #region Where
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includes);
        //IEnumerable<TReturnedVM> Where<TReturnedVM>(Expression<Func<TEntity, bool>> filter = null,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //    params Expression<Func<TEntity, object>>[] includes);
        #endregion

        #endregion

        #region Get
        //TReturnedVM Get<TReturnedVM>(Expression<Func<TEntity, bool>> whereClause);
        #endregion

        #region GetAll
        //IEnumerable<TReturnedVM> GetAll<TReturnedVM>(Expression<Func<TEntity, bool>> whereClause = null,
        //    Expression<Func<TReturnedVM, object>> orderByClause = null, SortOrder sortOrder = SortOrder.Descending);
        #endregion

        #region Paging
        IQueryable<TEntity> PageAll(int skip, int take);
        IQueryable<TEntity> PageAll(int skip, int take, Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> PageAll(int skip, int take, params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> PageAll(int skip, int take, Expression<Func<TEntity, bool>> filter = null, 
            params Expression<Func<TEntity, object>>[] includes);
        #endregion

        #region Include
        IQueryable<TEntity> Include(Expression<Func<TEntity, object>> predicate);
        IQueryable<TEntity> IncludeMultiple(params Expression<Func<TEntity, object>>[] includeProperties);
        IQueryable<TEntity> IncludeMultiple(params string[] includeProperties);

        #endregion

        #region Aggregation
        int Max(Func<TEntity, int> selector);
        long Max(Func<TEntity, long> selector);
        int Count();
        int Count(Expression<Func<TEntity, bool>> predicate);
        #endregion

        #region Add
        TEntity Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        // Deprecated
        void AddOrUpdate(TEntity entity);
        void Update(TEntity entity, object newValue);
        #endregion

        #region Remove
        void Remove(object id);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        #endregion Remove

        IQueryable<TEntity> GetQueryable();
    }
}
