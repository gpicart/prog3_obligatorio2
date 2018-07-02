using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Obligatorio2.Models;

namespace Obligatorio2.Controllers
{
    public class CaseController : Controller
    {

        public ManagementContext db = new ManagementContext();

        // GET: Case
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string searchValue, string filterOption)
        {

            List<Case> returnList = new List<Case>();

            switch(filterOption)
            {
                case "ciFIlter":
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

            returnList = returnList.OrderBy(q => q.Requester.CI).ThenByDescending(t => t.CreatedTime).ToList();

            return View(returnList);
        }
    }
}