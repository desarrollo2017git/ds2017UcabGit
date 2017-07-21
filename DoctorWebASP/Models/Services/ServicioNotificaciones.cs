using DoctorWebASP.Controllers.Helpers;
using DoctorWebASP.Models.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoctorWebASP.Models.Services
{
    /// <summary>
    /// Esta clase permite instanciar un objeto que te da acceso a los servios web para trabajar con las notificaciones.
    /// </summary>
    public class ServicioNotificaciones : IServicioNotificaciones
    {
        #region Instancia

        /// <summary>
        /// Este medoto permite obtener las notificaciones paginando segun se indique y filtrando por el nombre si es necesario.
        /// </summary>
        /// <param name="cantidadPaginas">Cantidad de paginas segun la cantidad de filas.</param>
        /// <param name="nombre">Permite filtrar los datos usando el nombre.</param>
        /// <param name="pagina">Numero de pagina que se esta solicitando.</param>
        /// <param name="cantidadFilas">Cantidad de registros por pagina.</param>
        /// <returns>Lista de notificaciones, puede estar vacia en caso de no encontrar.</returns>
        public List<Notificacion> ObtenerTodos(out int cantidadPaginas, string nombre, int pagina, int cantidadFilas)
        {
            try
            {
                var lista = new List<Notificacion>();
                var cliente = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioNotificaciones"));

                var accion = "ObtenerTodos";
                var solicitud = new RestRequest(resource: accion, method: Method.GET);

                if (!String.IsNullOrEmpty(nombre))
                    solicitud.AddQueryParameter("nombre", nombre);
                solicitud.AddQueryParameter("indice", pagina.ToString());
                solicitud.AddQueryParameter("filas", cantidadFilas.ToString());

                var respuesta = cliente.Execute(solicitud);

                if (respuesta != null && respuesta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(respuesta.Content);
                    var resultado = datos[$"{accion}Result"].ToObject<ResultadoServicioPaginado<Notificacion>>();
                    if (resultado != null && resultado.SinProblemas)
                    {
                        cantidadPaginas = resultado.CantidadPaginas;
                        return resultado.Contenido.ToList();
                    }
                    else
                        throw Fabrica.CrearExcepcion(mensaje: resultado.Mensaje);
                }
                throw Fabrica.CrearExcepcion(mensaje: "No finalizo correctamente");
            }
            catch (DoctorWebException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw Fabrica.CrearExcepcion(interna: e);
            }
        }

        /// <summary>
        /// Este metodo permite obtener una notificacion a partir del codigo que se le indique.
        /// </summary>
        /// <param name="codigo">Codigo de la notificacion.</param>
        /// <returns>Retorna la notificacion en caso de encontrar registro, si no es nulo.</returns>
        public Notificacion Obtener(int codigo)
        {
            try
            {
                var cliente = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioNotificaciones"));

                var accion = "Obtener";
                var solicitud = new RestRequest(resource: accion, method: Method.GET);

                solicitud.AddQueryParameter("codigo", codigo.ToString());

                var respuesta = cliente.Execute(solicitud);

                if (respuesta != null && respuesta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(respuesta.Content);
                    var resultado = datos["ObtenerResult"].ToObject<ResultadoServicio<Notificacion>>();
                    if (resultado != null && resultado.SinProblemas)
                    {
                        return resultado.Contenido;
                    }
                    else
                        throw Fabrica.CrearExcepcion(mensaje: resultado.Mensaje);
                }
                throw Fabrica.CrearExcepcion(mensaje: "No finalizo correctamente");
            }
            catch (DoctorWebException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw Fabrica.CrearExcepcion(interna: e);
            }
        }

        /// <summary>
        /// Este metodo permite guardar los cambios de la notificacion que se indica.
        /// </summary>
        /// <param name="mensaje">Mensaje de respuesta del servicio.</param>
        /// <param name="notificacion">Notificacion a guardar</param>
        /// <returns>Indica si finalizo correctamente o no.</returns>
        public bool Guardar(out string mensaje, Notificacion notificacion)
        {
            var lista = new List<Notificacion>();
            var cliente = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioNotificaciones"));
            try
            {
                var accion = "Guardar";
                var solicitud = new RestRequest(resource: accion, method: Method.POST);
                var cuerpo = new { notificacion = notificacion };

                solicitud.AddHeader("Content-Type", "application/json");
                solicitud.AddJsonBody(cuerpo);
                var response = cliente.Execute(solicitud);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos["GuardarResult"].ToObject<ResultadoProceso>();
                    if (resultado != null && resultado.SinProblemas)
                    {
                        mensaje = resultado.Mensaje;
                        return resultado.SinProblemas;
                    }
                    else
                        throw Fabrica.CrearExcepcion(mensaje: resultado.Mensaje);
                }
                throw Fabrica.CrearExcepcion(mensaje: "No finalizo correctamente");
            }
            catch (DoctorWebException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw Fabrica.CrearExcepcion(interna: e);
            }
        }

        /// <summary>
        /// Este metodo permite borrarla notificacion que conindicide con el codigo indicado.
        /// </summary>
        /// <param name="mensaje">Mensaje de respuesta del servicio.</param>
        /// <param name="codigo">Codigo de notificacion a borrar</param>
        /// <returns>Indica si finalizo correctamente o no.</returns>
        public bool Borrar(out string mensaje, int codigo)
        {
            var cliente = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioNotificaciones"));
            try
            {
                var accion = "Borrar";
                var solicitud = new RestRequest(resource: accion, method: Method.DELETE);

                solicitud.AddQueryParameter("codigo", codigo.ToString());

                var respuesta = cliente.Execute(solicitud);
                if (respuesta != null && respuesta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(respuesta.Content);
                    var resultado = datos["BorrarResult"].ToObject<ResultadoProceso>();
                    if (resultado != null && resultado.SinProblemas)
                    {
                        mensaje = resultado.Mensaje;
                        return resultado.SinProblemas;
                    }
                    else
                        throw Fabrica.CrearExcepcion(mensaje: resultado.Mensaje);
                }
                throw Fabrica.CrearExcepcion(mensaje: "No finalizo correctamente");
            }
            catch (DoctorWebException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw Fabrica.CrearExcepcion(interna: e);
            }
        }
        #endregion
    }
}