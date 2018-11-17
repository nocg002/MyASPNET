using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApp1 {
    /// <summary>
    /// URL Routing路由
    /// </summary>
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            //忽略路由
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();
            
            /* 自訂路由約束 
             * Authors/Index
             * Publishers/Index/3
             */
            /*routes.MapRoute(
                name: "Index", 
                url: "{controller}/{action}/{page}",
                defaults: new { page = 1 },
                constraints: new { controller = "(Author|Publisher)", action = "Index", page = "\\d+" }
            );*/

            /* 自訂路由約束 
             * Publishers/Edit/b36e5912-5205-4cf4-ae44-03397debf2b2
             * Publishers/Delete/b36e5912-5205-4cf4-ae44-03397debf2b2
             * Publishers/Details/b36e5912-5205-4cf4-ae44-03397debf2b2
             */
            /*routes.MapRoute(
                name: "Edit", 
                url: "{controller}/{action}/{id}",
                defaults: new {  },
                //id 被約束成 GUID
                constraints: new { controller = "(Author|Publisher)", action = "(Edit|Delete|Details)", id = new GuidConstraint() }
            );*/



            //C# 4.0 具名參數
            /* Home
             * Index: localhost
             * About: localhost/About
             * Contact: localhost/Contact
             * 其他: localhost/Account/Login
            */
            //對應路由
            /*routes.MapRoute(
                name: "Home", 
                url: "{action}/{id}", //網址樣式移除了{controller} 就必須吃預設值
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                //constraints限制: 利用正規表示式Regex完整比對, 允許About和Contact
                constraints: new { action = "(Index|About|Contact)" }, 
                //namespace限制: 限制只有命名空間相同的才比對成功, 通常應用於Area
                namespaces: new[] { "WebApplication1.Controllers" }
            );*/



            /*
             * Publishers/Create
             */
            //MapRoute 定義網址樣式
            /*routes.MapRoute(
                name: "Default", //Route的名稱, 必須是唯一
                url: "{controller}/{action}",//{id}
                defaults: new { controller = "Home", action = "Index" } //, id = UrlParameter.Optional
            );*/

            routes.MapRoute(
                name: "Default", 
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional } 
            );
        }




        //Constraints 自訂的路由約束條件, 判斷Guid
        public class GuidConstraint : IRouteConstraint {
            public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, 
                RouteDirection routeDirection) {
                #region
                //先確定Route中有我們設定的變數
                if (values.ContainsKey(parameterName)) {
                    var guid = values[parameterName] as Guid?;
                    if (guid.HasValue == false) {
                        var stringValue = values[parameterName] as string;
                        if (string.IsNullOrWhiteSpace(stringValue) == false) {
                            Guid parsedGuid;
                            // .NET 4 新增的 Guid.TryParse
                            Guid.TryParse(stringValue, out parsedGuid);
                            guid = parsedGuid;
                        }
                    }
                    return (guid.HasValue && guid.Value != Guid.Empty);
                }
                return false;
                #endregion
            }
        }

    }
}
