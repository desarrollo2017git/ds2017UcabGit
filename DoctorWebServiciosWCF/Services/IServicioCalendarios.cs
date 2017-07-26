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

        // Contrato del servicio web para obtener un medico a partir de su codigo de usuario
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerMedico?userId={userId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<List<Medico>> ObtenerMedico(string userId);

        // Contrato del servicio web para obtener un paciente a partir de su codigo de usuario
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerPaciente?userId={userId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<List<Paciente>> ObtenerPaciente(string userId);

        // Contrato del servicio web para obtener el tiempo disponible de un medico a partir de su codigo de identificacion
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerTiempoDoctor?medicoId={medicoId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<List<Calendario>> ObtenerTiempoDoctor(int medicoId);

        // Contrato del servicio web para obtener las citas asociadas a un medico a partir del codigo del mismo
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerCitasDoctor?medicoId={medicoId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<List<Calendario>> ObtenerCitasDoctor(int medicoId);

        // Contrato del servicio web para obtenr el paciente asociado a un calendario específico
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerPacienteCalendario?calendarioId={calendarioId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<Paciente> ObtenerPacienteCalendario(int calendarioId);

        // Contrato del servicio web que sirve para guardar el calendario recibido en el sistema
        [OperationContract]
        [WebInvoke(UriTemplate = "/GuardarCalendario", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<Calendario> GuardarCalendario(Calendario calendario);

        // Contrato del servicio web que sirve para eliminar el calendario recibido del sistema
        [OperationContract]
        [WebInvoke(UriTemplate = "/EliminarCalendario", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "DELETE", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso EliminarCalendario(Calendario calendario);

        // Contrato del servicio web para recibir todas las citas asociadas a un paciente específico
        [WebGet(UriTemplate = "/ObtenerCitasPaciente?pacienteId={pacienteId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<List<Calendario>> ObtenerCitasPaciente(int pacienteId);

        // Contrato del servicio web para recibir el medico asociado a un calendario específico
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerMedicoCalendario?calendarioId={calendarioId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<Medico> ObtenerMedicoCalendario(int calendarioId);
    }
}
