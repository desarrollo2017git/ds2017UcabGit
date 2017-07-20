using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DoctorWebServiciosWCF.Models
{
    [DataContract(IsReference = true)]
    public class Tratamiento
    {
        [DataMember]
        public int TratamientoId { get; set; }
        public virtual Cita Cita { get; set; }
    }
}