using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DoctorWebServiciosWCF.Models
{
    [DataContract]
    public class CentroMedico
    {
        [DataMember]
        public int CentroMedicoId { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Rif { get; set; }
        [DataMember]
        public string Direccion { get; set; }
        [DataMember]
        public string Telefono { get; set; }

        public virtual ICollection<Cita> Citas { get; set; }
        public virtual ICollection<Medico> Medicos { get; set; }
    }
}