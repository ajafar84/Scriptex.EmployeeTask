using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Reflection;
using System.Globalization;
using AutoMapper;
using Scriptex.EmployeeTask.Common.Repositories;
using Scriptex.EmployeeTask.Common.Enums;
using Scriptex.EmployeeTask.Common.Interfaces;
using AutoMapper.QueryableExtensions;
using Scriptex.EmployeeTask.Common.Classes.Response;
using Scriptex.EmployeeTask.Common.Helpers;
using Scriptex.EmployeeTask.Common.Search;
using PagedList;
using Scriptex.EmployeeTask.Business.AutoMapper;

namespace Scriptex.EmployeeTask.Business.Services
{
    public abstract class BaseService<TEntity, TPostVM> : IBaseService<TEntity>
        where TEntity : class
        where TPostVM : class
    {
        protected IMapper _mapper;
        protected IConfigurationProvider _mapConfig;

        protected readonly string _ar;
        protected readonly string _en;
        protected readonly string Culture;
        protected readonly ServiceResponse _response;

        protected IUnitOfWork _unitOfWork;

        public BaseService()
        {
            _response = new ServiceResponse();
            _unitOfWork = new UnitOfWork();

            _ar = CultureCode.ar.ToString();
            _en = CultureCode.en.ToString();
            Culture = CultureInfo.CurrentUICulture.Name;

            InitializeMapper();
        }

        public void InitializeMapper()
        {
            if (_mapper == null)
            {
                AutoMapperConfiguration.Configure();
                _mapper = AutoMapperConfiguration.Mapper;
                _mapConfig = AutoMapperConfiguration.Mapper.ConfigurationProvider;
            }
        }

        #region Abstract Methods
        public abstract IResponse GetCreate();
        public abstract IResponse Create(TPostVM postVM);
        public abstract IResponse GetEdit(int id);
        public abstract IResponse Edit(TPostVM postVM);
        public abstract IResponse Delete(int id);

        #endregion

        #region Get
        public IResponse Get<TReturnedVM>(Expression<Func<TEntity, bool>> whereClause)
        {
            IQueryable<TReturnedVM> result = _unitOfWork
                .BaseRepository<TEntity>()
                .Where(whereClause)
                .ProjectTo<TReturnedVM>(_mapConfig);

            return GetResponse(true, result.FirstOrDefault());
        }

        public IResponse Get<TReturnedVM>(Expression<Func<TEntity, bool>> whereClause,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _unitOfWork
                .BaseRepository<TEntity>()
                .IncludeMultiple(includes)
                .Where(whereClause);

            var result = query.FirstOrDefault();

            return GetResponse(true, _mapper.Map<TEntity, TReturnedVM>(result));
        }


        public IResponse Get<TTargetEntity, TReturnedVM>(Expression<Func<TTargetEntity, bool>> whereClause)
            where TTargetEntity : class
        {
            IQueryable<TReturnedVM> result = _unitOfWork
                .BaseRepository<TTargetEntity>()
                .Where(whereClause)
                .ProjectTo<TReturnedVM>(_mapConfig);

            return GetResponse(true, result.FirstOrDefault());
        }

        public IResponse Get<TTargetEntity, TReturnedVM>(Expression<Func<TTargetEntity, bool>> whereClause,
            params Expression<Func<TTargetEntity, object>>[] includes)
            where TTargetEntity : class
        {
            var query = _unitOfWork
                .BaseRepository<TTargetEntity>()
                .IncludeMultiple(includes)
                .Where(whereClause);

            var result = query.FirstOrDefault();

            return GetResponse(true, _mapper.Map<TTargetEntity, TReturnedVM>(result));
        }
        #endregion

        #region Get List

        public IResponse GetList<TReturnedVM>(Expression<Func<TEntity, bool>> whereClause = null,
            Expression<Func<TReturnedVM, object>> orderByClause = null, SortOrder sortOrder = SortOrder.Descending)
        {
            IQueryable<TEntity> query = _unitOfWork
                .BaseRepository<TEntity>()
                .GetQueryable();
            IQueryable<TReturnedVM> result;

            if (whereClause == null)
                result = query.ProjectTo<TReturnedVM>(_mapConfig);
            else
                result = query.Where(whereClause).ProjectTo<TReturnedVM>(_mapConfig);

            if (orderByClause != null)
            {
                if (sortOrder == SortOrder.Ascending)
                    result = result.OrderBy(orderByClause);
                else if (sortOrder == SortOrder.Descending)
                    result = result.OrderByDescending(orderByClause);
            }

            return GetResponse(true, result.ToList());
        }

        public IResponse GetList<TReturnedVM>(Expression<Func<TEntity, bool>> whereClause = null,
            Expression<Func<TReturnedVM, object>> orderByClause = null, SortOrder sortOrder = SortOrder.Descending,
            params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _unitOfWork
                .BaseRepository<TEntity>()
                .IncludeMultiple(includes);

            IQueryable<TReturnedVM> result;

            if (whereClause == null)
                result = query.ProjectTo<TReturnedVM>(_mapConfig);
            else
                result = query.Where(whereClause).ProjectTo<TReturnedVM>(_mapConfig);

            if (orderByClause != null)
            {
                if (sortOrder == SortOrder.Ascending)
                    result = result.OrderBy(orderByClause);
                else if (sortOrder == SortOrder.Descending)
                    result = result.OrderByDescending(orderByClause);
            }

            return GetResponse(true, result.ToList());
        }

        public IResponse GetList<TTargetEntity, TReturnedVM>(Expression<Func<TTargetEntity, bool>> whereClause = null,
            Expression<Func<TReturnedVM, object>> orderByClause = null, SortOrder sortOrder = SortOrder.Descending)
            where TTargetEntity : class
        {
            IQueryable<TTargetEntity> query = _unitOfWork
                .BaseRepository<TTargetEntity>()
                .GetQueryable();
            IQueryable<TReturnedVM> result;

            if (whereClause == null)
                result = query.ProjectTo<TReturnedVM>(_mapConfig);
            else
                result = query.Where(whereClause).ProjectTo<TReturnedVM>(_mapConfig);

            if (orderByClause != null)
            {
                if (sortOrder == SortOrder.Ascending)
                    result = result.OrderBy(orderByClause);
                else if (sortOrder == SortOrder.Descending)
                    result = result.OrderByDescending(orderByClause);
            }

            return GetResponse(true, result.ToList());
        }

        public IResponse GetList<TTargetEntity, TReturnedVM>(Expression<Func<TTargetEntity, bool>> whereClause = null,
            Expression<Func<TReturnedVM, object>> orderByClause = null, SortOrder sortOrder = SortOrder.Descending,
            params Expression<Func<TTargetEntity, object>>[] includes)
            where TTargetEntity : class
        {
            IQueryable<TTargetEntity> query = _unitOfWork
                .BaseRepository<TTargetEntity>()
                .IncludeMultiple(includes);
            IQueryable<TReturnedVM> result;

            if (whereClause == null)
                result = query.ProjectTo<TReturnedVM>(_mapConfig);
            else
                result = query.Where(whereClause).ProjectTo<TReturnedVM>(_mapConfig);

            if (orderByClause != null)
            {
                if (sortOrder == SortOrder.Ascending)
                    result = result.OrderBy(orderByClause);
                else if (sortOrder == SortOrder.Descending)
                    result = result.OrderByDescending(orderByClause);
            }

            return GetResponse(true, result.ToList());
        }

        public IResponse GetList<TListPageVM, TGridItemVM>(SearchModel searchModel,
            Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _unitOfWork.BaseRepository<TEntity>().GetQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var listPageVM = (TListPageVM)Activator.CreateInstance(typeof(TListPageVM), new object[] { });
            var serchResult = query.ProjectTo<TGridItemVM>(_mapConfig)
                .DynamicSearch(searchModel)
                .ToPagedList(searchModel.PageNumber, searchModel.PageSize);

            List<PropertyInfo> properties = typeof(TListPageVM).GetProperties().ToList();
            foreach (var property in properties)
            {
                if (property.Name == "PagingMetaData")
                    property.SetValue(listPageVM, serchResult.GetMetaData());
                else property.SetValue(listPageVM, serchResult);
            }

            return GetResponse(true, listPageVM);
        }

        #endregion

        #region GetResponse
        public IResponse GetResponse(bool isSuccess, object data)
        {
            _response.IsSuccess = isSuccess;
            _response.Data = data;
            return _response;
        }

        public IResponse GetResponse(bool isSuccess, FailReason failReason)
        {
            _response.IsSuccess = isSuccess;
            _response.Data = null;
            _response.FailReason = failReason;
            return _response;
        }

        public IResponse GetResponse(bool isSuccess, object data, FailReason failReason)
        {
            _response.IsSuccess = isSuccess;
            _response.Data = data;
            _response.FailReason = failReason;
            return _response;
        }
        #endregion
    }
}
