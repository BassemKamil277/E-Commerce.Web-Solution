using E_Commerce.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.CustomMiddleWare
{
    public class ExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlerMiddleWare> logger;

        public ExceptionHandlerMiddleWare(RequestDelegate Next , ILogger<ExceptionHandlerMiddleWare> logger)
        {
            next = Next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext); // b2olo ro7 ll MiddleWare ely b3dha lw tmam
                await HandleNotFoundEndPointAsync(httpContext);

            }
            catch (Exception ex) 
            {
                // Logging
                logger.LogError(ex, "Something Went Wrong");

                //Return custom error Msg
                    
                var Probem = new ProblemDetails()
                {
                    Title = "An UnExpected Error Occured",
                    Detail = ex.Message,
                    Instance = httpContext.Request.Path,
                    Status = ex switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        _=> StatusCodes.Status500InternalServerError,
                    }
                };
                httpContext.Response.StatusCode = Probem.Status.Value;
                await httpContext.Response.WriteAsJsonAsync(Probem);
            }
        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound&&!httpContext.Response.HasStarted)
            {
                var Probem = new ProblemDetails()
                {
                    Title = "Error will proccessing the Http Request - EndPoint Not Found",
                    Status = StatusCodes.Status404NotFound,
                    Detail = $"EndPoint {httpContext.Request.Path} Not Found",
                    Instance = httpContext.Request.Path
                };
                await httpContext.Response.WriteAsJsonAsync(Probem);
            }
        }
    }
}
