using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using PrimeAppsDotNet;
using PrimeAppsDotNet.Exceptions;

namespace Sample.Controllers
{
    public class BaseController : Controller
    {
        private PrimeApps _primeApps;
        public static string Authorization { get; set; }
        public static int AppId { get; set; }
        public static int TenantId { get; set; }
        public PrimeApps PrimeApps => _primeApps ?? (_primeApps = GetPrimeAppsClient());

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var requestHeader = context.HttpContext.Request.Headers;

            if (!requestHeader.TryGetValue("Authorization", out var authorization))
            {
                context.Result = new UnauthorizedObjectResult(null);
                return;
            }

            if (!requestHeader.TryGetValue("X-App-Id", out var xAppId))
            {
                context.Result = new BadRequestObjectResult("X-App-Id cannot be null.");
                return;
            }

            if (!requestHeader.TryGetValue("X-Tenant-Id", out var xTenantId))
            {
                context.Result = new BadRequestObjectResult("X-Tenant-Id cannot be null.");
                return;
            }

            if (!int.TryParse(xAppId, out var appId))
            {
                context.Result = new BadRequestObjectResult("X-App-Id must be a number.");
                return;
            }

            if (!int.TryParse(xTenantId, out var tenantId))
            {
                context.Result = new BadRequestObjectResult("X-Tenant-Id must be a number.");
                return;
            }

            Authorization = authorization;
            AppId = appId;
            TenantId = tenantId;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is UnauthorizedException exception)
            {
                context.Result = new UnauthorizedObjectResult(null);
                context.ExceptionHandled = true;
            }
        }

        private PrimeApps GetPrimeAppsClient()
        {
            var configuration = (IConfiguration)HttpContext.RequestServices.GetService(typeof(IConfiguration));
            var memoryCache = (IMemoryCache)HttpContext.RequestServices.GetService(typeof(IMemoryCache));
            var primeapps = new PrimeApps(configuration, memoryCache);

            primeapps.Authorization = Authorization;
            primeapps.AppId = AppId;
            primeapps.TenantId = TenantId;

            return primeapps;
        }
    }
}