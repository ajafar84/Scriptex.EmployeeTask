using Scriptex.EmployeeTask.Data.ViewModels.Employee;
using Scriptex.EmployeeTask.Data.ViewModels.Employee.Page;
using Scriptex.EmployeeTask.API.Attributes;
using Scriptex.EmployeeTask.Common.Search;
using System;
using System.Web.Http;
using Scriptex.EmployeeTask.Common.Classes.Response;
using Scriptex.EmployeeTask.Business.Services.Employee;

namespace Scriptex.EmployeeTask.API.Controllers
{
    [RoutePrefix("Employee")]
    public class EmployeeController : BaseController<Data.Models.Employee, EmployeeService, EmployeePostVM>
    {
        public EmployeeController()
        {
            Service = new EmployeeService();
        }

        [HttpGet]
        [Route("Get/{id}")]
        public override IHttpActionResult Get(int? id)
        {
            #region Validations
            if (id == null)
            {
                return Ok(GetResponse(false, ResourceFiles.Employee.InvalidId));
            }
            #endregion

            var response = Service.Get<EmployeeVM>(e => e.Id == id);
            return Ok(GetResponse(true, response.Data));
        }

        public override IHttpActionResult GetDetails(int? id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("GetListPage")]
        [ValidateModelState]
        public override IHttpActionResult GetListPage(SearchModel searchModel)
        {
            var response = Service.GetList<EmployeeListPageVM, EmployeeGridItemVM>(searchModel);
            return Ok(GetResponse(true, response.Data));
        }

        [HttpGet]
        [Route("GetCreatePage")]
        public override IHttpActionResult GetCreatePage()
        {
            var response = Service.GetCreate();
            return Ok(GetResponse(true, response.Data));
        }

        [HttpGet]
        [Route("GetEditPage/{id}")]
        public override IHttpActionResult GetEditPage(int? id)
        {
            #region Validations
            if (id == null)
            {
                return Ok(GetResponse(false, ResourceFiles.Global.InvalidPostedData));
            }
            #endregion

            var response = Service.GetEdit(id.Value);
            return Ok(GetResponse(true, response.Data));
        }

        [HttpPost]
        [Route("Create")]
        [ValidateModelState]
        public override IHttpActionResult Create(EmployeePostVM postVM)
        {
            #region validations
            if (!postVM.Mobile.StartsWith("010") && !postVM.Mobile.StartsWith("011")
                && !postVM.Mobile.StartsWith("012"))
            {
                return Ok(GetResponse(false, ResourceFiles.Employee.InvalidMobileFormat));
            }

            if (postVM.NationalId.StartsWith("0"))
            {
                return Ok(GetResponse(false, ResourceFiles.Employee.InvalidNationalIdFormat));
            }
            #endregion

            var response = Service.Create(postVM);
            if (response.IsSuccess)
            {
                return Ok(GetResponse(true, response.Data, ResourceFiles.Global.SaveSuccess));
            }
            else
            {
                return Ok(GetResponse(false, ResourceFiles.Global.Failure));
            }
        }

        [HttpPut]
        [Route("Edit")]
        [ValidateModelState]
        public override IHttpActionResult Edit(EmployeePostVM postVM)
        {
            #region validations
            if (!postVM.Mobile.StartsWith("010") && !postVM.Mobile.StartsWith("011")
                && !postVM.Mobile.StartsWith("012"))
            {
                return Ok(GetResponse(false, ResourceFiles.Employee.InvalidMobileFormat));
            }

            if (postVM.NationalId.StartsWith("0"))
            {
                return Ok(GetResponse(false, ResourceFiles.Employee.InvalidNationalIdFormat));
            }
            #endregion

            var response = Service.Edit(postVM);
            if (response.IsSuccess)
            {
                return Ok(GetResponse(true, response.Data, ResourceFiles.Global.EditSuccess));
            }
            else
            {
                if ((response as ServiceResponse).FailReason == Common.Enums.FailReason.InvalidId)
                    return Ok(GetResponse(false, ResourceFiles.Employee.InvalidId));
                else return Ok(GetResponse(false, ResourceFiles.Global.Failure));
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public override IHttpActionResult Delete(int? id)
        {
            #region Validations
            if (id == null)
            {
                return Ok(GetResponse(false, ResourceFiles.Employee.InvalidId));
            }
            #endregion

            var response = Service.Delete(id.Value);
            if (response.IsSuccess)
            {
                return Ok(GetResponse(true, response.Data, ResourceFiles.Global.DeleteSuccess));
            }
            else
            {
                if ((response as ServiceResponse).FailReason == Common.Enums.FailReason.InvalidId)
                    return Ok(GetResponse(false, ResourceFiles.Employee.InvalidId));
                else return Ok(GetResponse(false, ResourceFiles.Global.Failure));
            }
        }

        [HttpPut]
        [Route("ChangeStatus")]
        public IHttpActionResult ChangeStatus(EmployeePostVM postVM)
        {
            #region Validations
            if (postVM.Id == 0)
            {
                return Ok(GetResponse(false, ResourceFiles.Employee.InvalidId));
            }
            #endregion

            var response = Service.ChangeStatus(postVM.Id, postVM.IsActive);
            if (response.IsSuccess)
            {
                return Ok(GetResponse(true, response.Data, ResourceFiles.Global.SaveSuccess));
            }
            else
            {
                if ((response as ServiceResponse).FailReason == Common.Enums.FailReason.InvalidId)
                    return Ok(GetResponse(false, ResourceFiles.Employee.InvalidId));
                else return Ok(GetResponse(false, ResourceFiles.Global.Failure));
            }
        }
    }
}
