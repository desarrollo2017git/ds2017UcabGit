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

            calendario.Disponible = 1;
            var calendarioToMod = db.Calendarios.Single(c => c.CalendarioId == calendario.CalendarioId);
            db.Entry(calendarioToMod).CurrentValues.SetValues(calendario);

            var citaToMod = db.Citas.Single(c => c.CitaId == cita.CitaId);
            db.Citas.Remove(citaToMod);
            db.SaveChanges();

            //var calendariosDao = Fabrica.CrearCalendariosDAO();
            //calendario.Disponible = 1;
            //calendariosDao.Actualizar(calendario, calendario.CalendarioId);
        }

        public void GuardarCita(Cita cita, Calendario calendario)
        {
            // Colocamos la Fecha Reservada como NO disponible
            calendario.Disponible = 0;
            cita.CitaId = calendario.CalendarioId;

            // Creamos la cita utilizando COMANDO
            Crear(cita);

            // Procedemos a actualizar la disponibilidad del calendario
            // Para esto, debemos recuperar el objeto en la base de datos
            // luego cambiamos su contenido por el del calendario que recibimos cuya disponibilidad es 0
            var calendarioToMod = db.Calendarios.Single(c => c.CalendarioId == calendario.CalendarioId);
            db.Entry(calendarioToMod).CurrentValues.SetValues(calendario);
            
            // Guardamos los cambios en la BD
            db.SaveChanges();

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

        public Calendario ObtenerCalendario(int calendarioId)
        {
            var calendario = db.Calendarios.Single(c => c.CalendarioId == calendarioId);
            return calendario;

        }
        // Obtener cita por id
        public Cita ObtenerCita(int id)
        {
            return db.Citas.Include(c => c.Calendario).Include(c => c.CentroMedico).Include(c => c.Paciente).Where(c => c.CitaId == id).Single();
        }

        // Obtener especialidad medica por id
        public EspecialidadMedica ObtenerEspecialidadMedica(int espMedica)
        {
            return db.EspecialidadesMedicas.Single(e => e.EspecialidadMedicaId == espMedica);
        }
        // Obtener centro medico por id
        public CentroMedico ObtenerCentroMedico(int centroMedicoId)
        {
            return db.CentrosMedicos.Single(m => m.CentroMedicoId == centroMedicoId);

        }

        public EspecialidadMedica ObtenerEspecialidadMedicaDelDoctor(int medicoId)
        {
            return db.Personas.OfType<Medico>().Where(m => m.PersonaId == medicoId).Select(p => p.EspecialidadMedica).Single();
        }

        public Medico ObtenerMedicoAsignadoACita(int citaId)
        {
            return db.Calendarios.Where(m => m.Cita.CitaId == citaId).Select(p => p.Medico).Single();
        }

        // Obtener paciente por id
        public Paciente ObtenerPaciente(string userId)
        {
            return db.Personas.OfType<Paciente>().Single(p => p.ApplicationUserId == userId);
        }
        // Obtener un medico por id
        public Medico ObtenerMedico(string userId)
        {
            return db.Personas.OfType<Medico>().Include(m => m.EspecialidadMedica).Single(p => p.ApplicationUserId == userId);
        }
         // Lista de citas paciente
        public List<Cita> ObtenerListaCitas(string userId)
        {
            return db.Citas.Include(c => c.CentroMedico).Include(c => c.Paciente).Include(c => c.Calendario).Where(c => c.Paciente.ApplicationUserId == userId).ToList();
        }
        // Lista de citas del doctor
        public List<Cita> ObtenerCitasDoctor(string userId)
        {
            return db.Citas.Include(c => c.CentroMedico).Include(c => c.Paciente).Include(c => c.Calendario).Where(c => c.Calendario.Medico.ApplicationUserId == userId).ToList();
        }
        // Obtener listado de centros medicos
        public List<CentroMedico> ObtenerSelectListCentrosMedicos()
        {
            return db.CentrosMedicos.ToList();
        }
        // ARREGLAR DE URTIMO
        public List<EspecialidadMedica> ObtenerEsMedicasPorMedicosEnCentroMedico(int cMedicoId)
        {
            //.Include(e => e.EspecialidadMedica).Include(e => e.CentroMedico)
            return db.Personas.OfType<Medico>().Where(m => m.CentroMedico.CentroMedicoId == cMedicoId).Select(c => c.EspecialidadMedica).Distinct().ToList();
        }
        // Obtener listado de medicos disponibles en un centro medico por especialidad
        public List<Medico> ObtenerSelectListMedicosQueTrabajanEnCentroMedico(int centroMedicoId, int espMedica)
        {
            return db.Personas.OfType<Medico>().Where(p => p.CentroMedico.CentroMedicoId == centroMedicoId && p.EspecialidadMedica.EspecialidadMedicaId == espMedica).ToList();
        }

        public CentroMedico ObtenerCentroMedicoRif(string centroMedicoRif)
        {
            return db.CentrosMedicos.Single(m => m.Rif == centroMedicoRif);
        }

        public List<Calendario> ObtenerListaDisponibilidad(int medicoId)
        {
            //Where(m => m.Medico.PersonaId == mdId && m.Disponible == 1).OrderBy(m => m.HoraInicio)
            return db.Calendarios.Where(m => m.Medico.PersonaId == medicoId && m.Disponible == 1).OrderBy(m => m.HoraInicio).ToList();
        }
    }
}