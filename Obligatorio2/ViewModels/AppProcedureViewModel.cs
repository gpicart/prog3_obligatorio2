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
}