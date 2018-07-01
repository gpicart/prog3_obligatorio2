using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Obligatorio2.Models
{
    
    //Se junta con la clase Official
    public class AppUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public virtual AppGroup Group { get; set; }

        [Required(ErrorMessage = "The Name is a required data.")]
        public string Name { get; set; }

        //public AppUser()
        //{

        //}
    }
}