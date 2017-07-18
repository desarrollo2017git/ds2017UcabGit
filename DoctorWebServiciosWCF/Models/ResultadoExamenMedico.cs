using System;
using System.ComponentModel.DataAnnotations;

namespace DoctorWebServiciosWCF.Models
{
    public class ResultadoExamenMedico
    {
        [Key]
        public int ResultadoExamenMedicoID { get; set; }
        [Required]
        public String Comentario { get; set; }
        public virtual Paciente Paciente { get; set; }

    }
}