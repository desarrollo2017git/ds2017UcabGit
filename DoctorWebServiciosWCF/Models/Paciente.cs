using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DoctorWebServiciosWCF.Models
{
    [DataContract]
    public class Paciente : Persona
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Paciente()
        {
            this.Seguros = new HashSet<Seguro>();
        }
        [DataMember]
        public string TipoSangre { get; set; }

        public virtual ICollection<HistoriaMedica> HistoriasMedicas { get; set; }
        public virtual ICollection<Cita> Citas { get; set; }
        public virtual ICollection<ObservacionMedica> ObservacionMedicas { get; set; }
        public virtual ICollection<ObservacionDeAtencionClinica> ObservacionDeAtencionClinicas { get; set; }
        public virtual ICollection<ResultadoExamenMedico> ResultadoExamenMedicos { get; set; }
        public virtual ICollection<Seguro> Seguros { get; set; }
    }
}