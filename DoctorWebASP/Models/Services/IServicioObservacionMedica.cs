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
    public interface IServicioObservacionMedica
    {
        // Metodo en el cliente para obtener una Observacion Medica
        ObservacionMedica ObtenerObservacionMedica(int observacionMedicaId);

        // Metodo en el cliente utilizado para obtener un paciente en especifico
        Paciente ObtenerPaciente(string userId);

        // Metodo del cliente que realiza el llamado para Guardar una observacion Medica
        void GuardarObservacionMedica(ObservacionMedica observacionMedica);

        // Metodo del cliente que realiza el llamado para eliminar una observacion medica
        void EliminarObservacionMedica(ObservacionMedica observacionMedica);

    }
}
