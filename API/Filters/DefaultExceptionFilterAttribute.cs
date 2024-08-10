using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace criptomoeda.api.Filters
{
    [ExcludeFromCodeCoverage]
    public class DefaultExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context is null) return;

            if (context.Exception is NotFoundException)
            {
                var message = context.Exception.Message;
                var statusCode = HttpStatusCode.NotFound.GetHashCode();
                context.Result = new ObjectResult(new
                {
                    RequestId = Guid.NewGuid().ToString(),
                    Message = message,
                    StatusCode = statusCode.ToString()
                }){ StatusCode = statusCode };
            }

            if (context.Exception is BusinessException)
            {
                var message = context.Exception.Message;
                var statusCode = HttpStatusCode.BadRequest.GetHashCode();
                context.Result = new ObjectResult(new
                {
                    RequestId = Guid.NewGuid().ToString(),
                    Message = message,
                    StatusCode = statusCode.ToString()
                })
                { StatusCode = statusCode };
            }
        }
    }
}
