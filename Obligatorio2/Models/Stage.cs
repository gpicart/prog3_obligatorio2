using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Obligatorio2.Models
{
    
    public class Stage
    {
        [Key]
        public int Id { get; set; }
        public int MaxDays { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool Completed { get; set; }
        public string documentName { get; set; }
        public DateTime completedDate { get; set; }
        
        public Stage()
        {

        }
    }
}