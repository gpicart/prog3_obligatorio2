using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Obligatorio2.Models
{
    
    public class AppGroup
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<AppProcedure> procedures { get; set; } = new List<AppProcedure>();

        public AppGroup()
        {

        }
    }
}