using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWebServiciosWCF.Models.DAO
{
    public interface IObservacionMedicaE2DAO
    {
        // Metodo del DAO para guardar Observaciones Medicas en la Base de datos
        void GuardarObservacionMedicaE2(ObservacionMedicaE2 observacionMedicaE2);

        // Metodo del DAO para obtener una lista de las observaciones medicas
        List<ObservacionMedicaE2> ObtenerSelectListObservacionMedicaE2();

        // Metodo del DAO utilizado para eliminar observaciones medicas. 
        void EliminarObservacionMedicaE2(ObservacionMedicaE2 observacionMedicaE2);

    }
}