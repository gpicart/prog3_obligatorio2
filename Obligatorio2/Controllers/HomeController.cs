using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Obligatorio2.Models;

namespace Obligatorio2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ManagementContext db = new ManagementContext();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            

            return View();
        }

        public ActionResult DBUpdated()
        {
            ViewBag.Message = "Se han cargado los datos.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}