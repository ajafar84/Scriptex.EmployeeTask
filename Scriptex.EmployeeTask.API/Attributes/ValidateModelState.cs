using Scriptex.EmployeeTask.Common.Classes.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Scriptex.EmployeeTask.API.Attributes
{
    public class ValidateModelState : ActionFilterAttribute
    {
        private readonly Predicate<Dictionary<string, object>> CheckForNull = args => args.ContainsValue(null);
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (CheckForNull(actionContext.ActionArguments) || !actionContext.ModelState.IsValid)
                actionContext.Response = actionContext.Request
                     .CreateResponse(System.Net.HttpStatusCode.OK, new ApiResponse()
                     {
                         IsSuccess = false,
                         Message = ResourceFiles.Global.InvalidPostedData
                     });
        }
    }
}