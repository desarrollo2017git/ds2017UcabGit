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
        /// Metodo utilizado para obtener una lista de medicos realcionados, suministrando el identificador de su usuario
        /// </summary>
        /// <param name="userId">Identificador de usuario del medico</param>
        /// <returns>Una lista con los medicos asociados</returns>
        public List<Medico> ObtenerMedico(string userId)
        {
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCalendarios"));

                var action = "ObtenerMedico";
                var request = new RestRequest(resource: action, method: Method.GET);
                request.AddQueryParameter("userId", userId);
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
        /// Metodo  utilizado para obtener una lista de pacientes suministrando el identificador de su usuario
        /// </summary>
        /// <param name="userId">Identificador de usuario del paciente</param>
        /// <returns>Paciente</returns>
        public List<Paciente> ObtenerPaciente(string userId)
        {
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCalendarios"));
                var action = "ObtenerPaciente";
                var request = new RestRequest(resource: action, method: Method.GET);
                request.AddQueryParameter("userId", userId);
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
        /// Metodo que sirve paraobtener una lista de los horarios de un medico proporcionando su identificador
        /// disponibles de un doctor
        /// </summary>
        /// <param name="medicoId">Identificador del doctor</param>
        /// <returns>Lista de horarios</returns>
        public List<Calendario> ObtenerTiempoDoctor(int medicoId)
        {
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCalendarios"));
                var action = "ObtenerTiempoDoctor";
                var request = new RestRequest(resource: action, method: Method.GET);
                request.AddQueryParameter("medicoId", medicoId.ToString());
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
        /// <summary>
        /// Metodo que al pasarle el identificador de un medico, devuelve una lista con sus citas agendadas
        /// </summary>
        /// <param name="medicoId"> Número único que identifica a cada medico</param>
        /// <returns> Una lista de calendario que corresponde a las citas agendadas del medico </returns>
        public List<Calendario> ObtenerCitasDoctor(int medicoId)
        {
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCalendarios"));
                var action = "ObtenerCitasDoctor";
                var request = new RestRequest(resource: action, method: Method.GET);
                request.AddQueryParameter("medicoId", medicoId.ToString());
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

        /// <summary>
        /// Método que recibe el identificador de un calendario y retorna el paciente asociado 
        /// </summary>
        /// <param name="calendarioId"> Número identificador del calendario </param>
        /// <returns> Un objeto paciente asociado al calendario suministrado </returns>
        public Paciente ObtenerPacienteCalendario(int calendarioId)
        {
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCalendarios"));

                var action = "ObtenerPacienteCalendario";
                var request = new RestRequest(resource: action, method: Method.GET);
                request.AddQueryParameter("calendarioId", calendarioId.ToString());
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

        /// <summary>
        /// Este método crea y registra un calendario recibiendo un objeto de este tipo
        /// </summary>
        /// <param name="cal"> Un objeto calendario a ser agregado al sistema </param>
        /// <returns> El objeto agregado, que al ser validado se corrobora que la creacion fue exitosa </returns>
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

        /// <summary>
        /// Este método se encarga de eliminar del sistema al objeto suministrado
        /// </summary>
        /// <param name="cal"> Un objeto calendario para eliminar </param>
        /// <returns> El objeto calendario que se eliminó, de manera que sirva para comprobar que la eliminación fue exitosa </returns>
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

        /// <summary>
        /// Método que recibe el identificador de un paciente y retorna una lista con todas sus citas
        /// </summary>
        /// <param name="pacienteId"> El codigo identificador de un paciente </param>
        /// <returns> Una lista donde estarán todas sus citas </returns>
        public List<Calendario> ObtenerCitasPaciente(int pacienteId)
        {
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCalendarios"));
                var action = "ObtenerCitasPaciente";
                var request = new RestRequest(resource: action, method: Method.GET);
                request.AddQueryParameter("pacienteId", pacienteId.ToString());
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

        /// <summary>
        /// Metodo que consigue un objeto médico al pasarle el identificador de un objeto calendario que tenga asociado
        /// </summary>
        /// <param name="calendarioId"> El códogo identificador del calendario </param>
        /// <returns> El objeto médico asociado al calendario del codigo suministrado </returns>
        public Medico ObtenerMedicoCalendario(int calendarioId)
        {
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCalendarios"));
                var action = "ObtenerMedicoCalendario";
                var request = new RestRequest(resource: action, method: Method.GET);
                request.AddQueryParameter("calendarioId", calendarioId.ToString());
                var response = client.Execute(request);
                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos[$"{action}Result"].ToObject<ResultadoServicio<Medico>>();
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