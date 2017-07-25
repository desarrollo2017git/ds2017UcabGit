using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWebServiciosWCF.Models.DAO
{
	public interface IObservacionClinicaE2DAO
	{
        // Metodo del DAO para guardar Observaciones clinicas en la Base de datos
        void GuardarObservacionClinicaE2(ObservacionClinicaE2 observacionClinicaE2);

        // Metodo del DAO para obtener una lista de las observaciones clinicas
        List<ObservacionClinicaE2> ObtenerSelectListObservacionClinicaE2();

        // Metodo del DAO utilizado para eliminar observaciones clinicas. 
        void EliminarObservacionClinicaE2(ObservacionClinicaE2 observacionClinicaE2);

    }
}