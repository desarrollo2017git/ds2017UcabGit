using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.DAO
{
	public interface IObservacionDeAtencionClinicaDAO
    {
    // Metodo del DAO para guardar Observaciones Medias en la Base de datos
    void GuardarObservacionDeAtencionClinica(ObservacionDeAtencionClinica observacionDeAtencionClinica);

    // Metodo del DAO para obtener una lista de las observaciones DeAtencionClinicas
    List<ObservacionDeAtencionClinica> ObtenerSelectListObservacionDeAtencionClinica();

    // Metodo del Data Access Object utilizado para eliminar observaciones DeAtencionClinicas. 
    void EliminarObservacionDeAtencionClinica(ObservacionDeAtencionClinica observacionDeAtencionClinica);
	
	}
}