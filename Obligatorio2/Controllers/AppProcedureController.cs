using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Obligatorio2.Models;
using Obligatorio2.ViewModels;
using System.Data.Entity;

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

            List<AppUser> users = new List<AppUser>();

            AppProcedure selectedProcedure = new AppProcedure();

            foreach(AppGroup g in selectedProcedure.Groups)
            {
                users.AddRange(db.AppUser.Where(q => q.Group.Id == g.Id).ToList());
            }

            return PartialView("_Stages", stages);
        }

        public ActionResult SearchCase()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View("SearchCase");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchCase(ProcedureViewModel @vmProcedure)
        {
            if (ModelState.IsValid)
            {
                try {
                    var cas = (from c in db.Case
                                where c.Id == vmProcedure.cases.Id
                                select c).First();
                    vmProcedure.cases = cas;
                    vmProcedure.caseId = cas.Id;
                    return RedirectToAction("ListStagesView", new { idCase = cas.Id});
                }
                catch
                {
                    ModelState.AddModelError("", "No existe el expediente con ese codigo");
                    return View();
                }
            }

            return View(@vmProcedure);
        }

        public ActionResult ListStagesView(string idCase)
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                var casId = Convert.ToInt32(idCase);
                var cas = (from c in db.Case
                           where c.Id == casId
                           select c).First();

                var proc = (from p in db.AppProcedure
                            where p.Code == cas.Procedure.Code.ToString()
                            select p).First();
                ProcedureViewModel model = new ProcedureViewModel();
                model.procedure = proc;
                model.cases = cas;
                return View(model);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "No existe el expediente con ese codigo. " + ex.Message);
                return View("SearchCase", "AppProcedure");
            }
        }


        public ActionResult ListOfficialView(string idStage, string idCase)
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                ProcedureViewModel model = new ProcedureViewModel();
                var casId = Convert.ToInt32(idCase);
                var cas = (from c in db.Case
                           where c.Id == casId
                           select c).First();

                var proc = (from p in db.AppProcedure
                            where p.Code == cas.Procedure.Code.ToString()
                            select p).First();
                model.procedure = proc;
                model.cases = cas;

                List<AppUser> users = new List<AppUser>();
                foreach(AppGroup group in proc.Groups)
                {
                    users.AddRange(db.AppUser.Where(q => q.Group.Id == group.Id).ToList());
                }
                model.users = users;
                model.caseId = Convert.ToInt32(idCase);
                model.stageId = Convert.ToInt32(idStage);
                return View(model);
            }
            catch
            {
                ModelState.AddModelError("", "Error al mostrar funcionarios");
                return View();
            }
        }

        public ActionResult UploadImageView (string idStage, string idCase, string idOfficial)
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                ProcedureViewModel model = new ProcedureViewModel();
                model.userId = Convert.ToInt32(idOfficial);
                model.stageId = Convert.ToInt32(idStage);
                var casId = Convert.ToInt32(idCase);
                var cases = (from c in db.Case
                           where c.Id == casId
                           select c).First();

                var stage = (from s in db.Stage
                             where s.Id == model.stageId
                             select s).First();

                var official = (from u in db.AppUser
                                where u.Id == model.userId
                                select u).First();

                stage.Completed = true;
                stage.completedDate = DateTime.Now;

                if ((stage.completedDate - cases.CreatedTime).Value.Days > stage.MaxDays)
                {
                    //envio aviso
                    ModelState.AddModelError("", "se supero los dias");
                }

                cases.OfficialEmail = official.Email;
                var count = 0;
                foreach(Stage sta in cases.Procedure.Stages)
                {
                    if (sta.Completed != null && sta.Completed == true)
                    {
                        count++;
                    }
                }
                if (cases.Procedure.Stages.Count == count)
                {
                    cases.Closed = true;
                }
                db.Entry(stage).State = EntityState.Modified;
                db.Entry(cases).State = EntityState.Modified;
                db.SaveChanges();
                return View(model);
            }
            catch
            {
                ModelState.AddModelError("", "Error al mostrar funcionarios");
                return View();
            }
        }

        [HttpPost]
        public ActionResult UploadImageView(HttpPostedFileBase file, string stageId)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    FileUploadImage img = guardarArchivo(file);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return View();
        }

        private FileUploadImage guardarArchivo(HttpPostedFileBase archivo)
        {
            FileUploadImage f = new FileUploadImage();

            if (archivo != null)
            {
                string ruta = System.IO.Path.Combine
                (AppDomain.CurrentDomain.BaseDirectory, "Content/docs");
                if (!System.IO.Directory.Exists(ruta))
                    System.IO.Directory.CreateDirectory(ruta);
                ruta = System.IO.Path.Combine(ruta, archivo.FileName);
                archivo.SaveAs(ruta);
                f.guardo = true;
                f.fileName = archivo.FileName;
                return f;
            }
            else
            {
                f.guardo = false;
                return f;
            }
       }
        
    }
}