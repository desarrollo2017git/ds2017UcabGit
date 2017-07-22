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
    /// Clase de las citas del centro medico del lado del servidor
    /// </summary>
    public class Cita
    {
        [DataMember]
        //Identificador
        public int CitaId { get; set; }
        [DataMember]
        //Atributo Paciente para el mapeo del Entity Framework (EF)
        public virtual Paciente Paciente { get; set; }
        [DataMember]
        //Atributo Calendario para el mapeo del Entity Framework (EF)
        public virtual Calendario Calendario { get; set; }
        [DataMember]
        //Atributo Centro medico para el mapeo del Entity Framework (EF)
        public virtual CentroMedico CentroMedico { get; set; }
        [DataMember]
        //Atributo Coleccion de tratamientos para el mapeo del Entity Framework (EF)
        public virtual ICollection<Tratamiento> Tratamientos { get; set; }

       
    }
}