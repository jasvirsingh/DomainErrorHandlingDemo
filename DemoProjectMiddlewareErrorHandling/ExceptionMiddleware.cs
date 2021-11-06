using App.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DemoProjectMiddlewareErrorHandling
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (App.Domain.DomainNotFoundException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync(new ErrorDetails
                { 
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message =  ex.Message
                }.ToString());
            }
            catch (App.Domain.DomainValidationException ex)
            {
                // context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                // await context.Response.WriteAsync(ex.Message);
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = ex.Message
                }.ToString());
            }
            catch (Exception ex)
            {
                //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                // await context.Response.WriteAsync(ex.Message);
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                }.ToString());
            }
        }
    }
}
