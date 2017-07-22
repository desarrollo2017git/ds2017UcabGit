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
    /// <summary>
    /// Interfaz con las primitivas que debe implementar la clase Servicio de Notificaciones.
    /// </summary>
    [ServiceContract]
    public interface IServicioNotificaciones
    {
        /// <summary>
        /// Este medoto permite obtener las notificaciones paginando segun se indique y filtrando por el nombre si es necesario.
        /// </summary>
        /// <param name="nombre">Permite filtrar los datos usando el nombre.</param>
        /// <param name="pagina">Numero de pagina que se esta solicitando.</param>
        /// <param name="cantidadFilas">Cantidad de registros por pagina.</param>
        /// <returns>Indica el resultado del proceso</returns>
        [OperationContract]
        [WebGet(UriTemplate = "/obtenerTodos?nombre={nombre}&indice={indicePagina}&filas={numeroFilas}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicioPaginado<Notificacion> ObtenerTodos(string nombre, int indicePagina = 0, int numeroFilas = 30);

        /// <summary>
        /// Este metodo permite obtener una notificacion a partir del codigo que se le indique.
        /// </summary>
        /// <param name="codigo">Codigo de la notificacion.</param>
        /// <returns>Indica el resultado del proceso</returns>
        [OperationContract]
        [WebGet(UriTemplate = "/obtener?codigo={codigo}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoServicio<Notificacion> Obtener(string codigo);

        /// <summary>
        /// Este metodo permite guardar los cambios de la notificacion que se indica.
        /// </summary>
        /// <param name="notificacion">Notificacion a guardar</param>
        /// <returns>Indica el resultado del proceso</returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "/guardar", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso Guardar(Notificacion notificacion);

        /// <summary>
        /// Este metodo permite borrarla notificacion que conindicide con el codigo indicado.
        /// </summary>
        /// <param name="codigo">Codigo de notificacion a borrar</param>
        /// <returns>Indica el resultado del proceso</returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "/borrar?codigo={codigo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, Method = "DELETE")]
        ResultadoProceso Borrar(string codigo);

        /// <summary>
        /// Este metodo permite ejecutar una notificacion e indicar el contenido a travez de la cabecera de la solucitud.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="correo"></param>
        /// <returns>Indica el resultado del proceso</returns>
        [OperationContract]
        [WebGet(UriTemplate = "/Enviar?notificacion={nombre}&a={correo}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ResultadoProceso Enviar(string nombre, string correo);

    }
}
