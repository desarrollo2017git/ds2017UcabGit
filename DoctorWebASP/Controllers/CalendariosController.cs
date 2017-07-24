﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoctorWebASP.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using DoctorWebASP.ViewModels;
using DoctorWebASP.Models.Services;

namespace DoctorWebASP.Controllers
{
    public class CalendariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public IServicioCalendarios consulta { get; set; }
        public CalendariosController(): this(new ServicioCalendarios()) {
        }
        public CalendariosController(IServicioCalendarios db)
        {
            this.consulta = db;
        }



        // GET: Calendarios
        public ActionResult Index()
        {
            return View(db.Calendarios.ToList());
        }

        // GET: Calendarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calendario calendario = db.Calendarios.Find(id);
            if (calendario == null)
            {
                return HttpNotFound();
            }
            return View(calendario);
        }

        [HttpPost]
        public ActionResult ErrorCalendario()
        {
            return View();
        }

        public ActionResult ErrorCalendario(string mensaje)
        {
            var model = new SadFaceViewModel
            {
                Mensaje = mensaje
            };
            return View("ErrorCalendario", model);
        }

        // GET: Calendarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Calendarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CalendarioId,HoraInicio,HoraFin,Cancelada,Disponible")] Calendario calendario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var calendarios = new SelectList(""); var calendarios2 = new SelectList("");
                    string userID = consulta.ObtenerUsuarioLoggedIn(this);
                    calendario.Medico = consulta.ObtenerMedico(userID).Single();
                    if ((ModelState.IsValid))
                    {
                        try
                        {
                            Calendario pepe = consulta.GuardarCalendario(calendario);
                            if (pepe == null)
                            {
                                string mensaje = "El bloque de horas solicitado no está disponible";
                                return RedirectToAction("ErrorCalendario", "Calendarios", new { mensaje });
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            string mensaje = "Hubo un problema creando su horario de cita";
                            return RedirectToAction("ErrorCalendario", "Calendarios", new { mensaje });
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return RedirectToAction("ErrorCalendario");
                }
            }

            return View(calendario);
        }



        // GET: Calendarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calendario calendario = db.Calendarios.Find(id);
            if (calendario == null)
            {
                return HttpNotFound();
            }
            return View(calendario);
        }

        // POST: Calendarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CalendarioId,HoraInicio,HoraFin,Cancelada,Disponible")] Calendario calendario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(calendario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(calendario);
        }

        // GET: Calendarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calendario calendario = db.Calendarios.Find(id);
            if (calendario == null)
            {
                return HttpNotFound();
            }
            return View(calendario);
        }

        public ActionResult Eliminar()
        {
            return View();
        }

        public ActionResult listaCalendario()
        {
            //string userID = User.Identity.GetUserId();
            // return View(db.Calendarios.Where(c => c.Medico.ApplicationUser.Id == userID && c.Disponible == 1).ToList());
            string userID = consulta.ObtenerUsuarioLoggedIn(this);
            var medicos = consulta.ObtenerMedico(userID);
            return View(consulta.ObtenerTiempoDoctor(medicos.First().PersonaId));


        }

        /// <summary>
        /// ///////////////////////////////////////
        /// </summary>
        /// <param name="calendario"></param>
        /// <returns></returns>

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar([Bind(Include = "CalendarioId,HoraInicio,HoraFin,Cancelada,Disponible")] Calendario calendario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string userID = consulta.ObtenerUsuarioLoggedIn(this);
                    var medicos = consulta.ObtenerMedico(userID);
                    if (medicos.Count() > 0)
                    {
                        try
                        {
                            Calendario cal2 = new Calendario();
                            var cal = consulta.ObtenerTiempoDoctor(medicos.First().PersonaId);
                            
                            if (cal.Count() > 0)
                            {

                                cal2 = cal.Find(x => x.CalendarioId == calendario.CalendarioId);
                                if (cal2.CalendarioId == calendario.CalendarioId && cal2.Disponible == 1)
                                {
                                    //calendario.Cancelada = false;
                                    //calendario.HoraFin = calendario.HoraInicio.AddHours(2);
                                    //calendario.Disponible = 1;
                                    consulta.EliminarCalendario(cal2);
                                    //db.SaveChanges();
                                    return RedirectToAction("Eliminar");
                                }
                                else
                                    return RedirectToAction("ErrorCalendario");
                            }
                            else
                            {
                                string mensaje = "ID incorrecto";
                                return RedirectToAction("ErrorCalendario", "Calendarios", new { mensaje });
                            }

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            return RedirectToAction("ErrorCalendario");
                        }
                    }
                    else
                    {
                        string mensaje = "Fecha invalida";
                        return RedirectToAction("ErrorCalendario", "Calendarios", new { mensaje });
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return RedirectToAction("ErrorCalendario");
                }
            }
            return View(calendario);
        }

        // POST: Calendarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Calendario calendario = db.Calendarios.Find(id);
            db.Calendarios.Remove(calendario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /*public JsonResult Json()
        {
            var calendarsDates = GetCalendario();
            string cal = JsonConvert.SerializeObject(calendarsDates.ToArray());
            return Json(calendarsDates, JsonRequestBehavior.AllowGet);
        }*/



        /*private SelectList GetCalendario()
        {
            var calendarList = new SelectList("");
            string userID = User.Identity.GetUserId();
            Medico login = new Medico();
            int medicoid;
            login = db.Personas.OfType<Medico>().Single(p => p.ApplicationUser.Id == userID);
            medicoid = login.PersonaId;
            calendarList = new SelectList(db.Calendarios.Where(c => c.Medico.PersonaId == medicoid));
            return calendarList;
        }*/
        [HttpPost]
        //[Authorize]
        public ActionResult Json(Calendario obj)
        {
            try
            {
                //Persona uno = new Persona();
                //var login = new Medico();
                //Paciente login2 = new Paciente();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                int medicoid;
                int pacienteid;
                string jsondata;
                string path;
                string userId = consulta.ObtenerUsuarioLoggedIn(this);
                var medico = consulta.ObtenerMedico(userId);
                var paciente = consulta.ObtenerPaciente(userId);
                //int a = 1;
                //var login = db.Personas.OfType<Medico>().Where(p => p.ApplicationUser.Id == userID);
                // var login2 = db.Personas.OfType<Paciente>().Where(p => p.ApplicationUser.Id == userID);
                if (medico.Count > 0)
                {

                    medicoid = medico.First().PersonaId;
                    var callist = consulta.ObtenerTiempoDoctor(medicoid);
                    var calArray = from cal in callist select new { id = cal.CalendarioId, title = cal.CalendarioId + ". Tiempo agendado para cita ", start = cal.HoraInicio.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"), end = cal.HoraFin.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"), cal.Disponible, cal.Cancelada, backgroundColor = "#00a65a" };


                    var citlist = consulta.ObtenerCitasDoctor(medicoid);
                    var citArray = from cit in citlist select new { id = cit.CalendarioId, title = "Cita Médica con: " + consulta.ObtenerPacienteCalendario(cit.CalendarioId).NombreCompleto, start = cit.HoraInicio.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"), end = cit.HoraFin.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"), cit.Disponible, cit.Cancelada, backgroundColor = "#f56954" };

                    //String intento = "";

                    foreach (var elemento in citArray)
                    {
                        Persona pana = consulta.ObtenerPacienteCalendario(elemento.id);
                        
                       
                    }


                    var cita_tiempo = citArray.Union(calArray);
                    

                    
                    serializer.MaxJsonLength = Int32.MaxValue;
                    jsondata = serializer.Serialize(cita_tiempo);
                    path = Server.MapPath("~/Content/");
                
                     // Write that JSON to txt file,  
                    System.IO.File.WriteAllText(path + "calendario.json", jsondata);
                    TempData["msg"] = "Json file Generated! check this in your App_Data folder";
                    return RedirectToAction("Index");

                }

                //Paceinteid = login2.PersonaId;
                else if (paciente.Count > 0)
                {


                    pacienteid = paciente.First().PersonaId;
                    var citlist = consulta.ObtenerCitasPaciente(pacienteid);
                    var citArray = from cit in citlist select new { id = cit.CalendarioId, title = "Cita Médica con: " + consulta.ObtenerMedicoCalendario(cit.CalendarioId).NombreCompleto, start = cit.HoraInicio.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"), end = cit.HoraFin.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"), cit.Disponible, cit.Cancelada, backgroundColor = "#f56954" };
                   // var citlist = db.Calendarios.Where(c => c.Cita.Paciente.PersonaId == pacienteid && c.Disponible == 0).ToList()
                    //    .Select(c => new { id = c.CalendarioId, title = "Cita Médica con " + c.Medico.NombreCompleto, start = c.HoraInicio.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"), end = c.HoraFin.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"), c.Disponible, c.Cancelada, backgroundColor = "#f56954" });
                    // Pass the "personlist" object for conversion object to JSON string
         

                    serializer.MaxJsonLength = Int32.MaxValue;
                    jsondata = serializer.Serialize(citArray);
                    path = Server.MapPath("~/Content/");

                    // Write that JSON to txt file,  
                    System.IO.File.WriteAllText(path + "calendario.json", jsondata);
                    TempData["msg"] = "Json file Generated! check this in your App_Data folder";
                    return RedirectToAction("Index");

                }
                else
                    return RedirectToAction("ErrorCalendario");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return RedirectToAction("ErrorCalendario"); // hola
            }
        }
    }
}
