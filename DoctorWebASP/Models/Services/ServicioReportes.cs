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

                var accion = "Reportes";
                var requestUrl = $"{accion}/{ReporteTipo.preestablecido.ToString()}/1";
                var solicitud = new RestRequest(resource: requestUrl, method: Method.GET);
                if (String.IsNullOrEmpty(fechaInicio) || String.IsNullOrEmpty(fechaFin))
                    throw Fabrica.CrearExcepcion("La fecha de inicio o fecha fin están vacías o son nulas");
                solicitud.AddQueryParameter("fechaInicio", fechaInicio);
                solicitud.AddQueryParameter("fechaFin", fechaFin);

                var respuesta = cliente.Execute(solicitud);

                if (respuesta != null && respuesta.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject) JsonConvert.DeserializeObject(respuesta.Content);
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

        public double getPromedioCitasCanceladasPorMedico(string fechaInicioStr, string fechaFinStr)
        {
            throw new NotImplementedException();
        }

        public double getPromedioCitasPorMedico()
        {
            throw new NotImplementedException();
        }

        public double getPromedioEdadPaciente()
        {
            throw new NotImplementedException();
        }

        public ResultadoProceso getPromedioRecursosDisponibles(string fechaInicio, string fechaFin)
        {

            try
            {
                var cliente = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioReportes"));

                var accion = "Reportes";
                var requestUrl = $"{accion}/{ReporteTipo.preestablecido.ToString()}/4";
                var solicitud = new RestRequest(resource: requestUrl, method: Method.GET);
                if (String.IsNullOrEmpty(fechaInicio) || String.IsNullOrEmpty(fechaFin))
                    throw Fabrica.CrearExcepcion("La fecha de inicio o fecha fin están vacías o son nulas");
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

        public double getPromedioUsoApp()
        {
            throw new NotImplementedException();
        }
    }
}