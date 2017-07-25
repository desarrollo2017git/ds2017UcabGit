using DoctorWebServiciosWCF.Models.ORM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DoctorWebServiciosWCF.Models
{
    [DataContract]
    /// <summary>
    /// Clase de observaciones medicas en el WCF
    /// </summary>
    public class ObservacionMedicaE2
    {
        [DataMember]
        [Key]
        //ID de la clase
        public int ObservacionMedicaId { get; set; }
        [DataMember]
        //Diagnostico dado por el medico
        public String Diagnostico { get; set; }
        [DataMember]
        //Indicaciones dadas por el medico
        public String Indicacion { get; set; }
        [DataMember]
        //Cedula de identidad del paciente
        public int Paciente  { get; set; }
    }
}