using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Obligatorio2.Models;

namespace Obligatorio2.Controllers
{
    public class CasesController : Controller
    {
        private ManagementContext db = new ManagementContext();
        
        // GET: Cases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case @case = db.Case.Find(id);
            if (@case == null)
            {
                return HttpNotFound();
            }
            return View(@case);
        }

        //GET:Cases/RequesterCreate/5
        public ActionResult RequesterCreate(string CI)
        {
            Requester req = new Requester();
            req.CI = CI;
            return View(req);
        }

        // POST: Cases/RequesterCreate
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequesterCreate([Bind(Include = "CI,Email,FirstName,Id,LastName,phone")] Requester @requester)
        {
            if (ModelState.IsValid)
            {
                db.Requester.Add(@requester);
                db.SaveChanges();
                return RedirectToAction("CreateCase", new { id = requester.Id});
            }

            return View(@requester);
        }

        //GET:Cases/CreateCase/5
        public ActionResult CreateCase(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var req = db.Requester.Find(id);
            SolicitanteDetailViewModel sol = new SolicitanteDetailViewModel();
            sol.requester = req;
            sol.RequesterId = req.Id;
            sol.procedures = db.AppProcedure.ToList();
            return View(sol);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCase(SolicitanteDetailViewModel @solicitante)
        {
            if (!ModelState.IsValid)
            {
                return View(@solicitante);
            }
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
                Case @case = new Case();
                var id = Convert.ToInt32(Session["idUser"]);
                var user = (from u in db.AppUser
                            where u.Id == id
                            select u).First();
                @case.OfficialEmail = user.Email;
                var req = (from r in db.Requester
                           where r.Id == @solicitante.RequesterId
                           select r).First();
                @case.Requester = req;
                @case.CreatedTime = DateTime.Now;

                var procedure = (from t in db.Procedure
                                 where t.Code == @solicitante.SelectedProcedure.ToString()
                                 select t).First();

                @case.Procedure = procedure;
                db.Case.Add(@case);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                ModelState.AddModelError("", "Catch error in login.");
                return View(@solicitante);
            }
        }


        //GET:Cases/SearchRequester
        public ActionResult SearchRequester()
        {
            if (Session["idUser"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Cases/SearchRequester
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchRequester([Bind(Include = "CI")] SolicitanteViewModel @solicitante)
        {
            if (!ModelState.IsValid)
            {
                return View(@solicitante);
            }
            ApplicationDbContext db = new ApplicationDbContext();
            try
            {
               var requester = (from r in db.Requester
                              where r.CI == @solicitante.CI
                              select r
                              ).First();
                if (requester.Id != 0)
                {
                    //ver detalle de requester
                    return RedirectToAction("CreateCase", new { id = requester.Id});
                }
                else
                {
                    //redirijo a crear requeter
                    return RedirectToAction("RequesterCreate", new { CI = @solicitante.CI});
                }
            }
            catch
            {
                return RedirectToAction("RequesterCreate", new { CI = @solicitante.CI });
            }
        }

        // GET: Cases/Create
        public ActionResult Create()
        {
            return View();
        }

            // POST: Cases/Create
            // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
            // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create([Bind(Include = "Id,OfficialEmail,createdTime")] Case @case)
            {
                if (ModelState.IsValid)
                {
                    db.Case.Add(@case);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(@case);
            }



        // GET: Cases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case @case = db.Case.Find(id);
            if (@case == null)
            {
                return HttpNotFound();
            }
            return View(@case);
        }

        // POST: Cases/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OfficialEmail,createdTime")] Case @case)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@case).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@case);
        }

        // GET: Cases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Case @case = db.Case.Find(id);
            if (@case == null)
            {
                return HttpNotFound();
            }
            return View(@case);
        }

        // POST: Cases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Case @case = db.Case.Find(id);
            db.Case.Remove(@case);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Case
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string searchValue, string filterOption)
        {

            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = searchValue.Trim();
            }

            List<Case> returnList = new List<Case>();

            switch (filterOption)
            {
                case "ciFilter":
                    returnList = db.Case.Where(q => string.Equals(q.Requester.CI, searchValue)).ToList();
                    break;
                case "numberFilter":
                    returnList.Add(db.Case.Find(Convert.ToInt32(searchValue)));
                    break;
                case "byOfficial":
                    returnList = db.Case.Where(q => string.Equals(q.OfficialEmail, searchValue)).ToList();
                    break;
                case "byFullfiled":
                    returnList = db.Case.Where(q => q.Closed == true).ToList();
                    break;
                case "byNotFullfiled":
                    returnList = db.Case.Where(q => q.Closed == false).ToList();
                    break;
                default:
                    returnList = db.Case.ToList();
                    break;

            }

            if (returnList.Count() > 1)
            {

                returnList = returnList.OrderBy(q => q.Requester.CI).ThenByDescending(t => t.CreatedTime).ToList();

            }

            return View(returnList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
