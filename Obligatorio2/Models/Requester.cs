﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Obligatorio2.Models
{
    
    public class Requester
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe ingresar la cédula del solicitante.")]
        public string CI { get; set; }
        [Required(ErrorMessage = "Debe ingresar el nombre del solicitante.")]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Debe ingresar el apellido del solicitante.")]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Debe ingresar el correo del solicitante.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Debe ingresar el teléfono del solicitante.")]
        [Display(Name = "Telefono")]
        public int phone { get; set; }
        
        public Requester()
        {

        }
    }

    public class RequesterViewModel
    {
        public Requester requester { get; set; }
    }
}