using DoctorWebASP.Controllers.Helpers;
using DoctorWebASP.Models.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DoctorWebASP.Controllers;
using DoctorWebServiciosWCF.Helpers;

namespace DoctorWebASP.Models.Services
{
    public class ServicioCitas : IServicioCitas
    {

        public void EliminarCita(Cita cita, Calendario calendario)
        {
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("CitaService"));

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
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("CitaService"));

                var action = "GuardarCita";
                var request = new RestRequest(resource: action, method: Method.POST);
                var body = new { cita = cita, calendario = calendario };
                //var json = JsonConvert.SerializeObject(body);

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

        public Calendario ObtenerCalendario(int calendarioId)
        {
            try
            {
                var client = new RestClient(baseUrl: Utilidades.ObtenerUrlServicioWeb("CitaService"));

                var action = "ObtenerCalendario";
                var request = new RestRequest(resource: action, method: Method.POST);
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
            throw new NotImplementedException();
        }

        public List<Cita> ObtenerCitasDoctor(string userId)
        {
            //return db.Citas.Where(c => c.Calendario.Medico.ApplicationUser.Id == userId).ToList();
            throw new NotImplementedException();
        }

        public SelectList ObtenerEsMedicasPorMedicosEnCentroMedico(CentroMedico cMedico)
        {
            //return new SelectList(db.Personas.OfType<Medico>().Where(m => m.CentroMedico.CentroMedicoId == cMedico.CentroMedicoId).Select(c => c.EspecialidadMedica).Distinct().ToList(), "EspecialidadMedicaId", "Nombre");
            throw new NotImplementedException();
        }

        public EspecialidadMedica ObtenerEspecialidadMedica(int espMedica)
        {
            throw new NotImplementedException();
            //return db.EspecialidadesMedicas.Single(e => e.EspecialidadMedicaId == espMedica);
        }

        public List<Cita> ObtenerListaCitas(string userId)
        {
            throw new NotImplementedException();
            //return db.Citas.Where(c => c.Paciente.ApplicationUser.Id == userId).ToList();
        }

        public Medico ObtenerMedico(string userId)
        {
            throw new NotImplementedException();
            //return db.Personas.OfType<Medico>().Single(p => p.ApplicationUser.Id == userId);
        }

        public Paciente ObtenerPaciente(string userId)
        {
            throw new NotImplementedException();
            //return db.Personas.OfType<Paciente>().Single(p => p.ApplicationUser.Id == userId);
        }

        public SelectList ObtenerSelectListCentrosMedicos()
        {
            throw new NotImplementedException();
            //return new SelectList(db.CentrosMedicos.ToList(), "Rif", "Nombre");
        }

        public SelectList ObtenerSelectListMedicosQueTrabajanEnCentroMedico(int centroMedicoId, int espMedica)
        {
            throw new NotImplementedException();
            //return new SelectList(db.Personas.OfType<Medico>().Where(p => p.CentroMedico.CentroMedicoId == centroMedicoId && p.EspecialidadMedica.EspecialidadMedicaId == espMedica).ToList(), "PersonaId", "ConcatUserName");
        }

        public CentroMedico ObtenerSingleCentroMedico(string centroMedico)
        {
            throw new NotImplementedException();
            //return db.CentrosMedicos.Single(m => m.Rif == centroMedico);
        }

        public CentroMedico ObtenerSingleCentroMedico(int centroMedicoId)
        {
            throw new NotImplementedException();
            //return db.CentrosMedicos.Single(c => c.CentroMedicoId == centroMedicoId);
        }

        public string ObtenerUsuarioLoggedIn(CitasController citasController)
        {
            throw new NotImplementedException();
            //return citasController.User.Identity.GetUserId();
        }

        public CentroMedico ObtenerCentroMedico(int centroMedicoId)
        {
            throw new NotImplementedException();
            //return db.CentrosMedicos.Single(m => m.CentroMedicoId == centroMedicoId);
        }

    }
}