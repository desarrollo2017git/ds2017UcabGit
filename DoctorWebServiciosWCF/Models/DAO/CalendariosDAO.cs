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
        /// <summary>
        /// Metodo del DAO para obtener medicos a partir de su identificador de usuario
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <returns>Medico</returns>
        public Calendario GuardarCalendario(Calendario calendario)
        {
            calendario.HoraFin = calendario.HoraInicio.AddHours(2);
            calendario.Cancelada = false;
            calendario.Disponible = 1;
            // Creamos la cita utilizando comando Crear
            if (HorarioValidoCalendario(calendario))
            {
                Crear(calendario);
                return calendario;
            }
            else
                return null;
        }

        public Calendario EliminarCalendario(Calendario calendario)
        {
            try
            {
                var calendarioTmp = ObtenerPrimeroQue(c => c.CalendarioId == calendario.CalendarioId);
                if (calendarioTmp != null)
                {
                    Borrar(calendarioTmp);
                    return calendarioTmp;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// Metodo del DAO para obtener medicos a partir de su identificador de usuario
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <returns>Medico</returns>
        public List<Medico> ObtenerMedico(string userId)
        {
            var daoPersonas = Utilidades.Instancia.Fabrica.CrearDAO<Persona>();
            return daoPersonas.ObtenerTodos().OfType<Medico>().Where(p => p.ApplicationUserId == userId).ToList();
        }

        /// <summary>
        /// Metodo del DAO para obtener pacientes a partir de su identificador de usuario
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <returns>Paciente</returns>
        public List<Paciente> ObtenerPaciente(string userId)
        {
            return db.Personas.OfType<Paciente>().Where(p => p.ApplicationUserId == userId).ToList();
        }

        /// <summary>
        /// Metodo del DAO para obtener pacientes a partir de su identificador de usuario
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <returns>Paciente</returns>
        public List<Calendario> ObtenerTiempoDoctor(int medicoid)
        {
            var Calendarios = Utilidades.Instancia.Fabrica.CrearDAO<Calendario>();
            return Calendarios.ObtenerTodos().Where(c => c.Medico.PersonaId == medicoid && c.Disponible == 1).ToList();
        }


        /// <summary>
        /// Metodo del DAO para obtener pacientes a partir de su identificador de usuario
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <returns>Paciente</returns>
        public List<Calendario> ObtenerCitasDoctor(int medicoid)
        {
            var Calendarios = Utilidades.Instancia.Fabrica.CrearDAO<Calendario>();
            return Calendarios.ObtenerTodos().Where(c => c.Medico.PersonaId == medicoid && c.Disponible == 0 && c.Cancelada == false).ToList();
        }

        public Paciente ObtenerPacienteCalendario(int calendarioId)
        {
            int pacienteId = db.Citas.Where(c => c.CitaId == calendarioId).Select(p => p.Paciente.PersonaId).Single();
            return db.Personas.OfType<Paciente>().Single(p => p.PersonaId == pacienteId);


        }
        /// <summary>
        /// Metodo del DAO para obtener medicos a partir de su identificador de usuario
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <returns>Medico</returns>
        public bool HorarioValidoCalendario(Calendario calendario)
        {
            var Calendarios = Utilidades.Instancia.Fabrica.CrearDAO<Calendario>();
            var Calendarios2 = Utilidades.Instancia.Fabrica.CrearDAO<Calendario>();
            var calendarios = db.Calendarios.Where(c => c.Medico.PersonaId == calendario.Medico.PersonaId && c.HoraInicio <= calendario.HoraInicio && c.HoraFin > calendario.HoraInicio);
            var calendarios2 = db.Calendarios.Where(c => c.Medico.PersonaId == calendario.Medico.PersonaId && c.HoraInicio < calendario.HoraFin && c.HoraFin >= calendario.HoraFin);
            if (((calendarios.Count() == 0) && (calendarios2.Count() == 0)) && (calendario.HoraInicio >= System.DateTime.Now))
                return true;
            else
                return false;
        }

    }
}