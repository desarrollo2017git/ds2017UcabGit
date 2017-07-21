using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWebServiciosWCF.Models.DAO
{
    public interface ICitasConsultasDAO
    {
        CentroMedico ObtenerCentroMedico(int centroMedicoId);
        Paciente ObtenerPaciente(string userId);
        Calendario ObtenerCalendario(int calendarioId);
        void GuardarCita(Cita cita, Calendario calendario);
        Medico ObtenerMedico(string userId);
        List<Cita> ObtenerListaCitas(string userId);
        List<Cita> ObtenerCitasDoctor(string userId);
        List<CentroMedico> ObtenerSelectListCentrosMedicos();
        List<EspecialidadMedica> ObtenerEsMedicasPorMedicosEnCentroMedico(int cMedicoId);
        EspecialidadMedica ObtenerEspecialidadMedica(int espMedica);
        List<Medico> ObtenerSelectListMedicosQueTrabajanEnCentroMedico(int centroMedicoId, int espMedica);
        Cita ObtenerCita(int id);
        void EliminarCita(Cita cita, Calendario calendario);
        CentroMedico ObtenerCentroMedicoRif(string centroMedicoRif);
        List<Calendario> ObtenerListaDisponibilidad(int medicoId);
    }
}
