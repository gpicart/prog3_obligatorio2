using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Obligatorio2.Models;

namespace Obligatorio2.ViewModels
{
    public class AppProcedureViewModel
    {
        public int SelectedValue { get; set; }
        public List<AppProcedure> Procedures { get; set; } = new List<AppProcedure>();
    }


    public class ProcedureViewModel
    {
        public Case cases { get; set; }
        public int caseId { get; set; }
        public string procedureId { get; set; }
        public AppProcedure procedure { get; set; }
        public List<Stage> stages { get; set; }
        public int stageId { get; set; }
        public List<AppUser> users { get; set; }
        public int userId { get; set; }
        public string fileName { get; set; }
    }
}