using DoctorWebServiciosWCF.Models.ORM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models
{
    public class Cita
    {
        public int CitaId { get; set; }

        public virtual Paciente Paciente { get; set; }
        public virtual Calendario Calendario { get; set; }
        public virtual CentroMedico CentroMedico { get; set; }
        public virtual ICollection<Tratamiento> Tratamientos { get; set; }

        private ContextoBD db = new ContextoBD();

        public Boolean isDoctor(string id)
        {
            var user = db.Personas.OfType<Medico>().Where(p => p.ApplicationUserId == id);
            if (user == null)
            {
                return false;
            }
            return true;
        }
    }
}