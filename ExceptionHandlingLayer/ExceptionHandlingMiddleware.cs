using ExceptionHandlingLayer.USERFRIENDLYEXCEPTIONS;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExceptionHandlingLayer
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {

        /*
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (DuplicateRecordException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync(ex.Message);
            }
        }
        */


        //ALTERNATEIVELY YOU CAN ADD THE ABOVE MULTIPLE CATCH BLOCK WITH A PRIVATE FUNCTION AS SHOWN BELOW

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context,Exception ex)
        {
            context.Response.ContentType = "application/json";
            var exceptionData = GetExceptionDetails(ex);
            context.Response.StatusCode = exceptionData.StatusCode;
            await context.Response.WriteAsync(exceptionData.ToString());
        }  
        
        private ResponseModel GetExceptionDetails(Exception ex)
        {
            var model = new ResponseModel();
            var type = ex.GetType().ToString();
            switch (type)
            {
                case "NotFoundException":
                    model.StatusCode = (int)HttpStatusCode.NotFound;
                    model.Message = ex.Message;
                    break;
                
                case "DuplicateRecordException":
                    model.StatusCode = (int)HttpStatusCode.NotFound;
                    model.Message = ex.Message;
                    break;

                default:
                    model.StatusCode = (int)HttpStatusCode.InternalServerError;
                    model.Message = ex.Message;
                    break; 
            }
            return model;
        }

        private ResponseModel GetExceptionDetailss(Exception exception)
        {
            var model = new ResponseModel();
            var exceptionType = exception.GetType();

            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                model.StatusCode = (int)HttpStatusCode.Unauthorized;
                model.Message = exception.Message;
            }
            else if (exceptionType == typeof(BadResultException))
            {
                model.StatusCode = (int)HttpStatusCode.BadRequest;
                model.Message = exception.Message;
            }
            else if (exceptionType == typeof(RecordNotFoundException)
                || exceptionType == typeof(DuplicateRecordException))
            {
                model.StatusCode = (int)HttpStatusCode.PreconditionFailed;
             //   model.Message = ContentLoader.ReturnLanguageData(exception.Message, "");
            }
            else
            {
                model.StatusCode = (int)HttpStatusCode.InternalServerError;
                model.Message = exception.Message;
            }
            return model;
        }

        public class ResponseModel
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }
            public object Data { get; set; }
            public List<Errors> Errors { get; set; }
            public override string ToString()
            {
                return JsonSerializer.Serialize(this);
            }
        }
        public class Errors
        {
            public string PropertyName { get; set; }
            public string[] ErrorMessages { get; set; }
        }


    }
}
