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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IServicioCalendarios" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServicioCalendarios
    {

        void DoWork();

        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerMedico?userId={userId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<List<Medico>> ObtenerMedico(string userId);

        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerPaciente?userId={userId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<List<Paciente>> ObtenerPaciente(string userId);
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerTiempoDoctor?medicoId={medicoId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<List<Calendario>> ObtenerTiempoDoctor(int medicoId);
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerCitasDoctor?medicoId={medicoId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<List<Calendario>> ObtenerCitasDoctor(int medicoId);
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerPacienteCalendario?calendarioId={calendarioId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<Paciente> ObtenerPacienteCalendario(int calendarioId);
        [OperationContract]
        [WebInvoke(UriTemplate = "/GuardarCalendario", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso GuardarCalendario(Calendario calendario);
        [OperationContract]
        [WebInvoke(UriTemplate = "/EliminarCalendario", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "DELETE", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso EliminarCalendario(Calendario calendario);
        [WebGet(UriTemplate = "/ObtenerCitasPaciente?pacienteId={pacienteId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<List<Calendario>> ObtenerCitasPaciente(int pacienteId);
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerMedicoCalendario?calendarioId={calendarioId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<Medico> ObtenerMedicoCalendario(int calendarioId);
    }
}
