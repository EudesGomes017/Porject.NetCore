using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;


namespace Hvex.Exception.ExceptionBase {
    public class FilterExcepetion : IExceptionFilter {
        public void OnException(ExceptionContext context) {
            if (context.Exception is HvexException) {
                ProcessMessageHvexException(context);
            }
            else {
                SendMessageErroUnknown(context);
            }
        }
        private void ProcessMessageHvexException(ExceptionContext context) {
            if (context.Exception is ErroValidatorException) {
                ProcessErroValidatorException(context);
            }
        }
        private void ProcessErroValidatorException(ExceptionContext context) {
            var erroValidatorException = context.Exception as ErroValidatorException;
            if(!erroValidatorException.MesssageError.Contains(ResourceMessageErro.NOT_FOUND)) {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            context.Result = new ObjectResult(new ResponseErroJson(erroValidatorException.MesssageError));
        }
        private void SendMessageErroUnknown(ExceptionContext context) {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ResponseErroJson(ResourceMessageErro.ERRO_UNKNOWN));
        }
    }
}

