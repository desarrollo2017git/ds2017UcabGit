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
    public class ServicioCitas : IServicioCitas
    {

        public void EliminarCita(Cita cita, Calendario calendario)
        {
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCitas"));

                var action = "EliminarCita";
                var request = new RestRequest(resource: action, method: Method.DELETE);
                var body = new { cita = cita, calendario = calendario };

                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(body);
                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos[$"{action}Result"].ToObject<ResultadoProceso>();
                    if (resultado != null && resultado.SinProblemas)
                    {

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

        public void GuardarCita(Cita cita, Calendario calendario)
        {
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCitas"));

                var action = "GuardarCita";
                var request = new RestRequest(resource: action, method: Method.POST);
                var body = new { cita = cita, calendario = calendario };
                var json = JsonConvert.SerializeObject(body);

                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(body);
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

        public Calendario ObtenerCalendario(int calendarioId)
        {
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCitas"));

                var action = "ObtenerCalendario";
                var request = new RestRequest(resource: action, method: Method.GET);
                request.AddQueryParameter("calendarioId", calendarioId.ToString());
                //var json = JsonConvert.SerializeObject(body);

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
                 
        public Cita ObtenerCita(int id)
        {

            // db.Citas.Find(id);
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCitas"));

                var action = "ObtenerCita";
                var request = new RestRequest(resource: action, method: Method.GET);
                request.AddQueryParameter("id", id.ToString());
                //var json = JsonConvert.SerializeObject(body);

                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos[$"{action}Result"].ToObject<ResultadoServicio<Cita>>();
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

        public EspecialidadMedica ObtenerEspecialidadMedicaDelDoctor(int medicoId)
        {
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCitas"));

                var action = "ObtenerEspecialidadMedicaDelDoctor";
                var request = new RestRequest(resource: action, method: Method.GET);
                request.AddQueryParameter("medicoId", medicoId.ToString());
                //var json = JsonConvert.SerializeObject(body);

                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos[$"{action}Result"].ToObject<ResultadoServicio<EspecialidadMedica>>();
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

        public List<Cita> ObtenerCitasDoctor(string userId)
        {
            //return db.Citas.Where(c => c.Calendario.Medico.ApplicationUser.Id == userId).ToList();
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCitas"));


                var action = "ObtenerCalendario";
                var request = new RestRequest(resource: action, method: Method.GET);
                request.AddQueryParameter("userId", userId);
                //var json = JsonConvert.SerializeObject(body);

                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos[$"{action}Result"].ToObject<ResultadoServicio<List<Cita>>>();
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

        public SelectList ObtenerEsMedicasPorMedicosEnCentroMedico(int cMedicoId)
        {
            //return new SelectList(db.Personas.OfType<Medico>().Where(m => m.CentroMedico.CentroMedicoId == cMedico.CentroMedicoId).Select(c => c.EspecialidadMedica).Distinct().ToList(), "EspecialidadMedicaId", "Nombre");
            //return new SelectList(db.CentrosMedicos.ToList(), "Rif", "Nombre");
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCitas"));


                var action = "ObtenerEsMedicasPorMedicosEnCentroMedico";
                var request = new RestRequest(resource: action, method: Method.GET);
                request.AddQueryParameter("cMedicoId", cMedicoId.ToString());
                //var json = JsonConvert.SerializeObject(body);

                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos[$"{action}Result"].ToObject<ResultadoServicio<List<EspecialidadMedica>>>();
                    if (resultado != null && resultado.SinProblemas)
                    {
                        SelectList selectList = new SelectList(resultado.Contenido, "EspecialidadMedicaId", "Nombre");
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

        public EspecialidadMedica ObtenerEspecialidadMedica(int espMedica)
        {
            //return db.EspecialidadesMedicas.Single(e => e.EspecialidadMedicaId == espMedica);
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCitas"));

                var action = "ObtenerEspecialidadMedica";
                var request = new RestRequest(resource: action, method: Method.GET);
                request.AddQueryParameter("espMedica", espMedica.ToString());
                //var json = JsonConvert.SerializeObject(body);

                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos[$"{action}Result"].ToObject<ResultadoServicio<EspecialidadMedica>>();
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

        public Medico ObtenerMedicoAsignadoACita(int citaId)
        {
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCitas"));

                var action = "ObtenerMedicoAsignadoACita";
                var request = new RestRequest(resource: action, method: Method.GET);
                request.AddQueryParameter("citaId", citaId.ToString());
                //var json = JsonConvert.SerializeObject(body);

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

        public List<Cita> ObtenerListaCitas(string userId)
        {
            //return db.Citas.Where(c => c.Paciente.ApplicationUser.Id == userId).ToList();
            //return db.Citas.Where(c => c.Calendario.Medico.ApplicationUser.Id == userId).ToList();
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCitas"));


                var action = "ObtenerListaCitas";
                var request = new RestRequest(resource: action, method: Method.GET);
                request.AddQueryParameter("userId", userId);
                //var json = JsonConvert.SerializeObject(body);

                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos[$"{action}Result"].ToObject<ResultadoServicio<List<Cita>>>();
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

        public Medico ObtenerMedico(string userId)
        {
            //return db.Personas.OfType<Medico>().Single(p => p.ApplicationUser.Id == userId);
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCitas"));

                var action = "ObtenerMedico";
                var request = new RestRequest(resource: action, method: Method.GET);
                request.AddQueryParameter("userId", userId);
                //var json = JsonConvert.SerializeObject(body);

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

        public SelectList ObtenerSelectListCentrosMedicos()
        {
            //return new SelectList(db.CentrosMedicos.ToList(), "Rif", "Nombre");
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCitas"));


                var action = "ObtenerSelectListCentrosMedicos";
                var request = new RestRequest(resource: action, method: Method.GET);
                //request.AddQueryParameter("userId", userId);
                //var json = JsonConvert.SerializeObject(body);

                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos[$"{action}Result"].ToObject<ResultadoServicio<List<CentroMedico>>>();
                    if (resultado != null && resultado.SinProblemas)
                    {
                        SelectList selectList = new SelectList(resultado.Contenido, "Rif", "Nombre");
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

        public SelectList ObtenerSelectListMedicosQueTrabajanEnCentroMedico(int centroMedicoId, int espMedica)
        {
            //return new SelectList(db.Personas.OfType<Medico>().Where(p => p.CentroMedico.CentroMedicoId == centroMedicoId && p.EspecialidadMedica.EspecialidadMedicaId == espMedica).ToList(), "PersonaId", "ConcatUserName");
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCitas"));


                var action = "ObtenerSelectListMedicosQueTrabajanEnCentroMedico";
                var request = new RestRequest(resource: action, method: Method.GET);
                request.AddQueryParameter("centroMedicoId", centroMedicoId.ToString());
                request.AddQueryParameter("espMedica", espMedica.ToString());
                //var json = JsonConvert.SerializeObject(body);

                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos[$"{action}Result"].ToObject<ResultadoServicio<List<Medico>>>();
                    if (resultado != null && resultado.SinProblemas)
                    {
                        SelectList selectList = new SelectList(resultado.Contenido, "PersonaId", "ConcatUserName");
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

        public string ObtenerUsuarioLoggedIn(CitasController citasController)
        {
            return citasController.User.Identity.GetUserId();
        }

        public CentroMedico ObtenerCentroMedico(int centroMedicoId)
        {
            //return db.CentrosMedicos.Single(m => m.CentroMedicoId == centroMedicoId);
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCitas"));

                var action = "ObtenerCentroMedico";
                var request = new RestRequest(resource: action, method: Method.GET);
                request.AddQueryParameter("centroMedicoId", centroMedicoId.ToString());
                //var json = JsonConvert.SerializeObject(body);

                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos[$"{action}Result"].ToObject<ResultadoServicio<CentroMedico>>();
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

        public List<Calendario> ObtenerListaDisponibilidad(string medicoId)
        {
            //Where(m => m.Medico.PersonaId == mdId && m.Disponible == 1).OrderBy(m => m.HoraInicio)
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCitas"));


                var action = "ObtenerListaDisponibilidad";
                var request = new RestRequest(resource: action, method: Method.GET);
                request.AddQueryParameter("medicoId", medicoId);
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

        public CentroMedico ObtenerCentroMedicoRif(string centroMedicoRif)
        {
            //return db.CentrosMedicos.Single(m => m.CentroMedicoId == centroMedicoId);
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("ServicioCitas"));

                var action = "ObtenerCentroMedicoRif";
                var request = new RestRequest(resource: action, method: Method.GET);
                request.AddQueryParameter("centroMedicoRif", centroMedicoRif);
                //var json = JsonConvert.SerializeObject(body);

                var response = client.Execute(request);

                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var datos = (JObject)JsonConvert.DeserializeObject(response.Content);
                    var resultado = datos[$"{action}Result"].ToObject<ResultadoServicio<CentroMedico>>();
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