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
    public class ServicioCalendarios : IServicioCalendarios
    {

        public string ObtenerUsuarioLoggedIn(CalendariosController calendariosController)
        {
            return calendariosController.User.Identity.GetUserId();
        }

        /// <summary>
        /// Metodo en el cliente utilizado para obtener un medico especifico
        /// </summary>
        /// <param name="userId">Identificador de usuario del medico</param>
        /// <returns>Medico</returns>
        public List<Medico> ObtenerMedico(string userId)
        {
            //return db.Personas.OfType<Medico>().Single(p => p.ApplicationUser.Id == userId);
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCalendarios"));

                var action = "ObtenerMedico";
                var request = new RestRequest(resource: action, method: Method.GET);
                request.AddQueryParameter("userId", userId);
                //var json = JsonConvert.SerializeObject(body);

                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos[$"{action}Result"].ToObject<ResultadoServicio<List<Medico>>>();
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
        public List<Paciente> ObtenerPaciente(string userId)
        {

            //return db.Personas.OfType<Paciente>().Single(p => p.ApplicationUser.Id == userId);
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCalendarios"));

                var action = "ObtenerPaciente";
                var request = new RestRequest(resource: action, method: Method.GET);
                request.AddQueryParameter("userId", userId);
                //var json = JsonConvert.SerializeObject(body);

                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos[$"{action}Result"].ToObject<ResultadoServicio<List<Paciente>>>();
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
        /// Metodo en el cliente para obtener una lista de los horarios
        /// disponibles de un doctor
        /// </summary>
        /// <param name="medicoId">Identificador del doctor</param>
        /// <returns>Lista de horarios</returns>
        public List<Calendario> ObtenerTiempoDoctor(int medicoId)
        {
            //Where(m => m.Medico.PersonaId == mdId && m.Disponible == 1).OrderBy(m => m.HoraInicio)
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCalendarios"));


                var action = "ObtenerTiempoDoctor";
                var request = new RestRequest(resource: action, method: Method.GET);
                request.AddQueryParameter("medicoId", medicoId.ToString());
                //request.AddQueryParameter("espMedica", espMedica.ToString());
                //var json = JsonConvert.SerializeObject(body);

                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos[$"{action}Result"].ToObject<ResultadoServicio<List<Calendario>>>();
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

        public List<Calendario> ObtenerCitasDoctor(int medicoId)
        {
            //Where(m => m.Medico.PersonaId == mdId && m.Disponible == 1).OrderBy(m => m.HoraInicio)
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCalendarios"));


                var action = "ObtenerCitasDoctor";
                var request = new RestRequest(resource: action, method: Method.GET);
                request.AddQueryParameter("medicoId", medicoId.ToString());
                //request.AddQueryParameter("espMedica", espMedica.ToString());
                //var json = JsonConvert.SerializeObject(body);

                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos[$"{action}Result"].ToObject<ResultadoServicio<List<Calendario>>>();
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


        public Paciente ObtenerPacienteCalendario(int calendarioId)
        {

            //return db.Personas.OfType<Paciente>().Single(p => p.ApplicationUser.Id == userId);
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCalendarios"));

                var action = "ObtenerPacienteCalendario";
                var request = new RestRequest(resource: action, method: Method.GET);
                request.AddQueryParameter("calendarioId", calendarioId.ToString());
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

        public Calendario GuardarCalendario(Calendario cal)
        {
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCalendarios"));

                var action = "GuardarCalendario";
                var request = new RestRequest(resource: action, method: Method.POST);
                request.RequestFormat = DataFormat.Json;
                var settings = new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
                var body = new {calendario = cal};
                var json = JsonConvert.SerializeObject(body, settings);
                request.AddParameter("application/json", json, null, ParameterType.RequestBody);

                //var json = JsonConvert.SerializeObject(body);


                //request.AddHeader("Content-Type", "application/json");
                //request.AddJsonBody(body);
                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos[$"{action}Result"].ToObject<ResultadoServicio<Calendario>>();
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

        public Calendario EliminarCalendario(Calendario cal)
        {
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCalendarios"));
                var action = "EliminarCalendario";
                var request = new RestRequest(resource: action, method: Method.DELETE);
                request.RequestFormat = DataFormat.Json;
                var settings = new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
                var body = new { calendario = cal };
                var json = JsonConvert.SerializeObject(body, settings);
                request.AddParameter("application/json", json, null, ParameterType.RequestBody);

                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos[$"{action}Result"].ToObject<ResultadoServicio<Calendario>>();
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

    }
}