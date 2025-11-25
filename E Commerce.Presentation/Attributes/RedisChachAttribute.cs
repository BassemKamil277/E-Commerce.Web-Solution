using E_Commerce.Services_Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Attributes
{
    internal class RedisChachAttribute : ActionFilterAttribute
    {
        private readonly int durationInMin;

        public RedisChachAttribute(int DurationInMin = 5) // 34an el time span yb2a flexable
        {
            durationInMin = DurationInMin;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // el step dy zy eny 3mlt constractor w a5t ref mn el interface
            var CacheSrevice = context.HttpContext.RequestServices.GetRequiredService<ICacheSrevice>();
            var CacheKey = CreateCacheKey(context.HttpContext.Request);  // el key 
            var CacheValue = await CacheSrevice.GetAsync(CacheKey);
            if(CacheValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = CacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }
            var ExecutedContext =  await next.Invoke();
            if(ExecutedContext.Result is OkObjectResult result)
            {
                await CacheSrevice.SetAsync(CacheKey , result.Value , TimeSpan.FromMinutes(durationInMin));
            }


        }

         //  /api/Products
        //  /api/Products?typeId=1
       //  /api/Products?brandId=2
      //  /api/Products?typeId=1&brandId=2
        private string CreateCacheKey(HttpRequest request)
        {
            StringBuilder Key = new StringBuilder(); // 34an ynf3 a3dl 3la el value
            Key.Append(request.Path); // /api/Products
            foreach(var item in request.Query.OrderBy(x=> x.Key))
                Key.Append($"|{item.Key}-{item.Value}");
            return Key.ToString();
                


        }
    }
}
