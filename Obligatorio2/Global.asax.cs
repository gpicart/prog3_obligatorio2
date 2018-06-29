using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Obligatorio2.Models;

namespace Obligatorio2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            //TODO: esto pa que hay que hacerlo?
            var db = new ManagementContext();
            db.Database.Initialize(true);
            db.Database.CreateIfNotExists();
            

            //TODO: usamos esto?
            Session["idUser"] = null;
        }
    }
}
