using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DoctorWebServiciosWCF.Models
{
    [DataContract(IsReference = true)]
    public class Paciente : Persona
    {
        [DataMember]
        public string TipoSangre { get; set; }

        public virtual ICollection<HistoriaMedica> HistoriasMedicas { get; set; }
        public virtual ICollection<Cita> Citas { get; set; }
        public virtual ICollection<ObservacionMedica> ObservacionMedicas { get; set; }
        public virtual ICollection<ObservacionDeAtencionClinica> ObservacionDeAtencionClinicas { get; set; }
        public virtual ICollection<ResultadoExamenMedico> ResultadoExamenMedicos { get; set; }
    }
}