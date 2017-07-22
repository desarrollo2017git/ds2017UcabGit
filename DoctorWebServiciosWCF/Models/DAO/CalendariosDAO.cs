using DoctorWebServiciosWCF.Helpers;
using DoctorWebServiciosWCF.Models.ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.DAO
{
    public class CalendariosDAO : DAO<Calendario>, ICalendariosDAO
    {
        public CalendariosDAO() : base()
        {
            coleccion = db.Calendarios;
        }

        /// <summary>
        /// Metodo del DAO para obtener medicos a partir de su identificador de usuario
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <returns>Medico</returns>
        public List<Medico> ObtenerMedico(string userId)
        {
            return db.Personas.OfType<Medico>().Where(p => p.ApplicationUserId == userId).ToList() ;
        }

        /// <summary>
        /// Metodo del DAO para obtener pacientes a partir de su identificador de usuario
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <returns>Paciente</returns>
        public List<Paciente> ObtenerPaciente(string userId)
        {
            return db.Personas.OfType<Paciente>().Where(p => p.ApplicationUserId == userId).ToList() ;
        }

        /// <summary>
        /// Metodo del DAO para obtener pacientes a partir de su identificador de usuario
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <returns>Paciente</returns>
        public List<Calendario> ObtenerTiempoDoctor(int medicoid)
        {
            return db.Calendarios.Where(c => c.Medico.PersonaId == medicoid && c.Disponible == 1).ToList();
        }


        /// <summary>
        /// Metodo del DAO para obtener pacientes a partir de su identificador de usuario
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <returns>Paciente</returns>
        public List<Calendario> ObtenerCitasDoctor(int medicoid)
        {
            return db.Calendarios.Where(c => c.Medico.PersonaId == medicoid && c.Disponible == 0 && c.Cancelada == false).ToList();
        }


    }
}