using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Obligatorio2.Models;
using System.Reflection;
using System.IO;

namespace Obligatorio2.Controllers
{
    public class FileCreateController : Controller
    {
        ManagementContext db = new ManagementContext();
        // GET: FileCreate
        public ActionResult Index()
        {
            try
            {
                this.ReadGroupFile();
                this.ReadOfficialFile();
                this.ReadProcedureFile();
                this.ReadStageFile();
                this.ReadAssignFile();

                db.SaveChanges();
            }
            catch (Exception e)
            {
                System.Diagnostics.
                    Debug.Assert(false, "Error: " + e.Message);
            }

            return RedirectToAction("DBUpdated", "Home");
        }

        // GET: FileCreate/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FileCreate/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FileCreate/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            return View();
               
        }

        // GET: FileCreate/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FileCreate/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void ReadGroupFile()
        {
            string path = Server.MapPath((@"App_Data\Archivos\todosLosGrupos.txt"));

            string[] lines = System.IO.File.ReadAllLines(path);

            foreach (string line in lines)
            {
                string[] splittedLine = line.Split('#');
                AppGroup g = new AppGroup()
                {
                    Id = Convert.ToInt32(splittedLine[0].Trim()),
                    Name = splittedLine[1].Trim()
                };

                db.AppGroup.Add(g);
            }

        }

        private void ReadOfficialFile()
        {
            string path = Server.MapPath((@"App_Data\Archivos\todosLosUsers.txt"));

            string[] lines = System.IO.File.ReadAllLines(path);

            foreach (string line in lines)
            {
                string[] splittedLine = line.Split('#');
                AppUser u = new AppUser()
                {
                    Name = splittedLine[0].Trim(),
                    Email = splittedLine[1].Trim(),
                    Password = splittedLine[3].Trim()
                };

                string groupName = splittedLine[2].Trim();

                AppGroup g = db.AppGroup.Local.Where(q => string.Equals(q.Name, groupName)).FirstOrDefault();

                u.Group = g;

                db.AppUser.Add(u);
            }

        }

        private void ReadProcedureFile()
        {
            string path = Server.MapPath((@"App_Data\Archivos\datosTramites.txt"));

            string[] lines = System.IO.File.ReadAllLines(path);

            foreach (string line in lines)
            {
                string[] splittedLine = line.Split('|');
                AppProcedure p = new AppProcedure()
                {
                    Code = splittedLine[0].Trim(),
                    Title = splittedLine[1].Trim(),
                    Description = splittedLine[2].Trim(),
                    Days = Convert.ToInt32(splittedLine[3].Trim()),
                    Cost = Convert.ToInt32(splittedLine[4].Trim())
                };

                db.AppProcedure.Add(p);
            }

        }

        private void ReadStageFile()
        {
            string path = Server.MapPath((@"App_Data\Archivos\datosEtapas.txt"));

            string[] lines = System.IO.File.ReadAllLines(path);

            foreach (string line in lines)
            {
                string[] splittedLine = line.Split('@');

                String procId = splittedLine[0].Trim();

                Stage s = new Stage()
                {
                    Code = splittedLine[1].Trim(),
                    Description = splittedLine[2].Trim(),
                    MaxDays = Convert.ToInt32(splittedLine[3].Trim())
                };
                db.Stage.Add(s);

                AppProcedure p = db.AppProcedure.Find(procId);
                p.Stages.Add(s);
            }

        }

        private void ReadAssignFile()
        {
            string path = Server.MapPath((@"App_Data\Archivos\datosAsignaciones.txt"));

            string[] lines = System.IO.File.ReadAllLines(path);

            foreach (string line in lines)
            {
                string[] splittedLine = line.Split('$');

                int groupId = Convert.ToInt32(splittedLine[0].Trim());
                string procId = splittedLine[1].Trim();

                AppProcedure p = db.AppProcedure.Find(procId);
                AppGroup g = db.AppGroup.Find(groupId);

                p.Groups.Add(g);
            }

        }
    }
}
