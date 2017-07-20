using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.Service;
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
        ResultadoServicio<Paciente> ObtenerPaciente(string userId);

        [OperationContract]
        ResultadoServicio<Calendario> ObtenerCalendario(int calendarioId);

        [OperationContract]
        [WebInvoke(UriTemplate = "/GuardarCita", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso GuardarCita(Cita cita, Calendario calendario);

        [OperationContract]
        ResultadoServicio<Medico> ObtenerMedico(string userId);

        [OperationContract]
        ResultadoServicio<List<Cita>> ObtenerListaCitas(string userId);

        [OperationContract]
        ResultadoServicio<List<Cita>> ObtenerCitasDoctor(string userId);

        [OperationContract]
        ResultadoServicio<List<CentroMedico>> ObtenerSelectListCentrosMedicos();

        [OperationContract]
        ResultadoServicio<List<EspecialidadMedica>> ObtenerEsMedicasPorMedicosEnCentroMedico(CentroMedico cMedico);

        [OperationContract]
        ResultadoServicio<EspecialidadMedica> ObtenerEspecialidadMedica(int espMedica);

        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerSelectListMedicosQueTrabajanEnCentroMedico?centroMedicoId={centroMedicoId}&espMedica={espMedica}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<List<Medico>> ObtenerSelectListMedicosQueTrabajanEnCentroMedico(int centroMedicoId, int espMedica);

        [OperationContract]
        ResultadoServicio<Cita> ObtenerCita(int id);

        [OperationContract]
        [WebInvoke(UriTemplate = "/EliminarCita", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "DELETE", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso EliminarCita(Cita cita, Calendario calendario);
    }
}
