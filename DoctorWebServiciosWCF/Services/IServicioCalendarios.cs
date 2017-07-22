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
    }
}
