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
        /// Metodo del DAO que se encarga de guardar en la base de datos el objeto calendario recibido
        /// </summary>
        /// <param name="userId"> Objeto Calendario a borrar </param>
        /// <returns> Objeto calendario borrado, el cual servira para validar que la creación realizada sea exitosa y notificacion </returns>
        public Calendario GuardarCalendario(Calendario calendario)
        {
            calendario.HoraFin = calendario.HoraInicio.AddHours(2); //ponemos la hora fin del bloque de citas
            calendario.Cancelada = false; 
            calendario.Disponible = 1;
            if (HorarioValidoCalendario(calendario))
            {
                Crear(calendario);
                var notificacionDAO = Utilidades.Instancia.Fabrica.CrearNotificacionDAO(); // empleo del modulo de notificaciones
                try
                {
                    var notificacion = notificacionDAO.Obtener("generarTiempo");

                    if (notificacion != null)
                    {   // mensaje que sera enviado en el campo name del correo de notificacion
                        String mensaje = " para el día " + calendario.HoraInicio.ToString("dd/MM/yy") + " desde las " + calendario.HoraInicio.ToString("HH:mm") + " hasta las " + calendario.HoraFin.ToString("HH:mm");
                        notificacion.Enviar(calendario.Medico.Email, new { nombre = calendario.Medico.ConcatUserName + mensaje });  // empleo del modulo de notificaciones
                    }
                }
                catch (DoctorWebException e)
                {
                    throw e;
                }
                return calendario;
            }
            else
                return null;
        }

        /// <summary>
        /// Método del DAO que se encarga de borrar de la base de datos el objeto suministrado
        /// </summary>
        /// <param name="calendario"> Objeto calendario a eliminar </param>
        /// <returns> Objeto calendario eliminado, el cual servira para validar que su eliminación haya sido satisfactoria y notificacion </returns>
        public Calendario EliminarCalendario(Calendario calendario)
        {
            try
            {
                var calendarioTmp = ObtenerPrimeroQue(c => c.CalendarioId == calendario.CalendarioId);
                if (calendarioTmp != null)
                {
                    Medico medico = ObtenerMedicoCalendario(calendarioTmp.CalendarioId); // objeto medico para obtener el correo y el nombre justo antes de eliminar el calendario
                    Borrar(calendarioTmp);
                    var notificacionDAO = Utilidades.Instancia.Fabrica.CrearNotificacionDAO();  // empleo del modulo de notificaciones
                    try
                    {
                        var notificacion = notificacionDAO.Obtener("eliminarTiempo");

                        if (notificacion != null)
                        {   // mensaje que sera enviado en el campo name del correo de notificacion
                            String mensaje = " para el día " + calendarioTmp.HoraInicio.ToString("dd/MM/yy") + " desde las " + calendarioTmp.HoraInicio.ToString("HH:mm") + " hasta las " + calendarioTmp.HoraFin.ToString("HH:mm");
                            notificacion.Enviar(medico.Email, new { nombre = medico.ConcatUserName + mensaje });  // empleo del modulo de notificaciones
                        }
                    }
                    catch (DoctorWebException e)
                    {
                        throw e;
                    }

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
        /// <returns>Lista de medicos asociada al usuario suministrado</returns>
        public List<Medico> ObtenerMedico(string userId)
        {
            var daoPersonas = Utilidades.Instancia.Fabrica.CrearDAO<Persona>();
            return daoPersonas.ObtenerTodos().OfType<Medico>().Where(p => p.ApplicationUserId == userId).ToList();
        }

        /// <summary>
        /// Metodo del DAO para obtener pacientes a partir de su identificador de usuario
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <returns>Lista de Pacientes asociados al usuario suministrado al metodo</returns>
        public List<Paciente> ObtenerPaciente(string userId)
        {
            var pacientedao = Utilidades.Instancia.Fabrica.CrearDAO<Persona>();
            return db.Personas.OfType<Paciente>().Where(p => p.ApplicationUserId == userId).ToList();
        }

        /// <summary>
        /// Metodo del DAO para obtener una lista de calendarios a partir del identificador del medico a quien pertenecen
        /// </summary>
        /// <param name="userId"> Identificador del medico </param>
        /// <returns>Lista de Calendarios asociados al calendario </returns>
        public List<Calendario> ObtenerTiempoDoctor(int medicoid)
        {
            var Calendariosdao = Utilidades.Instancia.Fabrica.CrearDAO<Calendario>();
            return Calendariosdao.ObtenerTodosLosQue(c => c.Medico.PersonaId == medicoid && c.Disponible == 1).ToList();
        }

        /// <summary>
        /// Metodo del DAO para obtener una lista de citas a partir del identificador del medico a quien pertenecen
        /// </summary>
        /// <param name="userId">Identificador del medico</param>
        /// <returns>Lista de Calendarios (citas) asociado al medico del identificador proporcionado</returns>
        public List<Calendario> ObtenerCitasDoctor(int medicoid)
        {
            var Calendarios = Utilidades.Instancia.Fabrica.CrearDAO<Calendario>();
            return Calendarios.ObtenerTodos().Where(c => c.Medico.PersonaId == medicoid && c.Disponible == 0 && c.Cancelada == false).ToList();
        }

        /// <summary>
        /// Método del Dao que recibe el paciente asociado a un calendario con el identificador suministrado
        /// </summary>
        /// <param name="calendarioId"> Identificador del calendario </param>
        /// <returns> Objeto paciente asociado al calendario mencionado con anterioridad </returns>
        public Paciente ObtenerPacienteCalendario(int calendarioId)
        {

            var pacientedao = Utilidades.Instancia.Fabrica.CrearDAO<Persona>();
            int pacienteId = db.Citas.Where(c => c.CitaId == calendarioId).Select(p => p.Paciente.PersonaId).Single();
            return (Paciente)pacientedao.ObtenerPrimeroQue(p => p.PersonaId == pacienteId);
        }

        /// <summary>
        /// Metodo del DAO que valida que las fechas del calendario sean adecuadas recibiendo un objeto de este tipo
        /// </summary>
        /// <param name="userId">Objeto calendario</param>
        /// <returns> valor booleando que sera true cuando el objeto calendarios que se desa validar tiene fechas adecuadas, en caso contrario será false</returns>
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

        /// <summary>
        /// Metodo DAO que retorna una lista de los calendarios de un paciente específico
        /// </summary>
        /// <param name="pacienteId"> Codigo del paciente </param>
        /// <returns> Una lista que contiene todos los elementos calendarios que sean citas, relacionados a ese paciente </returns>
        public List<Calendario> ObtenerCitasPaciente(int pacienteId)
        {
            var Calendarios = Utilidades.Instancia.Fabrica.CrearDAO<Calendario>();
            return Calendarios.ObtenerTodos().Where(c => c.Cita.Paciente.PersonaId == pacienteId && c.Disponible == 0).ToList();
        }

        /// <summary>
        /// Metodo DAO que al recibir el codigo de un calendario, devuelve el objeto médico asociado
        /// </summary>
        /// <param name="calendarioId"> Codigo identificador del calendario </param>
        /// <returns> Objeto medico relacionado al calendario del codigo suministrado </returns>
        public Medico ObtenerMedicoCalendario(int calendarioId)
        {
     
            var medicodao = Utilidades.Instancia.Fabrica.CrearDAO<Persona>();
            int medicoId = db.Calendarios.Where(c => c.CalendarioId == calendarioId).Select(p => p.Medico.PersonaId).Single();
            return (Medico)medicodao.ObtenerPrimeroQue(m => m.PersonaId == medicoId);
        }

    }
}