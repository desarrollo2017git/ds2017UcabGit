using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DoctorWebServiciosWCF.Models.DAO
{
    using Modelo = Paciente;
    /// <summary>
    /// Interface para DAO Pacientes
    /// </summary>
    interface IPacienteDAO
    {
        //Metodo para guardar paciente
        bool GuardarPaciente(Paciente paciente);

        //Metodo para obtener todos los seguros
        List<Seguro> ObtenerSeguros();
        
        //Obtener lista de pacientes
        List<Paciente> ObtenerPacientesList(String id);

        // Metodo del Data Access Object utilizado para eliminar Citas. 
        void EliminarPaciente(Paciente paciente);

        //Metodo para obtener paciente a partir del nombre
        Paciente ObtenerPaciente(string userId);
       
    }
}
