using DoctorWebASP.Controllers;
using DoctorWebASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DoctorWebASP.Models.Services
{
    public interface IServicioCalendarios
    {
        
        string ObtenerUsuarioLoggedIn(CalendariosController calendarioController);

        // Metodo utilizado para obtener una lista de medicos realcionados, suministrando el identificador de su usuario
        List<Medico> ObtenerMedico(string userId);

        // Metodo  utilizado para obtener una lista de pacientes suministrando el identificador de su usuario
        List<Paciente> ObtenerPaciente(string userId);

        // Metodo que sirve paraobtener una lista de los horarios de un medico proporcionando su identificador
        List<Calendario> ObtenerTiempoDoctor(int medicoId);

        // Metodo que al pasarle el identificador de un medico, devuelve una lista con sus citas agendadas
        List<Calendario> ObtenerCitasDoctor(int medicoId);

        // Método que recibe el identificador de un calendario y retorna el paciente asociado 
        Paciente ObtenerPacienteCalendario(int calendarioId);

        // Este método crea y registra un calendario recibiendo un objeto de este tipo
        Calendario GuardarCalendario(Calendario calendario);

        // Este método se encarga de eliminar del sistema al objeto suministrado
        Calendario EliminarCalendario(Calendario calendarioId);

        //Método que recibe el identificador de un paciente y retorna una lista con todas sus citas
        List<Calendario> ObtenerCitasPaciente(int pacienteId);

        // Metodo que consigue un objeto médico al pasarle el identificador de un objeto calendario que tenga asociado
        Medico ObtenerMedicoCalendario(int calendarioId);
    }
}
