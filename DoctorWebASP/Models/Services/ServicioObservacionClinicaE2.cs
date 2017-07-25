using DoctorWebASP.Controllers.Helpers;
using DoctorWebASP.Models.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DoctorWebASP.Controllers;
using Microsoft.AspNet.Identity;


namespace DoctorWebASP.Models.Services
{
    public class ServicioObservacionClinicaE2 : IServicioObservacionClinicaE2
    {
        /// <summary>
        /// Metodo del cliente que realiza el llamado para eliminar una observacion clinica
        /// </summary>
        /// <param name="observacionClinicaE2">observacion a Eliminar</param>
        public void EliminarObservacionClinicaE2(ObservacionClinicaE2 observacionClinicaE2)
        {
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioObservacionClinicaE2"));

                var action = "EliminarObservacionClinicaE2";
                var request = new RestRequest(resource: action, method: Method.DELETE);
                request.RequestFormat = DataFormat.Json;
                var settings = new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
                var body = new { observacionClinicaE2 = observacionClinicaE2 };
                var json = JsonConvert.SerializeObject(body, settings);
                request.AddParameter("application/json", json, null, ParameterType.RequestBody);

                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos[$"{action}Result"].ToObject<ResultadoProceso>();
                    if (resultado != null && resultado.SinProblemas)
                    {
                        return;
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
        /// Metodo del cliente que realiza el llamado para Guardar una observacion Clinica
        /// </summary>
        /// <param name="observacionClinicaE2">observacion que se guardará</param>
        public void GuardarObservacionClinicaE2(ObservacionClinicaE2 observacionClinicaE2)
        {
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioObservacionClinicaE2"));

                var action = "GuardarObservacionClinicaE2";
                var request = new RestRequest(resource: action, method: Method.POST);
                request.RequestFormat = DataFormat.Json;
                var settings = new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
                var body = new { observacionClinicaE2 = observacionClinicaE2 };
                var json = JsonConvert.SerializeObject(body, settings);
                request.AddParameter("application/json", json, null, ParameterType.RequestBody);


                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos[$"{action}Result"].ToObject<ResultadoProceso>();
                    if (resultado != null && resultado.SinProblemas)
                    {
                        return;
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
        /// Metodo del cliente para obtener la lista de observaciones Clinicas
        /// </summary>
        /// <returns>Lista de observaciones Clinicas</returns>
        public List<ObservacionClinicaE2> ObtenerSelectListObservacionClinicaE2()
        {
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioObservacionClinicaE2"));


                var action = "ObtenerSelectListObservacionClinicaE2";
                var request = new RestRequest(resource: action, method: Method.GET);

                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos[$"{action}Result"].ToObject<ResultadoServicio<List<ObservacionClinicaE2>>>();
                    if (resultado != null && resultado.SinProblemas)
                    {
                        return resultado.Contenido.ToList();
                    }
                    else
                        throw new DoctorWebException(resultado.Mensaje);
                }
                else
                {
                    throw new DoctorWebException("No finalizo");
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /*
        public SelectList ObtenerSelectListObservacionClinica()
        {
            //return new SelectList(db.CentrosMedicos.ToList(), "Rif", "Nombre");
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioObservacionClinica"));


                var action = "ObtenerSelectListObservacionClinica";
                var request = new RestRequest(resource: action, method: Method.GET);
                //request.AddQueryParameter("userId", userId);
                //var json = JsonConvert.SerializeObject(body);

                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos[$"{action}Result"].ToObject<ResultadoServicio<List<ObservacionClinica>>>();
                    if (resultado != null && resultado.SinProblemas)
                    {
                        SelectList selectList = new SelectList(resultado.Contenido, "Diagnostico", "Indicacion", "Paciente");
                        return selectList;
                    }
                    else
                        throw new DoctorWebException(resultado.Mensaje);
                }
                else
                {
                    throw new DoctorWebException("No finalizo");
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        */

    }
}