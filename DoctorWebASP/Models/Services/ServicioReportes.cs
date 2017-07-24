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
    public class ServicioReportes : IServicioReportes
    {
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


        public void comprobarFecha(string fechaInicio, string fechaFin)
        {
            if (String.IsNullOrEmpty(fechaInicio) || String.IsNullOrEmpty(fechaFin))
                throw Fabrica.CrearExcepcion("La fecha de inicio o fecha fin están vacías o son nulas");
        }
    }
}