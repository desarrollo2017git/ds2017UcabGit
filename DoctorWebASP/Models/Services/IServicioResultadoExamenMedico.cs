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
    public interface IServicioResultadoExamenMedico
    {


        // Metodo del cliente que realiza el llamado para Guardar una Observacion Medica
        void GuardarResultadoExamenMedico(ResultadoExamenMedico resultadoExamenMedico);


        // Metodo en el cliente utilizado para obtener una lista de todos
        // Observaciones Medicas
        List<ResultadoExamenMedico> ObtenerSelectListResultadoExamenMedico();


        // Metodo del cliente que realiza el llamado para eliminar una Cita
        void EliminarResultadoExamenMedico(ResultadoExamenMedico resultadoExamenMedico);
    }
}