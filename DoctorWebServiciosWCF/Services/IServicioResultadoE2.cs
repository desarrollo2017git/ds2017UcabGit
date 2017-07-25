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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IServicioResultadoE2" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServicioResultadoE2
    {
        // Contrato inicial del servicio web para realizar prueba
        [OperationContract]
        [WebGet(UriTemplate = "/DoWork", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]

        void DoWork();

        // Contrato del servicio web para guardar un resultado de examen medico
        [OperationContract]
        [WebInvoke(UriTemplate = "/GuardarResultadoE2", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso GuardarResultadoE2(ResultadoE2 resultadoE2);

        // Contrato del servicio web para obtener la lista de resultados
        [OperationContract]
        [WebGet(UriTemplate = "/ObtenerSelectListResultadoE2", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<List<ResultadoE2>> ObtenerSelectListResultadoE2();
        // Contrato del servicio web para eliminar un resultado de examen
        [OperationContract]
        [WebInvoke(UriTemplate = "/EliminarResultadoE2", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "DELETE", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso EliminarResultadoE2(ResultadoE2 resultadoE2);
    }
}
}
