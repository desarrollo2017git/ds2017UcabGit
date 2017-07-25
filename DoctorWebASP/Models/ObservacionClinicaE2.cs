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
    /// Clase de las observaciones de atencion clinica
    /// </summary>
	public class ObservacionClinicaE2
	{
        [Key]
        //ID de la clase
        public int ObservacionDeAtencionMedicaId { get; set; }
        [Required]
        //Observacion asociada a la observacion clinica
        public string Observacion { get; set; }
        [Required]
        //Comentario asociado a la observacion clinica
        public string Comentario { get; set; }
        [Required]
        //Tipo asociado a la atencion clinica
        public string Tipo { get; set; }
        //Cedula de identidad del paciente asociado a la observacion clinica
        public int Paciente  { get; set; }
    }
}