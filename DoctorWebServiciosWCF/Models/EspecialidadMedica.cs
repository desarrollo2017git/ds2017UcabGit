using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DoctorWebServiciosWCF.Models
{
    [DataContract]
    public class EspecialidadMedica
    {
        [DataMember]
        public int EspecialidadMedicaId { get; set; }
        [DataMember]
        public string Nombre { get; set; }

        public virtual ICollection<Medico> Medicos { get; set; }
    }
}