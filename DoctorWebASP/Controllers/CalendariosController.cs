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

namespace DoctorWebASP.Controllers
{
    public class CalendariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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

        public ActionResult ErrorCalendario()
        {
            return View();
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
                    string userID = User.Identity.GetUserId();
                    calendario.HoraFin = calendario.HoraInicio.AddHours(2);
                    calendario.Medico = db.Personas.OfType<Medico>().Single(p => p.ApplicationUser.Id == userID);
                    calendarios = new SelectList(db.Calendarios.Where(c => c.Medico.PersonaId == calendario.Medico.PersonaId && c.HoraInicio <= calendario.HoraInicio && c.HoraFin > calendario.HoraInicio));
                    calendarios2 = new SelectList(db.Calendarios.Where(c => c.Medico.PersonaId == calendario.Medico.PersonaId && c.HoraInicio < calendario.HoraFin && c.HoraFin >= calendario.HoraFin));

                    if (((calendarios.Count() == 0) && (calendarios2.Count() == 0)) && (calendario.HoraInicio >= System.DateTime.Now))
                    {
                        try
                        {
                            calendario.Cancelada = false;
                            //calendario.HoraFin = calendario.HoraInicio.AddHours(2);
                            calendario.Disponible = 1;
                            db.Calendarios.Add(calendario);
                            db.SaveChanges();
                            return RedirectToAction("Create");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            return RedirectToAction("ErrorCalendario");
                        }
                    }
                    else
                        return new HttpNotFoundResult("Fecha inválida!");
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
            return View(db.Calendarios.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar([Bind(Include = "CalendarioId,HoraInicio,HoraFin,Cancelada,Disponible")] Calendario calendario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var calendarios = new SelectList("");
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
        public ActionResult Json(Calendario obj)
        {
            try
            {
                string userID = User.Identity.GetUserId();
                //Persona uno = new Persona();
                //var login = new Medico();
                //Paciente login2 = new Paciente();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                int medicoid;
                int pacienteid;
                string jsondata;
                string path;
                var login = db.Personas.OfType<Medico>().Where(p => p.ApplicationUser.Id == userID);
                var login2 = db.Personas.OfType<Paciente>().Where(p => p.ApplicationUser.Id == userID);
                int a = 1;
                if (login.Count() > 0)
                {
                    medicoid = login.First().PersonaId;    
                    // retrive the data from table  
                    var callist = db.Calendarios.Where(c => c.Medico.PersonaId == medicoid && c.Disponible == 1).ToList()
                        .Select(c => new { id = c.CalendarioId, title = c.CalendarioId + ". Tiempo agendado para cita ", start = c.HoraInicio.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"), end = c.HoraFin.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"), c.Disponible, c.Cancelada, backgroundColor = "#00a65a" });
                    var citlist = db.Calendarios.Where(c => c.Medico.PersonaId == medicoid && c.Disponible == 0 && c.Cancelada == false).ToList()
                        .Select(c => new { id = c.CalendarioId, title = "Cita Médica con " + c.Cita.Paciente.NombreCompleto, start = c.HoraInicio.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"), end = c.HoraFin.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"), c.Disponible, c.Cancelada, backgroundColor = "#f56954" });
                    // Pass the "personlist" object for conversion object to JSON string
                    var eventlist = callist.Concat(citlist);
                    
                    serializer.MaxJsonLength = Int32.MaxValue;
                    jsondata = serializer.Serialize(eventlist);
                    path = Server.MapPath("~/Content/");
                
                     // Write that JSON to txt file,  
                    System.IO.File.WriteAllText(path + "calendario.json", jsondata);
                    TempData["msg"] = "Json file Generated! check this in your App_Data folder";
                    return RedirectToAction("Index");

                }

                //Paceinteid = login2.PersonaId;
                else if (login2.Count() > 0)
                {


                    pacienteid = login2.First().PersonaId;
                    // retrive the data from table  
                   var citlist = db.Calendarios.Where(c => c.Cita.Paciente.PersonaId == pacienteid && c.Disponible == 0).ToList()
                        .Select(c => new { id = c.CalendarioId, title = "Cita Médica con " + c.Medico.NombreCompleto, start = c.HoraInicio.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"), end = c.HoraFin.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"), c.Disponible, c.Cancelada, backgroundColor = "#f56954" });
                    // Pass the "personlist" object for conversion object to JSON string
         

                    serializer.MaxJsonLength = Int32.MaxValue;
                    jsondata = serializer.Serialize(citlist);
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
