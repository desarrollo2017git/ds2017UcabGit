using DoctorWebASP.Controllers.Helpers;
using DoctorWebASP.Models.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models.Services
{
    /// <summary>
    /// Esta clase permite instanciar un objeto que da acceso a los servios web para trabajar con los reportes.
    /// </summary>
    public class ServicioReportes : IServicioReportes
    {
        #region REPORTES PREESTABLECIDOS
        #region REPORTE #1 - Cantidad de usuarios registrados en un tiempo determinado
        /// <summary>
        /// Método utilizado para obtener la cantidad de usuarios registrados durante el periodo de tiempo seleccionado por el usuario.
        /// </summary>
        /// <param name="fechaInicioStr">Fecha incicial para el periodo de conteo de registro de usuarios.</param>
        /// <param name="fechaFinStr">Fecha incicial para el periodo de conteo de registro de usuarios.</param>
        /// <exception cref="DoctorWebException">Esta excepción es lanzada en caso de existir algun error en al ejecución.</exception>
        /// <exception cref="System.Exception">Esta es la excepción general, es lanzada en caso de existir un error que no fue atrapado por excepciones especificas.</exception>
        /// <returns>Retorna un objeto "resultado" que indica si fue fue exitosa o fallida la operación.</returns>
        public ResultadoProceso getCantidadUsuariosRegistrados(string fechaInicio, string fechaFin)
        {
            try
            {
                var cliente = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioReportes"));

                var accion = "ReportesPreestablecidos";
                var requestUrl = "reportes/preestablecidos/1";
                var solicitud = new RestRequest(resource: requestUrl, method: Method.GET);
                comprobarFecha(fechaInicio, fechaFin);
                solicitud.AddQueryParameter("fechaInicio", fechaInicio);
                solicitud.AddQueryParameter("fechaFin", fechaFin);

                var respuesta = cliente.Execute(solicitud);

                if (respuesta != null && respuesta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(respuesta.Content);
                    var resultado = datos[$"{accion}Result"].ToObject<ResultadoProceso>();
                    if (resultado != null && resultado.SinProblemas)
                    {
                        return resultado;
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

        #region REPORTE #2 - Promedio de edad de los pacientes.
        /// <summary>
        /// Método utilizado para obtener el promedio de citas atendidas por médico.
        /// </summary>
        /// <exception cref="DoctorWebException">Esta excepción es lanzada en caso de existir algun error en al ejecución.</exception>
        /// <exception cref="System.Exception">Esta es la excepción general, es lanzada en caso de existir un error que no fue atrapado por excepciones especificas.</exception>
        /// <returns>Retorna un objeto "resultado" que indica si fue fue exitosa o fallida la operación.</returns>
        public ResultadoProceso getPromedioEdadPaciente()
        {
            try
            {
                var cliente = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioReportes"));

                var accion = "ReportesPreestablecidos";
                var requestUrl = "reportes/preestablecidos/2";
                var solicitud = new RestRequest(resource: requestUrl, method: Method.GET);

                var respuesta = cliente.Execute(solicitud);

                if (respuesta != null && respuesta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(respuesta.Content);
                    var resultado = datos[$"{accion}Result"].ToObject<ResultadoProceso>();
                    if (resultado != null && resultado.SinProblemas)
                    {
                        return resultado;
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

        #region REPORTE #3 - Promedio de citas por médico.
        /// <summary>
        /// Método utilizado para obtener la edad promedio de los pacientes.
        /// </summary>
        /// <exception cref="DoctorWebException">Esta excepción es lanzada en caso de existir algun error en al ejecución.</exception>
        /// <exception cref="System.Exception">Esta es la excepción general, es lanzada en caso de existir un error que no fue atrapado por excepciones especificas.</exception>
        /// <returns>Retorna un objeto "resultado" que indica si fue fue exitosa o fallida la operación.</returns>
        public ResultadoProceso getPromedioCitasPorMedico()
        {
            try
            {
                var cliente = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioReportes"));

                var accion = "ReportesPreestablecidos";
                var requestUrl = "reportes/preestablecidos/3";
                var solicitud = new RestRequest(resource: requestUrl, method: Method.GET);

                var respuesta = cliente.Execute(solicitud);

                if (respuesta != null && respuesta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(respuesta.Content);
                    var resultado = datos[$"{accion}Result"].ToObject<ResultadoProceso>();
                    if (resultado != null && resultado.SinProblemas)
                    {
                        return resultado;
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

        #region REPORTE #4 - Promedio de recursos disponibles en un tiempo determinado.
        /// <summary>
        /// Método utilizado para obtener el promedio de recursos disponibles en un periodo de tiempo seleccionado por el usuario.
        /// </summary>
        /// <param name="fechaInicioStr">Fecha incicial para el periodo de conteo de registro de usuarios.</param>
        /// <param name="fechaFinStr">Fecha incicial para el periodo de conteo de registro de usuarios.</param>
        /// <exception cref="DoctorWebException">Esta excepción es lanzada en caso de existir algun error en al ejecución.</exception>
        /// <exception cref="System.Exception">Esta es la excepción general, es lanzada en caso de existir un error que no fue atrapado por excepciones especificas.</exception>
        /// <returns>Retorna un objeto "resultado" que indica si fue fue exitosa o fallida la operación.</returns>
        public ResultadoProceso getPromedioRecursosDisponibles(string fechaInicio, string fechaFin)
        {
            try
            {
                var cliente = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioReportes"));

                var accion = "ReportesPreestablecidos";
                var requestUrl = "reportes/preestablecidos/4";
                var solicitud = new RestRequest(resource: requestUrl, method: Method.GET);
                comprobarFecha(fechaInicio, fechaFin);
                solicitud.AddQueryParameter("fechaInicio", fechaInicio);
                solicitud.AddQueryParameter("fechaFin", fechaFin);

                var respuesta = cliente.Execute(solicitud);

                if (respuesta != null && respuesta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(respuesta.Content);
                    var resultado = datos[$"{accion}Result"].ToObject<ResultadoProceso>();
                    if (resultado != null && resultado.SinProblemas)
                    {
                        return resultado;
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

        #region REPORTE #5 - Promedio de uso de la aplicación
        /// <summary>
        /// Método utilizado para obtener el promedio de uso de la aplicación.
        /// </summary>
        /// <exception cref="DoctorWebException">Esta excepción es lanzada en caso de existir algun error en al ejecución.</exception>
        /// <exception cref="System.Exception">Esta es la excepción general, es lanzada en caso de existir un error que no fue atrapado por excepciones especificas.</exception>
        /// <returns>Retorna un objeto "resultado" que indica si fue fue exitosa o fallida la operación.</returns>
        public ResultadoProceso getPromedioUsoApp()
        {
            try
            {
                var cliente = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioReportes"));

                var accion = "ReportesPreestablecidos";
                var requestUrl = "reportes/preestablecidos/5";
                var solicitud = new RestRequest(resource: requestUrl, method: Method.GET);

                var respuesta = cliente.Execute(solicitud);

                if (respuesta != null && respuesta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(respuesta.Content);
                    var resultado = datos[$"{accion}Result"].ToObject<ResultadoProceso>();
                    if (resultado != null && resultado.SinProblemas)
                    {
                        return resultado;
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

        #region REPORTE #6 - Promedio de citas canceladas por médico en un tiempo determinado
        /// <summary>
        /// Método utilizado para obtener el promedio de citas canceladas por médico en un periodo de tiempo seleccionado por el usuario.
        /// </summary>
        /// <param name="fechaInicioStr">Fecha incicial para el periodo de conteo de registro de usuarios.</param>
        /// <param name="fechaFinStr">Fecha incicial para el periodo de conteo de registro de usuarios.</param>
        /// <exception cref="DoctorWebException">Esta excepción es lanzada en caso de existir algun error en al ejecución.</exception>
        /// <exception cref="System.Exception">Esta es la excepción general, es lanzada en caso de existir un error que no fue atrapado por excepciones especificas.</exception>
        /// <returns>Retorna un objeto "resultado" que indica si fue fue exitosa o fallida la operación.</returns>
        public ResultadoProceso getPromedioCitasCanceladasPorMedico(string fechaInicio, string fechaFin)
        {
            try
            {
                var cliente = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioReportes"));

                var accion = "ReportesPreestablecidos";
                var requestUrl = "reportes/preestablecidos/6";
                var solicitud = new RestRequest(resource: requestUrl, method: Method.GET);
                comprobarFecha(fechaInicio, fechaFin);
                solicitud.AddQueryParameter("fechaInicio", fechaInicio);
                solicitud.AddQueryParameter("fechaFin", fechaFin);

                var respuesta = cliente.Execute(solicitud);

                if (respuesta != null && respuesta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(respuesta.Content);
                    var resultado = datos[$"{accion}Result"].ToObject<ResultadoProceso>();
                    if (resultado != null && resultado.SinProblemas)
                    {
                        return resultado;
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
        #endregion

        #region REPORTES CONFIGURADOS
        #region PASO #2: Obtener lista de atributos según las entidades seleccionadas.
        /// <summary>
        /// Método utilizado para llenar una lista de atributos, según el parámetro recibido. 
        /// </summary>
        /// <param name="selectedEntities">Parámetro que indica las entidades seleccionadas.</param>
        /// <exception cref="DoctorWebException">Esta excepción es lanzada en caso de existir algun error en al ejecución.</exception>
        /// <exception cref="System.Exception">Esta es la excepción general, es lanzada en caso de existir un error que no fue atrapado por excepciones especificas.</exception>
        /// <returns>Retorna un objeto "resultado" que indica si fue fue exitosa o fallida la operación.</returns>
        public ResultadoServicio<String> obtenerAtributos(List<string> entities)
        {
            try
            {
                var cliente = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioReportes"));

                var accion = "ObtenerAtributos";
                var solicitud = new RestRequest(resource: accion, method: Method.POST);
                var cuerpo = new { entidades = entities };

                solicitud.AddHeader("Content-Type", "application/json");
                solicitud.AddJsonBody(cuerpo);

                var respuesta = cliente.Execute(solicitud);

                if (respuesta != null && respuesta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(respuesta.Content);
                    var resultado = datos[$"{accion}Result"].ToObject<ResultadoServicio<String>>();
                    if (resultado != null && resultado.SinProblemas)
                    {
                        return resultado;
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

        #region PASO #3: Obtener reportes según la selección del usuario.
        /// <summary>
        /// Método que se encarga de procesar el query, con los datos seleccionados por el cliente.
        /// </summary>
        /// <param name="datosConfigurados">Objeto que contiene todas las opciones seleccionadas por el usuario.</param>
        /// <returns>Retorna un objeto "resultado" que indica si fue fue exitosa o fallida la operación.</returns>
        public ResultadoServicio<string> procesarQuery(List<DatosConfigurados> datosConfigurados)
        {
            try
            {
                var cliente = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioReportes"));

                var accion = "ReportesConfigurados";
                var requestUrl = "reportes/configurados";
                var solicitud = new RestRequest(resource: requestUrl, method: Method.POST);
                var cuerpo = new { datosConfigurados = datosConfigurados };

                solicitud.AddHeader("Content-Type", "application/json");
                solicitud.AddJsonBody(cuerpo);

                var respuesta = cliente.Execute(solicitud);

                if (respuesta != null && respuesta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(respuesta.Content);
                    var resultado = datos[$"{accion}Result"].ToObject<ResultadoServicio<string>>();
                    if (resultado != null && resultado.SinProblemas)
                    {
                        return resultado;
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
        #endregion

        /// <summary>
        /// Método utilizado para comprobar que las fechas ingresadas como parametros de los reportes 1, 4 y 6, sea válida.
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio del periodo seleccionado.</param>
        /// <param name="fechaFin">Fecha de fin del periodo seleccionado.</param>
        public void comprobarFecha(string fechaInicio, string fechaFin)
        {
            if (String.IsNullOrEmpty(fechaInicio) || String.IsNullOrEmpty(fechaFin))
                throw Fabrica.CrearExcepcion("La fecha de inicio o fecha fin están vacías o son nulas");
        }       
    }
}