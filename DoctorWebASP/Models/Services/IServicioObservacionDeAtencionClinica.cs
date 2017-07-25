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
    public interface IServicioObservacionDeAtencionClinica
    {
        // Metodo del cliente que realiza el llamado para Guardar una Observacion Medica
        void GuardarObservacionDeAtencionClinica(ObservacionDeAtencionClinica observacionDeAtencionClinica);


        // Metodo en el cliente utilizado para obtener una lista de todos
        // Observaciones Medicas
        List<ObservacionDeAtencionClinica> ObtenerSelectListObservacionDeAtencionClinica();


        // Metodo del cliente que realiza el llamado para eliminar una Cita
        void EliminarObservacionDeAtencionClinica(ObservacionDeAtencionClinica observacionDeAtencionClinica);
    }
}