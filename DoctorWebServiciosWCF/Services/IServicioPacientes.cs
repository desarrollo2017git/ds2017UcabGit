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

    [ServiceContract]
    public interface IServicioPacientes
    {

        [OperationContract]
        [WebGet(UriTemplate = "/DoWork", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        void DoWork();

        // Contrato del servicio web para eliminar una cita
        [OperationContract]
        [WebInvoke(UriTemplate = "/EliminarPaciente", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "DELETE", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso EliminarPaciente(Paciente paciente);

        // Contrato del servicio web para guardar una Cita
        [OperationContract]
        [WebInvoke(UriTemplate = "/GuardarPaciente", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso GuardarPaciente(Paciente paciente);

        // Contrato del servicio medico para obtener un paciente especifico
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerPaciente?userId={userId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<Paciente> ObtenerPaciente(string userId);

        // Contrato del servicio web para obtener la lista de seguros
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerSelectListSeguros", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<List<Seguro>> ObtenerSelectListSeguros();
    }
}
