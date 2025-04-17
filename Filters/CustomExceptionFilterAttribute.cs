using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using NLog;
using ILogger = NLog.ILogger;

namespace KnowCloud.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public void OnException(ExceptionContext context)
        {
            Logger.Error(context.Exception, "Ah ocurrido una excepcion en la solicitud.");

            context.Result = new ObjectResult(new { message = "An error occurred while processing your request." })
            {
                StatusCode = 500
            };

            context.ExceptionHandled = true;
        }
    }
}
