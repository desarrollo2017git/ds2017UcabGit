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
    public interface IServicioObservacionClinicaE2
    {
        // Metodo del cliente que realiza el llamado para Guardar una Observacion clinica
        void GuardarObservacionClinicaE2(ObservacionClinicaE2 observacionClinicaE2);


        // Metodo en el cliente utilizado para obtener una lista de todos
        // Observaciones clinicas
        List<ObservacionClinicaE2> ObtenerSelectListObservacionClinicaE2();


        // Metodo del cliente que realiza el llamado para eliminar una observacion clinica
        void EliminarObservacionClinicaE2(ObservacionClinicaE2 observacionClinicaE2);
    }
}