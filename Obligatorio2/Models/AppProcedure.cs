using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Obligatorio2.Models
{
    
    public class AppProcedure
    { 
        [Key]
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public int Days { get; set; }
        public virtual List<AppGroup> Groups { get; set; } = new List<AppGroup>();
        public virtual List<Stage> Stages { get; set; } = new List<Stage>();

        public AppProcedure()
        {

        }
    }

    public class FileUploadImage
    {
        public bool guardo { get; set; }
        public string fileName { get; set; }
    }

}