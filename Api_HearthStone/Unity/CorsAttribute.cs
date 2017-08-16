using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Api_HearthStone.Unity
{

    
    public class CorsAttribute : ActionFilterAttribute
    {
        private const string Origin = "Origin";
        private const string AccessControlAllowOrigin = "Access-Control-Allow-Origin";
        private const string originHeaderdefault = "*";
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response.Headers.Add(AccessControlAllowOrigin, originHeaderdefault);
            //HttpContext.Current.Response.Headers.Add(AccessControlAllowOrigin, originHeaderdefault);
        }
    }
}
    
