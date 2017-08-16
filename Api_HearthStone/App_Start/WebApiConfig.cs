using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Api_HearthStone
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {


            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "HearthStone/{controller}/{action}/{pageNum}",
            //    defaults: new { pageNum = RouteParameter.Optional }
            //);
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi11",
            //    routeTemplate: "HearthStone/{controller}/{action}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            config.MapHttpAttributeRoutes();
            
        }
    }
}
