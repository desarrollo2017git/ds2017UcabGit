using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.Results;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DoctorWebServiciosWCF.Services
{
    /// <summary>
    /// Interfaz con las primitivas que debe implementar la clase Servicio del módulo de Reportes.
    /// </summary>
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IReporteService" in both code and config file together.
    [ServiceContract]
    public interface IServicioReportes
    {
        [OperationContract]
        [WebGet(UriTemplate = "/obtener/{codigo}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string DoWork(string codigo);

        /// <summary>
        /// Método utilizado para indicar que operación se debe realizar según los parámetros indicados.
        /// </summary>
        /// <param name="codigo">Código que indica el id de la operación a realizar.</param>
        /// <param name="fechaInicio">Fecha de inicio del periodo seleccionado.</param>
        /// <param name="fechaFin">Fecha de fin del periodo seleccionado.</param>
        /// <returns>Resultado obtenido en la operación realizada.</returns>
        [OperationContract]
        [WebGet(UriTemplate = "/reportes/preestablecidos/{codigo}?fechaInicio={fechaInicio}&fechaFin={fechaFin}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso ReportesPreestablecidos(string codigo, string fechaInicio, string fechaFin);

        /// <summary>
        /// Método utilizado para llenar una lista de atributos, según el parámetro recibido. 
        /// </summary>
        /// <param name="selectedEntities">Parámetro que indica las entidades seleccionadas.</param>
        /// <returns>Objeto que contiene los atributos de las entidades seleccionadas.</returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "obtenerAtributos", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        //ResultadoServicio<JObject> ObtenerAtributos(List<string> entidades);
        ResultadoServicio<object> ObtenerAtributos(List<string> entidades);

        [OperationContract]
        [WebInvoke(UriTemplate = "/reportes/configurados", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<string> ReportesConfigurados(List<DatosConfigurados> datosConfigurados);
    }
}