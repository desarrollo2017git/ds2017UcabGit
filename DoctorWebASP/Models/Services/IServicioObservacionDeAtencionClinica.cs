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
    public interface IServicioObservacionDeAtencionClinica
    {
        // Metodo del cliente que realiza el llamado para Guardar una Observacion Clinia
        void GuardarObservacionDeAtencionClinica(ObservacionDeAtencionClinica observacionDeAtencionClinica);


        // Metodo en el cliente utilizado para obtener una lista de todos
        // Observaciones Clinicas
        List<ObservacionDeAtencionClinica> ObtenerSelectListObservacionDeAtencionClinica();


        // Metodo del cliente que realiza el llamado para eliminar una Observacion Clinica
        void EliminarObservacionDeAtencionClinica(ObservacionDeAtencionClinica observacionDeAtencionClinica);
    }
}