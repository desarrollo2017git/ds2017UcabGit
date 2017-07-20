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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServicioCitas" in both code and config file together.
    [ServiceContract]
    public interface IServicioCitas
    {
        [OperationContract]
        [WebGet(UriTemplate = "/DoWork", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]

        void DoWork();

        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerCentroMedico?centroMedicoId={centroMedicoId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<CentroMedico> ObtenerCentroMedico(int centroMedicoId);

        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerPaciente?userId={userId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]

        ResultadoServicio<Paciente> ObtenerPaciente(string userId);

        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerCalendario?calendarioId={calendarioId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<Calendario> ObtenerCalendario(int calendarioId);

        [OperationContract]
        [WebInvoke(UriTemplate = "/GuardarCita", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso GuardarCita(Cita cita, Calendario calendario);

        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerMedico?userId={userId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<Medico> ObtenerMedico(string userId);

        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerListaCitas?userId={userId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<List<Cita>> ObtenerListaCitas(string userId);

        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerCitasDoctor?userId={userId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<List<Cita>> ObtenerCitasDoctor(string userId);

        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerSelectListCentrosMedicos", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<List<CentroMedico>> ObtenerSelectListCentrosMedicos();

        //[OperationContract]
        //[WebGet(UriTemplate = "/ObtenerEsMedicasPorMedicosEnCentroMedico?dMedico={cMedico}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        //ResultadoServicio<List<EspecialidadMedica>> ObtenerEsMedicasPorMedicosEnCentroMedico(CentroMedico cMedico);

        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerEspecialidadMedica?espMedica={espMedica}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<EspecialidadMedica> ObtenerEspecialidadMedica(int espMedica);

        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerSelectListMedicosQueTrabajanEnCentroMedico?centroMedicoId={centroMedicoId}&espMedica={espMedica}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<List<Medico>> ObtenerSelectListMedicosQueTrabajanEnCentroMedico(int centroMedicoId, int espMedica);

        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerCita?id={id}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<Cita> ObtenerCita(int id);

        [OperationContract]
        [WebInvoke(UriTemplate = "/EliminarCita", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "DELETE", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso EliminarCita(Cita cita, Calendario calendario);
    }
}
