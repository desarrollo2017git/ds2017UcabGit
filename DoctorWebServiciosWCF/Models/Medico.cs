using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DoctorWebServiciosWCF.Models
{
    [DataContract(IsReference = true)]
    public class Medico : Persona
    {
        [DataMember]
        [DataType(DataType.Currency)]
        public decimal Sueldo { get; set; }

        public virtual EspecialidadMedica EspecialidadMedica { get; set; }
        public virtual ICollection<Calendario> Eventos { get; set; }
        public virtual CentroMedico CentroMedico { get; set; }
    }
}