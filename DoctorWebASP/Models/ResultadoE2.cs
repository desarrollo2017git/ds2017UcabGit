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
    /// Clase de los resultados de examenes medicos
    /// </summary>
    public class ResultadoE2
    {
        [Key]
        //ID de la clase
        public int ResultadoExamenMedicoID { get; set; }
        [Required]
        //Comentario asociado a un resultado de un examen medico
        public String Comentario { get; set; }
        //Cedula de identidad del paciente asociado
        public int Paciente  { get; set; }
    }
}