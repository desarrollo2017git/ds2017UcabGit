using System;
using System.ComponentModel.DataAnnotations;


namespace DoctorWebServiciosWCF.Models
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
        //  public virtual Paciente Paciente { get; set; }


    }
}
