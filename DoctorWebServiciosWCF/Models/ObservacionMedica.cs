using System;
using System.ComponentModel.DataAnnotations;

namespace DoctorWebServiciosWCF.Models
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