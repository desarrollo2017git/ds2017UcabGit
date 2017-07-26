using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    public class Paciente : Persona
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Paciente()
        {
            this.Seguros = new HashSet<Seguro>();
        }
        public virtual string TipoSangre { get; set; }
        public virtual ICollection<Seguro> Seguros { get; set; }
        public virtual ICollection<HistoriaMedica> HistoriasMedicas { get; set; }
        public virtual ICollection<Cita> Citas { get; set; }
    }
}