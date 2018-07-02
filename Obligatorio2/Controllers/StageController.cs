using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Obligatorio2.Models;
using Obligatorio2.ViewModels;

namespace Obligatorio2.Controllers
{
    public class StageController : Controller
    {
        ManagementContext db = new ManagementContext();
        // GET: Stage
        public ActionResult Index(string id)
        {

            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            List<Stage> stages = db.AppProcedure.Find(id).Stages.ToList();
            List<StageViewModel> response = new List<StageViewModel>();

            foreach(var s in stages)
            {
                StageViewModel viewM = new StageViewModel();
                viewM.Path = "file:///" + Server.MapPath((@"~\App_Data\docs\") + s.documentName);
                viewM.Stage = s;

                response.Add(viewM);
            }

            return View(response);
        }
    }
}