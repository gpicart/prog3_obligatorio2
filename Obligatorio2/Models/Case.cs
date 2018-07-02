using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

    public class SolicitanteViewModel
    {
        public string CI { get; set; }
        public int Id { get; set; }
    }

    public class SolicitanteDetailViewModel
    {
        public Requester requester { get; set; }
        public int RequesterId { get; set; }
        public List<AppProcedure> procedures { get; set; }
        [Required(ErrorMessage = "Debe seleccionar un tramite.")]
        public int SelectedProcedure { get; set; }


        public IEnumerable<SelectListItem> ProcedureItems
        {
            get { return procedures == null?  new SelectList("") : new SelectList(procedures, "Code", "Title"); }
        }
        
    }

}