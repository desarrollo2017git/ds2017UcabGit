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
    public interface IServicioCalendarios
    {

        // Contrato inicial del servicio web para realizar prueba
        [OperationContract]
        [WebGet(UriTemplate = "/DoWork", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]

        void DoWork();

        // Contrato del servicio web para obtener un centro medico especifico
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerCentroMedico?centroMedicoId={centroMedicoId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<CentroMedico> ObtenerCentroMedico(int centroMedicoId);

        // Contrato del servicio web para obtener un centro medico a partir de su RIF
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerCentroMedicoRif?centroMedicoRif={centroMedicoRif}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<CentroMedico> ObtenerCentroMedicoRif(string centroMedicoRif);

        // Contrato del servicio medico para obtener un paciente especifico
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerPaciente?userId={userId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<Paciente> ObtenerPaciente(string userId);

        // Contrato del servicio web para obtener un calendario especifico
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerCalendario?calendarioId={calendarioId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<Calendario> ObtenerCalendario(int calendarioId);

        // Contrato del servicio web para guardar una Cita
        [OperationContract]
        [WebInvoke(UriTemplate = "/GuardarCita", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso GuardarCita(Cita cita, Calendario calendario);

        // Contrato del servicio web para obtener un medico especifico
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerMedico?userId={userId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<Medico> ObtenerMedico(string userId);

        // Contrato del servicio web para obtener la lista de citas de un paciente
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerListaCitas?userId={userId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<List<Cita>> ObtenerListaCitas(string userId);

        // Contrato del servicio web para obtener la lista de citas de un doctor
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerCitasDoctor?userId={userId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<List<Cita>> ObtenerCitasDoctor(string userId);

        // Contrato del servicio web para obtener la lista de centros medicos
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerSelectListCentrosMedicos", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<List<CentroMedico>> ObtenerSelectListCentrosMedicos();

        // Contrato del servicio web para obtener la lista de las especialidades medicas de un centro medico
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerEsMedicasPorMedicosEnCentroMedico?cMedicoId={cMedicoId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<List<EspecialidadMedica>> ObtenerEsMedicasPorMedicosEnCentroMedico(int cMedicoId);

        // Contrato del servicio web para obtener una especialidad medica especifica
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerEspecialidadMedica?espMedica={espMedica}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<EspecialidadMedica> ObtenerEspecialidadMedica(int espMedica);

        // Contrato del servicio web para obtener una lista de medicos que trabajan en un centro medico especifico
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerSelectListMedicosQueTrabajanEnCentroMedico?centroMedicoId={centroMedicoId}&espMedica={espMedica}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<List<Medico>> ObtenerSelectListMedicosQueTrabajanEnCentroMedico(int centroMedicoId, int espMedica);

        // Contrato del servicio web para obtener una cita especifica
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerCita?id={id}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<Cita> ObtenerCita(int id);

        // Contrato del servicio web para eliminar una cita
        [OperationContract]
        [WebInvoke(UriTemplate = "/EliminarCita", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "DELETE", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso EliminarCita(Cita cita, Calendario calendario);

        // Contrato del servicio web para obtener una lista de horarios/calendarios disponibles del medico
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerListaDisponibilidad?medicoId={medicoId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<List<Calendario>> ObtenerListaDisponibilidad(int medicoId);

        // Contrato del servicio para obtener el medico asignado a una cita especifica
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerMedicoAsignadoACita?citaId={citaId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<Medico> ObtenerMedicoAsignadoACita(int citaId);

        // Contrato del servicio web para obtener la especialidad medica de un medico especifico
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerEspecialidadMedicaDelDoctor?medicoId={medicoId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<EspecialidadMedica> ObtenerEspecialidadMedicaDelDoctor(int medicoId);
    }
}
