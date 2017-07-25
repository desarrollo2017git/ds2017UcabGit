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
    public class ObservacionClinicaE2
    {

        [DataMember]
        [Key]
        //ID de la clase
        public int ObservacionDeAtencionMedicaId { get; set; }
        [DataMember]
        //Observacion clinica dada por el medico
        public string Observacion { get; set; }
        [DataMember]
        //Comentario a la atencion clinica dada por el medico
        public string Comentario { get; set; }
        [DataMember]
        //Tipo de atencion clinica
        public string Tipo { get; set; }
        [DataMember]
        //Cedula de identidad del paciente
        public int Paciente  { get; set; }
    }
}