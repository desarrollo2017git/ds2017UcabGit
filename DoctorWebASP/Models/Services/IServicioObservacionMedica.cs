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
    public interface IServicioObservacionMedica
    {
 
        // Metodo del cliente que realiza el llamado para Guardar una Observacion Medica
        void GuardarObservacionMedica(ObservacionMedica observacionMedica);

     
        // Metodo en el cliente utilizado para obtener una lista de todos
        // Observaciones Medicas
        List<ObservacionMedica> ObtenerSelectListObservacionMedica();

 
        // Metodo del cliente que realiza el llamado para eliminar una Observacion Medica
        void EliminarObservacionMedica(ObservacionMedica observacionMedica);

    }
}
