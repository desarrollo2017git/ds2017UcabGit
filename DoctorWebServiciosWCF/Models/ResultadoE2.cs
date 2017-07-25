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
    /// Clase de resultado de examenes medicos en el WCF
    /// </summary>
	public class ResultadoE2
	{
        [DataMember]
        //ID de la clase
        public int ResultadoExamenMedicoID { get; set; }
        [DataMember]
        //Comentario asociado al resultado de examen medico
        public String Comentario { get; set; }
        [DataMember]
        //Cedula de identidad del paciente
        public string Paciente { get; set; }
    }
}