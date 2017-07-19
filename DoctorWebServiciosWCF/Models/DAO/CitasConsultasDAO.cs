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


        public CentroMedico ObtenerSingleCentroMedico(string centroMedico)
        {
            return db.CentrosMedicos.Single(m => m.Rif == centroMedico);
        }

        public CentroMedico ObtenerSingleCentroMedico(int centroMedicoId)
        {
            return db.CentrosMedicos.Single(c => c.CentroMedicoId == centroMedicoId);
        }

        public CentroMedico ObtenerCentroMedico(int centroMedicoId)
        {
            CentroMedico cMedico = db.CentrosMedicos.Single(m => m.CentroMedicoId == centroMedicoId);
            db.Dispose();
            return cMedico;
        }

    }
}