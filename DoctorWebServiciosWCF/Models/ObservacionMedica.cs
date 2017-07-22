using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DoctorWebServiciosWCF.Models
{
    [DataContract]
    public class ObservacionMedica
    {
        [DataMember]
        public int ObservacionMedicaId { get; set; }
        [DataMember]
        public String Diagnostico { get; set; }
        [DataMember]
        public String Indicacion { get; set; }
        public virtual Paciente Paciente { get; set; }
    }
}