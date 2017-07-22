using DoctorWebServiciosWCF.Helpers;
using DoctorWebServiciosWCF.Models.ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.DAO
{
    public class CitasDAO : DAO<Cita>, ICitasConsultasDAO
    {
        public CitasDAO() : base()
        {
            coleccion = db.Citas;
        }

        /// <summary>
        /// Metodo del Data Access Object utilizado para eliminar Citas. 
        /// </summary>
        /// <param name="cita">Cita que se desea eliminar</param>
        /// <param name="calendario">Calendario para devolver la disponibilidad</param>
        public void EliminarCita(Cita cita, Calendario calendario)
        {
            var notificacionDAO = Fabrica.CrearNotificacionDAO();
            try
            {
                var notificacion = notificacionDAO.Obtener("cancelarCita");

                if (notificacion != null)
                {
                    notificacion.Enviar(calendario.Medico.Email, new { nombre = calendario.Medico.ConcatUserName });
                    notificacion.Enviar(cita.Paciente.Email, new { nombre = cita.Paciente.ConcatUserName });
                }
            }
            catch {}

            Borrar(cita);

            var calendariosDao = Fabrica.CrearCalendariosDAO();
            calendario.Disponible = 1;
            calendariosDao.Actualizar(calendario, calendario.CalendarioId);
        }

        /// <summary>
        /// Metodo del DAO para guardar Citas en la Base de datos
        /// </summary>
        /// <param name="cita">Cita que se desea guardar</param>
        /// <param name="calendario">Calendario para setear la disponibilidad</param>
        public void GuardarCita(Cita cita, Calendario calendario)
        {
            // Finalmente colocamos la Fecha Reservada como NO disponible
            var calendariosDao = Fabrica.CrearCalendariosDAO();
            calendario.Disponible = 0;
            calendariosDao.Actualizar(calendario, calendario.CalendarioId);

            Crear(cita);

            var notificacionDAO = Fabrica.CrearNotificacionDAO();
            try
            {
                var notificacion = notificacionDAO.Obtener("generarCita");

                if (notificacion != null)
                {
                    notificacion.Enviar(calendario.Medico.Email, new { nombre = calendario.Medico.ConcatUserName });
                    notificacion.Enviar(cita.Paciente.Email, new { nombre = cita.Paciente.ConcatUserName });
                }
            }
            catch { }
        }

        /// <summary>
        /// Metodo del DAO para obtener un calendario especifico
        /// </summary>
        /// <param name="calendarioId">Identificador del calendario</param>
        /// <returns>Calendario</returns>
        public Calendario ObtenerCalendario(int calendarioId)
        {
            var calendario = db.Calendarios.Single(c => c.CalendarioId == calendarioId);
            return calendario;

        }
        /// <summary>
        /// Metodo del DAO para obtener una cita especifica
        /// </summary>
        /// <param name="id">Identificar de la cita</param>
        /// <returns>Cita</returns>
        public Cita ObtenerCita(int id)
        {
            return db.Citas.Include(c => c.Calendario).Include(c => c.CentroMedico).Include(c => c.Paciente).Where(c => c.CitaId == id).Single();
        }

        /// <summary>
        /// Metodo del DAO para obtener una especialidad medica especifica
        /// </summary>
        /// <param name="espMedica">Identificador de la especialidad medica</param>
        /// <returns>Especialidad medica</returns>
        public EspecialidadMedica ObtenerEspecialidadMedica(int espMedica)
        {
            return db.EspecialidadesMedicas.Single(e => e.EspecialidadMedicaId == espMedica);
        }
        
        /// <summary>
        /// Metodo del DAO para obtener un centro medico especifico
        /// </summary>
        /// <param name="centroMedicoId">Identificador del centro medico</param>
        /// <returns>Centro medico</returns>
        public CentroMedico ObtenerCentroMedico(int centroMedicoId)
        {
            return db.CentrosMedicos.Single(m => m.CentroMedicoId == centroMedicoId);

        }

        /// <summary>
        /// Metodo del DAO para obtener la especialidad medica de un medico especifico
        /// </summary>
        /// <param name="medicoId">Identificador del medico</param>
        /// <returns>Especialidad medica</returns>
        public EspecialidadMedica ObtenerEspecialidadMedicaDelDoctor(int medicoId)
        {
            return db.Personas.OfType<Medico>().Where(m => m.PersonaId == medicoId).Select(p => p.EspecialidadMedica).Single();
        }

        /// <summary>
        /// Metodo del DAO para obtener el medico asignado a una cita especifica
        /// </summary>
        /// <param name="citaId">Identificador de la cita</param>
        /// <returns>Medico</returns>
        public Medico ObtenerMedicoAsignadoACita(int citaId)
        {
            return db.Calendarios.Where(m => m.Cita.CitaId == citaId).Select(p => p.Medico).Single();
        }

        /// <summary>
        /// Metodo del DAO para obtener pacientes a partir de su identificador de usuario
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <returns>Paciente</returns>
        public Paciente ObtenerPaciente(string userId)
        {
            return db.Personas.OfType<Paciente>().Single(p => p.ApplicationUserId == userId);
        }

        /// <summary>
        /// Metodo del DAO para obtener medicos a partir de su identificador de usuario
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <returns>Medico</returns>
        public Medico ObtenerMedico(string userId)
        {
            return db.Personas.OfType<Medico>().Include(m => m.CentroMedico).Include(m => m.EspecialidadMedica).Single(p => p.ApplicationUserId == userId);
        }
        
        /// <summary>
        /// Metodo del DAO para obtener la lista de las citas de un paciente
        /// </summary>
        /// <param name="userId">Identificador de usuario del paciente</param>
        /// <returns>Lista de citas</returns>
        public List<Cita> ObtenerListaCitas(string userId)
        {
            return db.Citas.Include(c => c.CentroMedico).Include(c => c.Paciente).Include(c => c.Calendario).Where(c => c.Paciente.ApplicationUserId == userId).ToList();
        }

        /// <summary>
        /// Metodo del DAO para obtener la lista de las citas de un medico
        /// </summary>
        /// <param name="userId">Identificador de usuario del medico</param>
        /// <returns>Lista de citas</returns>
        public List<Cita> ObtenerCitasDoctor(string userId)
        {
            return db.Citas.Include(c => c.CentroMedico).Include(c => c.Paciente).Include(c => c.Calendario).Where(c => c.Calendario.Medico.ApplicationUserId == userId).ToList();
        }
        
        /// <summary>
        /// Metodo del DAO para obtener una lista de los centros medicos
        /// </summary>
        /// <returns>Lista de centros medicos</returns>
        public List<CentroMedico> ObtenerSelectListCentrosMedicos()
        {
            return db.CentrosMedicos.ToList();
        }
        
        /// <summary>
        /// Metodo del DAO para obtener la lista de especialidades medicas en un centro medico
        /// </summary>
        /// <param name="cMedicoId">Identificador del centro medico</param>
        /// <returns>Lista de especialidades</returns>
        public List<EspecialidadMedica> ObtenerEsMedicasPorMedicosEnCentroMedico(int cMedicoId)
        {
            //.Include(e => e.EspecialidadMedica).Include(e => e.CentroMedico)
            return db.Personas.OfType<Medico>().Where(m => m.CentroMedico.CentroMedicoId == cMedicoId).Select(c => c.EspecialidadMedica).Distinct().ToList();
        }

        /// <summary>
        /// Metodo del DAO para obtener la lista de los doctores que trabajen en un centro medico
        /// </summary>
        /// <param name="centroMedicoId">Identificador del centro medico</param>
        /// <param name="espMedica">Identificador de la especialidad medica</param>
        /// <returns>Lista de medicos</returns>
        public List<Medico> ObtenerSelectListMedicosQueTrabajanEnCentroMedico(int centroMedicoId, int espMedica)
        {
            return db.Personas.OfType<Medico>().Where(p => p.CentroMedico.CentroMedicoId == centroMedicoId && p.EspecialidadMedica.EspecialidadMedicaId == espMedica).ToList();
        }

        /// <summary>
        /// Metodo del DAO para obtener centros medicos a partir de su RIF
        /// </summary>
        /// <param name="centroMedicoRif">RIF del centro medico</param>
        /// <returns>Centro medico</returns>
        public CentroMedico ObtenerCentroMedicoRif(string centroMedicoRif)
        {
            return db.CentrosMedicos.Single(m => m.Rif == centroMedicoRif);
        }

        /// <summary>
        /// Metodo del DAO para obtener la lista de horarios/calendario de un medico
        /// </summary>
        /// <param name="medicoId">Identificador del medico</param>
        /// <returns>Lista de calendarios</returns>
        public List<Calendario> ObtenerListaDisponibilidad(int medicoId)
        {
            //Where(m => m.Medico.PersonaId == mdId && m.Disponible == 1).OrderBy(m => m.HoraInicio)
            return db.Calendarios.Where(m => m.Medico.PersonaId == medicoId && m.Disponible == 1).OrderBy(m => m.HoraInicio).ToList();
        }
    }
}