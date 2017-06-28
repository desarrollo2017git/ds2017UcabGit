using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWebASP.Models
{
    public class ObservacionDeAtencionClinica
    {
        public enum Tipo
        {
            PresionArterial,
            Temperatura,
            Medicacion,
        }

        [Key]
        public int ObservacionDeAtencionMedicaId { get; set; }
        [Required]
        public string Observacion { get; set; }
        [Required]
        public string Comentario { get; set; }
        [Required]
        public Tipo tipo { get; set; }
        public virtual Paciente Paciente { get; set; }

    }
}
