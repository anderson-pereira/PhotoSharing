using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PhotoSharing.Controllers
{
    public class ValueReporter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            LogValues(filterContext.RouteData);
        }

        private void LogValues(RouteData routeData)
        {
            var controller = routeData.Values["controller"];
            var action = routeData.Values["acrion"];

            string message = $"Controller : {controller}; Aciont {action}";

            Debug.WriteLine(message);

            foreach (var item in routeData.Values)
            {
                Debug.WriteLine($"----- Key: {item.Key}; Value {item.Value}");
            }
        }
    }
}