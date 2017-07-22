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
        List<Medico> ObtenerMedico(string userId);
        List<Paciente> ObtenerPaciente(string userId);
        List<Calendario> ObtenerTiempoDoctor(int medicoId);
        List<Calendario> ObtenerCitasDoctor(int medicoId);
        Paciente ObtenerPacienteCalendario(int calendarioId);



    }
}
