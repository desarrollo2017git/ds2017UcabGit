using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.DAO
{
    public interface IObservacionMedicaDAO
    {
        // Metodo del DAO para obtener observaciones a partir de su identificador
        ObservacionMedica ObtenerObservacion(int observacionMedicaId);
        // Metodo del DAO para obtener pacientes a partir de su identificador de usuario
        Paciente ObtenerPaciente(string userId);
        // Metodo del DAO para guardar Observaciones Medicas en la Base de datos
        void GuardarObservacionMedica(ObservacionMedica observacionMedica);
        // Metodo del DAO utilizado para eliminar Observaciones Medicas
        void EliminarObservacionMedica(ObservacionMedica observacionMedica);
    }
}