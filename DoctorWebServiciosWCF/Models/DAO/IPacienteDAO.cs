using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DoctorWebServiciosWCF.Models.DAO
{
    using Modelo = Paciente;

    interface IPacienteDAO
    {
        bool GuardarPaciente(Paciente paciente);
        List<Seguro> ObtenerSeguros();

        // Metodo del Data Access Object utilizado para eliminar Citas. 
        void EliminarPaciente(Paciente paciente);
        //Metodo para obtener paciente a partir del nombre
        Paciente ObtenerPaciente(string userId);
        //Metodo para obtener la lista de seguros
        List<Seguro> ObtenerSelectListSeguros();
    }
}
