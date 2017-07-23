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
            // Procedemos a actualizar la disponibilidad del calendario
            // Para esto, debemos recuperar el objeto en la base de datos
            // luego cambiamos su contenido por el del calendario que recibimos cuya disponibilidad es 0  
            // usando el comando Actualizar
            /*var daoCalendario = Fabrica.CrearDAO<Calendario>();
            daoCalendario.Actualizar(calendario, registro => registro.CalendarioId == calendario.CalendarioId);

            // NOTA: Igual aca
            var notificacionDAO = Fabrica.CrearNotificacionDAO();
            try
            {
                var notificacion = notificacionDAO.Obtener("GuardarCalendario");

                if (notificacion != null)
                {
                    notificacion.Enviar(calendario.Medico.Email, new { nombre = calendario.Medico.ConcatUserName });
                }
            }
            catch (DoctorWebException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw Fabrica.CrearExcepcion(interna: e);
            }*/
        }

        /// <summary>
        /// Metodo del DAO para obtener medicos a partir de su identificador de usuario
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <returns>Medico</returns>
        public List<Medico> ObtenerMedico(string userId)
        {
            return db.Personas.OfType<Medico>().Where(p => p.ApplicationUserId == userId).ToList();
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

        public Paciente ObtenerPacienteCalendario(int calendarioId)
        {
            int pacienteId = db.Citas.Where(c => c.CitaId == calendarioId).Select(p => p.Paciente.PersonaId).Single();
            return db.Personas.OfType<Paciente>().Single(p => p.PersonaId == pacienteId);


        }

        public bool HorarioValidoCalendario(Calendario calendario)
        {
            var calendarios = db.Calendarios.Where(c => c.Medico.PersonaId == calendario.Medico.PersonaId && c.HoraInicio <= calendario.HoraInicio && c.HoraFin > calendario.HoraInicio);
            var calendarios2 = db.Calendarios.Where(c => c.Medico.PersonaId == calendario.Medico.PersonaId && c.HoraInicio < calendario.HoraFin && c.HoraFin >= calendario.HoraFin);
            if (((calendarios.Count() == 0) && (calendarios2.Count() == 0)) && (calendario.HoraInicio >= System.DateTime.Now))
                return true;
            else
                return false;
        }

    }
}