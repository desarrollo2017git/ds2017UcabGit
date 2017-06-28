using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWebASP.Models
{
    public class ResultadoExamenMedico
    {
        [Key]
        public int ResultadoExamenMedicoID { get; set; }
        [Required]
        public String[] Comentario { get; set; }
        public virtual Paciente Paciente { get; set; }

    }
}