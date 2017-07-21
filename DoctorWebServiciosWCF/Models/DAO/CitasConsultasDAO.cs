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

            Borrar(cita);

            var calendariosDao = Fabrica.CrearCalendariosDAO();
            calendario.Disponible = 1;
            calendariosDao.Actualizar(calendario, calendario.CalendarioId);
        }

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

        public Calendario ObtenerCalendario(int calendarioId)
        {
            var calendario = db.Calendarios.Include(b => b.Medico.EspecialidadMedica).Include(b => b.Medico.CentroMedico).Single(c => c.CalendarioId == calendarioId);
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
        // Obtener paciente por id
        public Paciente ObtenerPaciente(string userId)
        {
            return db.Personas.OfType<Paciente>().Single(p => p.ApplicationUserId == userId);
        }
        // Obtener un medico por id
        public Medico ObtenerMedico(string userId)
        {
            return db.Personas.OfType<Medico>().Include(m => m.CentroMedico).Include(m => m.EspecialidadMedica).Single(p => p.ApplicationUserId == userId);
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
        public List<EspecialidadMedica> ObtenerEsMedicasPorMedicosEnCentroMedico(CentroMedico cMedico)
        {
            return db.Personas.OfType<Medico>().Where(m => m.CentroMedico.CentroMedicoId == cMedico.CentroMedicoId).Select(c => c.EspecialidadMedica).Distinct().ToList();
        }
        // Obtener listado de medicos disponibles en un centro medico por especialidad
        public List<Medico> ObtenerSelectListMedicosQueTrabajanEnCentroMedico(int centroMedicoId, int espMedica)
        {
            return db.Personas.OfType<Medico>().Where(p => p.CentroMedico.CentroMedicoId == centroMedicoId && p.EspecialidadMedica.EspecialidadMedicaId == espMedica).ToList();
        }
    }
}