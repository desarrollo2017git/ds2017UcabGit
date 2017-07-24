using DoctorWebServiciosWCF.Models.Results;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DoctorWebServiciosWCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IReporteService" in both code and config file together.
    [ServiceContract]
    public interface IServicioReportes
    {
        [OperationContract]
        [WebGet(UriTemplate = "/obtener/{codigo}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string DoWork(string codigo);

        [OperationContract]
        [WebGet(UriTemplate = "/reportes/preestablecidos/{codigo}?fechaInicio={fechaInicio}&fechaFin={fechaFin}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso ReportesPreestablecidos(string codigo, string fechaInicio, string fechaFin);

        [OperationContract]
        [WebInvoke(UriTemplate = "obtenerAtributos", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        //ResultadoServicio<JObject> ObtenerAtributos(List<string> entidades);
        ResultadoServicio<object> ObtenerAtributos(List<string> entidades);

        [OperationContract]
        [WebInvoke(UriTemplate = "obtenerMetricas", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<String> ObtenerMetricas(List<string> atributos);

        [OperationContract]
        [WebInvoke(UriTemplate = "/reportes/configurados", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        void ReportesConfigurados(Dictionary<string, string> datos);
    }
}
