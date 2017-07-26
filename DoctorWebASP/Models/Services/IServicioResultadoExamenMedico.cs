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

    // Interface de Metodos para la invocacion del servicio Web
    public interface IServicioResultadoExamenMedico
    {


        // Metodo del cliente que realiza el llamado para Guardar un Resultado Medico
        void GuardarResultadoExamenMedico(ResultadoExamenMedico resultadoExamenMedico);


        // Metodo en el cliente utilizado para obtener una lista de todos
        // Resultados Medicos
        List<ResultadoExamenMedico> ObtenerSelectListResultadoExamenMedico();


        // Metodo del cliente que realiza el llamado para eliminar un Resultado Medico
        void EliminarResultadoExamenMedico(ResultadoExamenMedico resultadoExamenMedico);
    }
}