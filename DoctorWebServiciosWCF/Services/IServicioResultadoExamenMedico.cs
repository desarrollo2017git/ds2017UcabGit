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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IServicioResultadoExamenMedico" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServicioResultadoExamenMedico
    {
        // Contrato inicial del servicio web para realizar prueba
        [OperationContract]
        [WebGet(UriTemplate = "/DoWork", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]

        void DoWork();

        // Contrato del servicio web para guardar una observacion medica
        [OperationContract]
        [WebInvoke(UriTemplate = "/GuardarResultadoExamenMedico", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso GuardarResultadoExamenMedico(ResultadoExamenMedico resultadoExamenMedico);

        // Contrato del servicio web para obtener la lista de observaciones medicas
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerSelectListResultadoExamenMedico", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<List<ResultadoExamenMedico>> ObtenerSelectListResultadoExamenMedico();
        // Contrato del servicio web para eliminar una observacion medica
        [OperationContract]
        [WebInvoke(UriTemplate = "/EliminarResultadoExamenMedico", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "DELETE", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso EliminarResultadoExamenMedico(ResultadoExamenMedico resultadoExamenMedico);
    }
}
