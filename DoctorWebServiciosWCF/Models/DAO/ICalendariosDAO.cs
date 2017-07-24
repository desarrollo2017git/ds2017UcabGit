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

        // Metodo del DAO para obtener una lista de calendarios a partir del identificador del medico a quien pertenecen
        List<Calendario> ObtenerTiempoDoctor(int medicoid);

        // Metodo del DAO para obtener una lista de citas a partir del identificador del medico a quien pertenecen
        List<Calendario> ObtenerCitasDoctor(int medicoid);

        // Método del Dao que recibe el paciente asociado a un calendario con el identificador suministrado
        Paciente ObtenerPacienteCalendario(int calendarioid);

        // Metodo del DAO que se encarga de guardar en la base de datos el objeto calendario recibido
        Calendario GuardarCalendario(Calendario calendario);

        // Metodo del DAO que valida que las fechas del calendario sean adecuadas recibiendo un objeto de este tipo
        bool HorarioValidoCalendario(Calendario calendario);

        // Método del DAO que se encarga de borrar de la base de datos el objeto suministrado
        Calendario EliminarCalendario(Calendario calendarioId);

        // Metodo DAO que retorna una lista de los calendarios de un paciente específico
        List<Calendario> ObtenerCitasPaciente(int pacienteId);

        // Metodo DAO que al recibir el codigo de un calendario, devuelve el objeto médico asociado
        Medico ObtenerMedicoCalendario(int calendarioId);
    }

}
