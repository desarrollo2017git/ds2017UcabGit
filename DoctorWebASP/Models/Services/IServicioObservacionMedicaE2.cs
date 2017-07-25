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
    public interface IServicioObservacionMedicaE2
    {

        // Metodo del cliente que realiza el llamado para Guardar una Observacion Medica
        void GuardarObservacionMedicaE2(ObservacionMedicaE2 observacionMedicaE2);


        // Metodo en el cliente utilizado para obtener una lista de todos
        // Observaciones Medicas
        List<ObservacionMedicaE2> ObtenerSelectListObservacionMedicaE2();


        // Metodo del cliente que realiza el llamado para eliminar una observacion medica
        void EliminarObservacionMedicaE2(ObservacionMedicaE2 observacionMedicaE2);
    }
}