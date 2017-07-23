using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    public class Seguro
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public virtual ICollection<Paciente> Pacientes { get; set; }
    }
}