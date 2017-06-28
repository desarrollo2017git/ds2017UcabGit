using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWebASP.Models
{
    public class ObservacionMedica
    {
        [Key]
        public int ObservacionMedicaId { get; set; }
        [Required]
        public String Diagnostico { get; set; } 
        [Required]
        public String Indicacion { get; set; }
        public virtual Paciente Paciente { get; set; }
    }
}