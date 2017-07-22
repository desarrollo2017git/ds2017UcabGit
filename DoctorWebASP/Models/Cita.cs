using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    /// <summary>
    /// Clase de las citas del centro medico del lado del cliente.
    /// </summary>
    public class Cita
    {
       //Identificador
        public int CitaId { get; set; }
        //Atributo Paciente para el mapeo del Entity Framework (EF)
        public virtual Paciente Paciente { get; set; }
        //Atributo Calendario para el mapeo del Entity Framework (EF)
        public virtual Calendario Calendario { get; set; }
        //Atributo Centro medico para el mapeo del Entity Framework (EF)
        public virtual CentroMedico CentroMedico { get; set; }
        //Atributo Coleccion de tratamientos para el mapeo del Entity Framework (EF)
        public virtual ICollection<Tratamiento> Tratamientos { get; set; }

       
    }
}