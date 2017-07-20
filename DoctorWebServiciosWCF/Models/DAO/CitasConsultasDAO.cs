using DoctorWebServiciosWCF.Helpers;
using DoctorWebServiciosWCF.Models.ORM;
using System;
using System.Collections.Generic;
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
            return db.Calendarios.Single(c => c.CalendarioId == calendarioId);
        }

        public Cita ObtenerCita(int id)
        {
            return db.Citas.Find(id);
        }


        public EspecialidadMedica ObtenerEspecialidadMedica(int espMedica)
        {
            return db.EspecialidadesMedicas.Single(e => e.EspecialidadMedicaId == espMedica);
        }

        public CentroMedico ObtenerCentroMedico(int centroMedicoId)
        {
            CentroMedico cMedico = db.CentrosMedicos.Single(m => m.CentroMedicoId == centroMedicoId);
            db.Dispose();
            return cMedico;
        }

        public Paciente ObtenerPaciente(string userId)
        {
            return db.Personas.OfType<Paciente>().Single(p => p.ApplicationUserId == userId);
        }

        public Medico ObtenerMedico(string userId)
        {
            return db.Personas.OfType<Medico>().Single(p => p.ApplicationUserId == userId);
        }

        public List<Cita> ObtenerListaCitas(string userId)
        {
            return db.Citas.Where(c => c.Paciente.ApplicationUserId == userId).ToList();
        }

        public List<Cita> ObtenerCitasDoctor(string userId)
        {
            return db.Citas.Where(c => c.Calendario.Medico.ApplicationUserId == userId).ToList();
        }

        public List<CentroMedico> ObtenerSelectListCentrosMedicos()
        {
            return db.CentrosMedicos.ToList();
        }

        public List<EspecialidadMedica> ObtenerEsMedicasPorMedicosEnCentroMedico(CentroMedico cMedico)
        {
            return db.Personas.OfType<Medico>().Where(m => m.CentroMedico.CentroMedicoId == cMedico.CentroMedicoId).Select(c => c.EspecialidadMedica).Distinct().ToList();
        }

        public List<Medico> ObtenerSelectListMedicosQueTrabajanEnCentroMedico(int centroMedicoId, int espMedica)
        {
            return db.Personas.OfType<Medico>().Where(p => p.CentroMedico.CentroMedicoId == centroMedicoId && p.EspecialidadMedica.EspecialidadMedicaId == espMedica).ToList();
        }
    }
}