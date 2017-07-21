using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DoctorWebServiciosWCF.Models
{
    [DataContract]
    public class Medico : Persona
    {
        [DataMember]
        [DataType(DataType.Currency)]
        public decimal Sueldo { get; set; }
        [DataMember]
        public virtual EspecialidadMedica EspecialidadMedica { get; set; }
        [DataMember]
        public virtual ICollection<Calendario> Eventos { get; set; }
        [DataMember]
        public virtual CentroMedico CentroMedico { get; set; }
    }
}