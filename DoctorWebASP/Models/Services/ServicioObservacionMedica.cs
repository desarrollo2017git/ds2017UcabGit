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
    public class ServicioObservacionMedica : IServicioObservacionMedica
    {
        /// <summary>
        /// Metodo del cliente que realiza el llamado para eliminar una Observacion Medica
        /// </summary>
        /// <param name="observacionMedica">Observacion a Eliminar</param>
        public void EliminarObservacionMedica(ObservacionMedica observacionMedica)
        {
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioObservacionMedica"));

                var action = "EliminarObservacionMedica";
                var request = new RestRequest(resource: action, method: Method.DELETE);
                request.RequestFormat = DataFormat.Json;
                var settings = new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
                var body = new { observacionMedica = observacionMedica };
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
        /// Metodo del cliente que realiza el llamado para Guardar una observacion medica
        /// </summary>
        /// <param name="observacionMedica">observacion Medica que se guardará</param>
        public void GuardarObservacionMedica(ObservacionMedica observacionMedica)
        {
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioObservacionMedica"));

                var action = "GuardarObservarcionMedica";
                var request = new RestRequest(resource: action, method: Method.POST);
                request.RequestFormat = DataFormat.Json;
                var settings = new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
                var body = new { observacionMedica = observacionMedica };
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
        /// Metodo del cliente que realiza el llamado al servicio
        /// para obtener una observacion medica especifica
        /// </summary>
        /// <param name="id">Id de la observacion medica que se desea obtener</param>
        /// <returns>Observacion Medica</returns>                
        public ObservacionMedica ObtenerObservacionMedica(int id)
        {

       

            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioObservacionMedica"));

                var action = "ObtenerObservacionMedica";
                var request = new RestRequest(resource: action, method: Method.GET);
                request.AddQueryParameter("id", id.ToString());
                //var json = JsonConvert.SerializeObject(body);

                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos[$"{action}Result"].ToObject<ResultadoServicio<ObservacionMedica>>();
                    if (resultado != null && resultado.SinProblemas)
                    {
                        return resultado.Contenido;
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

        /// <summary>
        /// Metodo en el cliente utilizado para obtener un paciente en especifico
        /// </summary>
        /// <param name="userId">Identificador de usuario del paciente</param>
        /// <returns>Paciente</returns>
        public Paciente ObtenerPaciente(string userId)
        {

            //return db.Personas.OfType<Paciente>().Single(p => p.ApplicationUser.Id == userId);
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCitas"));

                var action = "ObtenerPaciente";
                var request = new RestRequest(resource: action, method: Method.GET);
                request.AddQueryParameter("userId", userId.ToString());
                //var json = JsonConvert.SerializeObject(body);

                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos[$"{action}Result"].ToObject<ResultadoServicio<Paciente>>();
                    if (resultado != null && resultado.SinProblemas)
                    {
                        return resultado.Contenido;
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


    }
}