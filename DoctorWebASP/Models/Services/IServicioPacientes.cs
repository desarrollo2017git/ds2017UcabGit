using DoctorWebASP.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DoctorWebASP.Models.Services
{
    /// <summary>
    /// Interfaz para los serviciosPaciente
    /// </summary>
    interface IServicioPacientes
    {
        //Metodo para eliminar un paciente
        void EliminarPaciente(Paciente paciente);
        //Metodo para guardar paciente
        void GuardarPaciente(Paciente paciente);
        //Metodo para obtener lista de los seguros
        SelectList ObtenerSeguros();
        //Metodo para obtener todos los pacientes
        SelectList ObtenerPacientesList();
        //Metodo para obtener un paciente
        Paciente ObtenerPaciente(string userId);
        //Obtiene el usuario loggeado
        string ObtenerUsuarioLoggedIn(PacientesController pacientesController);
    }
}
