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
    /// <summary>
    /// Clase de observaciones medicas en el WCF
    /// </summary>
    public class ObservacionMedicaE2
    {
        [DataMember]
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