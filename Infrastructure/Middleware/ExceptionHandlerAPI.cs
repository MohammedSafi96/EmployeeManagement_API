using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
   public class ExceptionHandlerAPI
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerAPI(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                //you can save the below details to log file of DB log...
                DateTime Date = DateTime.Now;
                string MachineIPAddress = httpContext.Connection.RemoteIpAddress.ToString();
                string MachinePort = httpContext.Connection.RemotePort.ToString();
                string RequestUrl = httpContext.Request.Path.Value;
                string Message = exception.InnerException != null ? exception.InnerException.Message : exception.Message;
                string RequestBody = string.Empty;
                using (StreamReader stream = new StreamReader(httpContext.Request.Body))
                {
                    RequestBody = await stream.ReadToEndAsync();
                }
                
                await HandleExceptionAsync(httpContext, exception);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)StatusCodes.Status500InternalServerError; ;
            await context.Response.WriteAsync(new
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }
    }

    public static class ExceptionHandlerAPIExtensions
    {
        public static void UseExceptionMiddlewareAPI(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerAPI>();
        }
    }
}

