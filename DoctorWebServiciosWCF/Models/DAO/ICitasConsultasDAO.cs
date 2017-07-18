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
        Calendario ObtenerCalendario(int calendarioId);
        void GuardarCita(Cita cita, Calendario calendario);
        CentroMedico ObtenerSingleCentroMedico(string centroMedico);
        CentroMedico ObtenerSingleCentroMedico(int centroMedicoId);
        EspecialidadMedica ObtenerEspecialidadMedica(int espMedica);
        Cita ObtenerCita(int id);
        void EliminarCita(Cita cita, Calendario calendario);
    }
}
