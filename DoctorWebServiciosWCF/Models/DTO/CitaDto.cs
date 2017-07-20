using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.DTO
{
    public class CitaDto
    {
        public int CitaId { get; set; }

        public virtual Paciente Paciente { get; set; }
        public virtual Calendario Calendario { get; set; }
        public virtual CentroMedico CentroMedico { get; set; }
        public virtual ICollection<Tratamiento> Tratamientos { get; set; }
    }
}