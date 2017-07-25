using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWebASP.Models
{
    /// <summary>
    /// Clase de las observaciones medicas
    /// </summary>
    public class ObservacionMedicaE2
    {
        [Key]
        //ID de la clase
        public int ObservacionMedicaId { get; set; }
        [Required]
        //Diagnostico asociado a la observacion medica
        public String Diagnostico { get; set; }
        [Required]
        //Indicacion asociada a la observacion medica
        public String Indicacion { get; set; }
        //Cedula de identidad del paciente asociado a la observacion medica 
        public int Paciente  { get; set; }
    }
}