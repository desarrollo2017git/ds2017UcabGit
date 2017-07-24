using DoctorWebServiciosWCF.Models.ORM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        [DataMember]
        public int Paciente { get; set; }
    }
}