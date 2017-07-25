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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IServicioObservacionDeAtencionClinica" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServicioObservacionDeAtencionClinica
    {


        // Contrato inicial del servicio web para realizar prueba
        [OperationContract]
        [WebGet(UriTemplate = "/DoWork", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]

        void DoWork();

        // Contrato del servicio web para guardar una observacion DeAtencionClinica
        [OperationContract]
        [WebInvoke(UriTemplate = "/GuardarObservacionDeAtencionClinica", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso GuardarObservacionDeAtencionClinica(ObservacionDeAtencionClinica observacionDeAtencionClinica);

        // Contrato del servicio web para obtener la lista de observaciones DeAtencionClinicas
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerSelectListObservacionDeAtencionClinica", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<List<ObservacionDeAtencionClinica>> ObtenerSelectListObservacionDeAtencionClinica();
        // Contrato del servicio web para eliminar una observacion DeAtencionClinica
        [OperationContract]
        [WebInvoke(UriTemplate = "/EliminarObservacionDeAtencionClinica", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "DELETE", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso EliminarObservacionDeAtencionClinica(ObservacionDeAtencionClinica observacionDeAtencionClinica);
    }
}
