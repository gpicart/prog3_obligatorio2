using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Obligatorio2.Models
{
    
    public class Case
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public virtual AppProcedure Procedure { get; set; }
        public string OfficialEmail { get; set; }
        public DateTime CreatedTime { get; set; }

        public virtual Requester Requester { get; set; }

        public bool Closed { get; set; }

        public Case()
        {

        }
    }
}