using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Obligatorio2.Models;
using Obligatorio2.ViewModels;

namespace Obligatorio2.Controllers
{
    public class AppProcedureController : Controller
    {
        public ManagementContext db = new ManagementContext();

        // GET: AppProcedure
        public ActionResult Index()
        {

            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            AppProcedureViewModel model = new AppProcedureViewModel();

            model.Procedures = db.AppProcedure.ToList();

            return View(model);
        }

        public ActionResult Select(string selectedValue)
        {
            List<Stage> stages = db.AppProcedure.Find(selectedValue).Stages.ToList();

            return PartialView("_Stages", stages);
        }

    }
}