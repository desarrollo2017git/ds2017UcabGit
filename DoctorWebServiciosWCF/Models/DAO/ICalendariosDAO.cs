using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWebServiciosWCF.Models.DAO
{
    public interface ICalendariosDAO
    {
        // Metodo del DAO para obtener medicos a partir de su identificador de usuario
        List<Medico> ObtenerMedico(string userId);
        // Metodo del DAO para obtener pacientes a partir de su identificador de usuario
        List< Paciente> ObtenerPaciente(string userId);
        // Metodo del DAO para obtener pacientes a partir de su identificador de usuario
        List<Calendario> ObtenerTiempoDoctor(int medicoid);
        List<Calendario> ObtenerCitasDoctor(int medicoid);
        Paciente ObtenerPacienteCalendario(int calendarioid);
        Calendario GuardarCalendario(Calendario calendario);
        bool HorarioValidoCalendario(Calendario calendario);
    }

}
