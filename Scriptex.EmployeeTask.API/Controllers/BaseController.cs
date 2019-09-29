using Scriptex.EmployeeTask.Common.Classes.Response;
using Scriptex.EmployeeTask.Business.Services;
using Scriptex.EmployeeTask.Common.Enums;
using Scriptex.EmployeeTask.Common.Interfaces;
using Scriptex.EmployeeTask.Common.Search;
using System.Globalization;
using System.Web;
using System.Web.Http;

namespace Scriptex.EmployeeTask.API
{
    public abstract class BaseController<TEntity, TService, TPostVM> : ApiController
        where TEntity : class
        where TService : class, IBaseService<TEntity>
        where TPostVM : class
    {
        private IBaseService<TEntity> _service;
        protected readonly ApiResponse _response;

        protected string Culture { get; private set; }

        public BaseController()
        {
            SetCulture();

            _response = new ApiResponse();
        }

        private void SetCulture()
        {
            string ar = CultureCode.ar.ToString();
            string en = CultureCode.en.ToString();
            var requestLanguage = HttpContext.Current.Request.Headers["Language"];

            //if (!string.IsNullOrEmpty(requestLanguage))
            //{
            //    if (requestLanguage.Contains(ar))
            //    {
            //        CultureInfo.CurrentUICulture = new CultureInfo(ar, false);
            //    }
            //    else if (requestLanguage.Contains(en))
            //    {
            //        CultureInfo.CurrentUICulture = new CultureInfo(en, false);
            //    }
            //    else
            //    {
            //        CultureInfo.CurrentUICulture = new CultureInfo(ar, false);
            //    }
            //}

            CultureInfo.CurrentUICulture = new CultureInfo(ar, false);
            Culture = CultureInfo.CurrentUICulture.Name;
        }

        #region Abstract Actions
        public abstract IHttpActionResult Get(int? id);
        public abstract IHttpActionResult GetDetails(int? id);
        public abstract IHttpActionResult GetListPage(SearchModel searchModel);
        public abstract IHttpActionResult GetCreatePage();
        public abstract IHttpActionResult Create(TPostVM postVM);
        public abstract IHttpActionResult GetEditPage(int? id);
        public abstract IHttpActionResult Edit(TPostVM postVM);
        public abstract IHttpActionResult Delete(int? id);
        #endregion

        protected TService Service
        {
            get
            {
                return _service as TService;
            }
            set
            {
                _service = value;
            }
        }

        #region GetResponse
        public IResponse GetResponse(bool isSuccess, object data)
        {
            _response.IsSuccess = isSuccess;
            _response.Data = data;

            return _response;
        }

        public IResponse GetResponse(bool isSuccess, string message)
        {
            _response.IsSuccess = isSuccess;
            _response.Data = null;
            _response.Message = message;

            return _response;
        }

        public IResponse GetResponse(bool isSuccess, object data, string message)
        {
            _response.IsSuccess = isSuccess;
            _response.Data = data;
            _response.Message = message;

            return _response;
        }
        #endregion
    }
}
