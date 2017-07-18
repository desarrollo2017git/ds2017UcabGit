using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models
{
    public class HistoriaMedica
    {

        
        public int HistoriaMedicaId { get; set; }

        public virtual Paciente Paciente { get; set; }



    }
}