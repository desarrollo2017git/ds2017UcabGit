using DoctorWebServiciosWCF.Models.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.DAO
{
    public class CitasConsultasDAO : Dao, ICitasConsultasDAO
    {


        public void EliminarCita(Cita cita, Calendario calendario)
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

            this.Borrar<Cita>(db.Citas, cita);
            calendario.Disponible = 1;
            this.Actualizar<Calendario>(db.Calendarios, calendario, calendario.CalendarioId);
            //db.Citas.Remove(cita);
            //db.SaveChanges();
        }

        public void GuardarCita(Cita cita, Calendario calendario)
        {
            // Finalmente colocamos la Fecha Reservada como NO disponible
            calendario.Disponible = 0;
            this.Actualizar<Calendario>(db.Calendarios, calendario, calendario.CalendarioId);
            this.Crear<Cita>(db.Citas, cita);

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