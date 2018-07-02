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
        private AppUser user = new AppUser();

        // GET: Cases
        public ActionResult Index()
        {
            return View(db.Case.ToList());
        }

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
                return RedirectToAction("DatosUsuario", new { id = requester.Id});
            }

            return View(@requester);
        }

        //GET:Cases/DatosUsuario/5
        public ActionResult DatosUsuario(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var req = db.Requester.Find(id);
            SolicitanteDetailViewModel sol = new SolicitanteDetailViewModel();
            sol.requester = req;
            sol.cases = new Case();
            return View(sol);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DatosUsuario([Bind(Include = "Email,Id")] SolicitanteDetailViewModel @solicitante)
        {
           /* Case @case = new Case();
            @case.OfficialEmail = @solicitante.Email;*/
            return View();
        }


        //GET:Cases/SearchRequester
        public ActionResult SearchRequester()
        {
            return View();
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
                    return RedirectToAction("DatosUsuario", new { id = requester.Id});
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
