using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DoctorWebServiciosWCF.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IServicioObservacionClinicaE2" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServicioObservacionClinicaE2
    {
        // Contrato inicial del servicio web para realizar prueba
        [OperationContract]
        [WebGet(UriTemplate = "/DoWork", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]

        void DoWork();

        // Contrato del servicio web para guardar una observacion clinica
        [OperationContract]
        [WebInvoke(UriTemplate = "/GuardarObservacionClinicaE2", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso GuardarObservacionClinicaE2(ObservacionClinicaE2 observacionClinicaE2);

        // Contrato del servicio web para obtener la lista de observaciones clinicas
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerSelectListObservacionClinicaE2", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<List<ObservacionClinicaE2>> ObtenerSelectListObservacionClinicaE2();
        // Contrato del servicio web para eliminar una observacion clinica
        [OperationContract]
        [WebInvoke(UriTemplate = "/EliminarObservacionClinicaE2", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "DELETE", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso EliminarObservacionClinicaE2(ObservacionClinicaE2 observacionClincaE2);
    }
}
