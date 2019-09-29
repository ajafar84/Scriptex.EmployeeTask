using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace Scriptex.EmployeeTask.API.Helpers
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            //Logger.Log.Error(context.Request.RequestUri + ": " + context.Exception.Message + ": " +
            //    (context.Exception.InnerException != null ? context.Exception.InnerException.Message : ""));
            //context.Result = new InternalServerErrorResult(context.Request);
        }
    }
}