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
    // Interface de los contratos para los servicios web.
    [ServiceContract]
    public interface IServicioObservacionMedica
    {

        // Contrato inicial del servicio web para realizar prueba
        [OperationContract]
        [WebGet(UriTemplate = "/DoWork", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]

        void DoWork();

        // Contrato del servicio web para obtener una observacion medica 
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerObservacionMedica?observacionMedicaId={observacionMedicaId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<ObservacionMedica> ObtenerObservacionMedica(int observacionMedicaId);

        // Contrato del servicio medico para obtener un paciente especifico
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerPaciente?userId={userId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<Paciente> ObtenerPaciente(string userId);

        // Contrato del servicio web para guardar una observacion medica
        [OperationContract]
        [WebInvoke(UriTemplate = "/GuardarObservacionMedica", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso GuardarObservacionMedica(ObservacionMedica observacionMedica);

        // Contrato del servicio web para eliminar una observacion medica
        [OperationContract]
        [WebInvoke(UriTemplate = "/EliminarObservacionMedica", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "DELETE", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso EliminarObservacionMedica(ObservacionMedica observacionMedica);

    }
}