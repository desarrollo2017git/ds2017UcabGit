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
    // Se establece interface IServicioObservacionMedica como Contrato de servico Web
    [ServiceContract]
    public interface IServicioObservacionMedica
    {

        // Contrato inicial del servicio web para realizar prueba
        [OperationContract]
        [WebGet(UriTemplate = "/DoWork", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]

        void DoWork();

        // Contrato del servicio web para guardar una observacion medica
        [OperationContract]
        [WebInvoke(UriTemplate = "/GuardarObservacionMedica", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso GuardarObservacionMedica(ObservacionMedica observacionMedica);

        // Contrato del servicio web para obtener la lista de observaciones medicas
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerSelectListObservacionMedica", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<List<ObservacionMedica>> ObtenerSelectListObservacionMedica();
        // Contrato del servicio web para eliminar una observacion medica
        [OperationContract]
        [WebInvoke(UriTemplate = "/EliminarObservacionMedica", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "DELETE", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso EliminarObservacionMedica(ObservacionMedica observacionMedica);
    }
}
