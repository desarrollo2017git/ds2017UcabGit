using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.DAO
{     // Interface DAO para Observaciones Medicas
    public interface IObservacionMedicaDAO
    {
        // Metodo del DAO para guardar Observaciones Medias en la Base de datos
        void GuardarObservacionMedica(ObservacionMedica observacionMedica);

        // Metodo del DAO para obtener una lista de las observaciones medicas
        List<ObservacionMedica> ObtenerSelectListObservacionMedica();

        // Metodo del Data Access Object utilizado para eliminar observaciones medicas. 
        void EliminarObservacionMedica(ObservacionMedica observacionMedica);
        
    }
}