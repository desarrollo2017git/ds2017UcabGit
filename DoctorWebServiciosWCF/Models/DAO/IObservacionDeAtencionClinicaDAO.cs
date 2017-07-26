using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.DAO
{
    // Interface de DAO para Clase ObservacionDeAtencionClinica
    public interface IObservacionDeAtencionClinicaDAO
    {
    // Metodo del DAO para guardar Observaciones de Atencion Clinica en la Base de datos
    void GuardarObservacionDeAtencionClinica(ObservacionDeAtencionClinica observacionDeAtencionClinica);

    // Metodo del DAO para obtener una lista de las observaciones De Atencion Clinica
    List<ObservacionDeAtencionClinica> ObtenerSelectListObservacionDeAtencionClinica();

    // Metodo del Data Access Object utilizado para eliminar observaciones De Atencion Clinica. 
    void EliminarObservacionDeAtencionClinica(ObservacionDeAtencionClinica observacionDeAtencionClinica);
	
	}
}