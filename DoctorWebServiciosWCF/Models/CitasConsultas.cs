using DoctorWebServiciosWCF.Models.ORM;
using System.Collections.Generic;
using System.Linq;

namespace DoctorWebServiciosWCF.Models
{
    public class CitasConsultas : ICitasConsultas
    {
        private ContextoBD db = new ContextoBD();

        public void EliminarCita(Cita cita,Calendario calendario)
        {
            var cNotificaciones = new DoctorWebServiciosWCF.Controllers.NotificacionController();
            var resp1 = cNotificaciones.Obtener("cancelarCita");
            var resp2 = cNotificaciones.Obtener("cancelarCita");

            if (resp1.SinProblemas)
            {
                resp1.Contenido.Enviar(calendario.Medico.Email, new { nombre = calendario.Medico.ConcatUserName });
            }
            if (resp2.SinProblemas)
            {
                resp2.Contenido.Enviar(cita.Paciente.Email, new { nombre = cita.Paciente.ConcatUserName });
            }

            db.Citas.Remove(cita);
            calendario.Disponible = 1;
            db.SaveChanges();
        }

        public void GuardarCita(Cita cita, Calendario calendario)
        {
            db.Citas.Add(cita);
            // Finalmente colocamos la Fecha Reservada como NO disponible
            calendario.Disponible = 0;
            db.SaveChanges();

            var cNotificaciones = new DoctorWebServiciosWCF.Controllers.NotificacionController();
            var resp1 = cNotificaciones.Obtener("generarCita");
            var resp2 = cNotificaciones.Obtener("generarCita");

            if (resp1.SinProblemas)
            {
                resp1.Contenido.Enviar(calendario.Medico.Email, new { nombre = calendario.Medico.ConcatUserName });
            }
            if (resp2.SinProblemas)
            {
                resp2.Contenido.Enviar(cita.Paciente.Email, new { nombre = cita.Paciente.ConcatUserName });
            }
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

        CentroMedico ICitasConsultas.ObtenerCentroMedico(int centroMedicoId)
        {
            return db.CentrosMedicos.Single(m => m.CentroMedicoId == centroMedicoId);
        }

        
    }
}