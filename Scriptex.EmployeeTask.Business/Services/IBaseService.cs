using Scriptex.EmployeeTask.Common.Interfaces;
using Scriptex.EmployeeTask.Common.Search;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace Scriptex.EmployeeTask.Business.Services
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        IResponse Get<TReturnedVM>(Expression<Func<TEntity, bool>> whereClause);
        IResponse Get<TReturnedVM>(Expression<Func<TEntity, bool>> whereClause,
            params Expression<Func<TEntity, object>>[] includes);

        IResponse Get<TTargetEntity, TReturnedVM>(Expression<Func<TTargetEntity, bool>> whereClause)
           where TTargetEntity : class;
        IResponse Get<TTargetEntity, TReturnedVM>(Expression<Func<TTargetEntity, bool>> whereClause,
            params Expression<Func<TTargetEntity, object>>[] includes)
                where TTargetEntity : class;

        IResponse GetList<TReturnedVM>(Expression<Func<TEntity, bool>> whereClause = null,
             Expression<Func<TReturnedVM, object>> orderByClause = null, SortOrder sortOrder = SortOrder.Descending);
        IResponse GetList<TReturnedVM>(Expression<Func<TEntity, bool>> whereClause = null,
            Expression<Func<TReturnedVM, object>> orderByClause = null, SortOrder sortOrder = SortOrder.Descending,
            params Expression<Func<TEntity, object>>[] includes);

        IResponse GetList<TTargetEntity, TReturnedVM>(Expression<Func<TTargetEntity, bool>> whereClause,
            Expression<Func<TReturnedVM, object>> orderByClause, SortOrder sortOrder = SortOrder.Descending)
                where TTargetEntity : class;
        IResponse GetList<TTargetEntity, TReturnedVM>(Expression<Func<TTargetEntity, bool>> whereClause = null,
            Expression<Func<TReturnedVM, object>> orderByClause = null, SortOrder sortOrder = SortOrder.Descending,
            params Expression<Func<TTargetEntity, object>>[] includes)
                where TTargetEntity : class;

        IResponse GetList<TListPageVM, TGridItemVM>(SearchModel searchModel, Expression<Func<TEntity, bool>> filter = null);
    }
}
