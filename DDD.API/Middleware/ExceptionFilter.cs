using DDD.API.Exceptions;
using DDD.API.Models;
using DDD.IoC.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DDD.API.Middleware
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var error = new ErrorModel { Message = context.Exception.Message };

            if (context.Exception is APIException apiException)
            {
                error.StatusCode = apiException.StatusCode;
            }

            var errorMessage = $"{error.StatusCode} {error.Message}";

            if (context.Exception.StackTrace != null)
            {
                Logger.Error(errorMessage, context.Exception.StackTrace);
            }

            if (context.Exception.StackTrace == null)
            {
                Logger.Error(errorMessage);
            }

            context.HttpContext.Response.StatusCode = error.StatusCode;
            context.HttpContext.Response.ContentType = "application/json";

            context.Result = new JsonResult(error);
        }
    }
}
