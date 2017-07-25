using System;
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
        /// <summary>
        /// Metodo que llama a la interfaz de calendario principal, 
        /// </summary>
        /// <returns> Interfaz calendario</returns>
        public ActionResult Index()
        {
            return View(db.Calendarios.ToList());
        }

        /// <summary>
        /// Devuelve la vista de error cuando no se sabe su causa
        /// </summary>
        /// <returns> vista de error general</returns>
        [HttpPost]
        public ActionResult ErrorCalendario()
        {
            return View();
        }
        /// <summary>
        /// Devuelve la vista de error cuando se sabe su causa
        /// </summary>
        /// <param name="mensaje"> mensaje a mostrar en la vista</param>
        /// <returns>vista de error personalizada</returns>
        public ActionResult ErrorCalendario(string mensaje)
        {
            var model = new SadFaceViewModel
            {
                Mensaje = mensaje
            };
            return View("ErrorCalendario", model);
        }

        // POST: Calendarios/Create
        /// <summary>
        ///Metodo que crea un nuevo tiempo libre para que el doctor pueda usarlo en citas recibe un objeto calendario
        /// </summary>
        /// <param name="calendario"> aclendario para ser creado</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CalendarioId,HoraInicio,HoraFin,Cancelada,Disponible")] Calendario calendario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string userID = consulta.ObtenerUsuarioLoggedIn(this);  // id del usuario
                    calendario.Medico = consulta.ObtenerMedico(userID).Single(); // set de medico en el calendario
                    if ((ModelState.IsValid))
                    {
                        try
                        {
                            Calendario pepe = consulta.GuardarCalendario(calendario); // intenta incertar si no cumple las condiciones devuelve null
                            if (pepe == null)
                            {
                                string mensaje = "El bloque de horas solicitado no está disponible";
                                return RedirectToAction("ErrorCalendario", "Calendarios", new { mensaje });
                            }
                            else
                            {
                                //" para el día " + calendario.HoraInicio.ToString("dd/MM/yy") + " desde las " + calendario.HoraInicio.ToString("HH:mm") + " hasta las " + calendario.HoraFin.ToString("HH:mm");
                                string mensaje = "Se ha creado un tiempo de citas para el: " + calendario.HoraInicio.ToString("dd/MM/yy") + " desde las " + calendario.HoraInicio.ToString("HH:mm") + " hasta las " + calendario.HoraInicio.AddHours(2).ToString("HH:mm");
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
                    string mensaje = "Su tipo de usuario no esta autorizado para realizar esta operación";
                    return RedirectToAction("ErrorCalendario", "Calendarios", new { mensaje });
                }
            }

            return View(calendario);
        }

        /// <summary>
        /// Metodo que enlista los tiempos libres de un doctor en la vista de eliminar
        /// </summary>
        /// <returns> vista de eliminar con la lista cargada</returns>
        public ActionResult listaCalendario()
        {
            try
            {
                string userID = consulta.ObtenerUsuarioLoggedIn(this); // id del usuario del sistema 
                var medicos = consulta.ObtenerMedico(userID); // medico que esta logeado
                return View(consulta.ObtenerTiempoDoctor(medicos.First().PersonaId));
            }
            catch  (Exception e)
            {
                Console.WriteLine(e);
                string mensaje = "No tiene permiso de ver esta página";
                return RedirectToAction("ErrorCalendario", "Calendarios", new { mensaje });
            }
        }

        /// <summary>
        /// Metodo para eliminar tiempos libres de doctor recibe el objeto calendario a eliminar
        /// </summary>
        /// <param name="calendario">calendario para ser eliminado</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar([Bind(Include = "CalendarioId,HoraInicio,HoraFin,Cancelada,Disponible")] Calendario calendario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string userID = consulta.ObtenerUsuarioLoggedIn(this); // usuario logeado
                    if (userID == null)
                    {
                        string mensaje = "No tiene autorizacion para eliminar tiempos";
                        return RedirectToAction("ErrorCalendario", "Calendarios", new { mensaje });
                    }
                    var medicos = consulta.ObtenerMedico(userID); // valida que sea medico
                    if (medicos.Count() > 0)
                    {
                        try
                        {
                            Calendario cal2 = new Calendario();
                            var cal = consulta.ObtenerTiempoDoctor(medicos.First().PersonaId); // busca los tiempos del medico
                            
                            if (cal.Count() > 0)
                            {

                                cal2 = cal.Find(x => x.CalendarioId == calendario.CalendarioId); // si el medico tiene ese tiempo asignado se le da a cal2
                                if (cal2.CalendarioId == calendario.CalendarioId && cal2.Disponible == 1)
                                {
                                    consulta.EliminarCalendario(cal2); //elimina el tiempo cal2
                                    string mensaje = "Se ha eliminando un tiempo de citas para el: " + cal2.HoraInicio.ToString("dd/MM/yy") + " desde las " + cal2.HoraInicio.ToString("HH:mm") + " hasta las " + cal2.HoraFin.ToString("HH:mm");
                                    return RedirectToAction("ErrorCalendario", "Calendarios", new { mensaje });
                                }
                                else
                                {
                                    string mensaje = "Id no valido";
                                    return RedirectToAction("ErrorCalendario", "Calendarios", new { mensaje });
                                }
                            }
                            else
                            {
                                string mensaje = "No hay nada que eliminar";
                                return RedirectToAction("ErrorCalendario", "Calendarios", new { mensaje });
                            }

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            string mensaje = "Id no valido";
                            return RedirectToAction("ErrorCalendario", "Calendarios", new { mensaje });
                        }
                    }
                    else
                    {
                        string mensaje = "No tiene autorizacion para eliminar ese tiempo";
                        return RedirectToAction("ErrorCalendario", "Calendarios", new { mensaje });
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    string mensaje = "Error inesperado";
                    return RedirectToAction("ErrorCalendario", "Calendarios", new { mensaje });
                }
            }
            return View(calendario);
        }

        // POST: Calendarios/Json
        /// <summary>
        /// Este metodo construye el Json que sera consumido por el calendario en la vista
        /// </summary>
        /// <returns> Json que es consumido</returns>
        [HttpPost]
        public ActionResult Json()
        {
            try
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                int medicoid;
                int pacienteid;
                string jsondata;
                string path;
                string userId = consulta.ObtenerUsuarioLoggedIn(this);
                var medico = consulta.ObtenerMedico(userId); // obtinene medico logeado o nulo si es paciente
                var paciente = consulta.ObtenerPaciente(userId); // obtinene paciente logeado o nulo si es medico
                if (medico.Count > 0) // si es medico
                {
                    medicoid = medico.First().PersonaId;
                    var callist = consulta.ObtenerTiempoDoctor(medicoid); // obtiene los tiempos para citas del doctor y abajo lo formatea para el consumo
                    var calArray = from cal in callist select new { id = cal.CalendarioId, title = cal.CalendarioId + ". Tiempo agendado para cita ", start = cal.HoraInicio.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"), end = cal.HoraFin.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"), cal.Disponible, cal.Cancelada, backgroundColor = "#00a65a" };
                    var citlist = consulta.ObtenerCitasDoctor(medicoid); // obtiene los tiempos de citas del doctor y abajo lo formatea para el consumo
                    var citArray = from cit in citlist select new { id = cit.CalendarioId, title = "Cita Médica con: " + consulta.ObtenerPacienteCalendario(cit.CalendarioId).NombreCompleto, start = cit.HoraInicio.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"), end = cit.HoraFin.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"), cit.Disponible, cit.Cancelada, backgroundColor = "#f56954" };
                    var cita_tiempo = citArray.Union(calArray); // une las dos listas de tiepos y citas
                    serializer.MaxJsonLength = Int32.MaxValue;
                    jsondata = serializer.Serialize(cita_tiempo);
                    path = Server.MapPath("~/Content/");
                    System.IO.File.WriteAllText(path + "calendario.json", jsondata); // contruye el json
                    TempData["msg"] = "Json file Generated! check this in your App_Data folder";
                    return RedirectToAction("Index");
                }

                else if (paciente.Count > 0) // si es paciente
                {
                    pacienteid = paciente.First().PersonaId; // obtiene el id del paciente 
                    var citlist = consulta.ObtenerCitasPaciente(pacienteid);  // obtiene los tiempos de citas del paciente y abajo lo formatea para el consumo
                    var citArray = from cit in citlist select new { id = cit.CalendarioId, title = "Cita Médica con: " + consulta.ObtenerMedicoCalendario(cit.CalendarioId).NombreCompleto, start = cit.HoraInicio.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"), end = cit.HoraFin.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"), cit.Disponible, cit.Cancelada, backgroundColor = "#f56954" };
                    serializer.MaxJsonLength = Int32.MaxValue;
                    jsondata = serializer.Serialize(citArray);
                    path = Server.MapPath("~/Content/");
                    System.IO.File.WriteAllText(path + "calendario.json", jsondata);  // contruye el json
                    TempData["msg"] = "Json file Generated! check this in your App_Data folder";
                    return RedirectToAction("Index");
                }
                else
                {
                    string mensaje = "Error inesperado";
                    return RedirectToAction("ErrorCalendario", "Calendarios", new { mensaje });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                string mensaje = "Error inesperado";
                return RedirectToAction("ErrorCalendario", "Calendarios", new { mensaje });
            }
        }


 //////////////////////////////CODIGO AUTO-GENERADO///////////////////////////////////////////////
   
        // POST: Calendarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)  // no es usado
        {
            Calendario calendario = db.Calendarios.Find(id);
            db.Calendarios.Remove(calendario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)  // no es usado
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        // GET: Calendarios/Delete/5
        public ActionResult Eliminar() // no es usado
        {
            return View();
        }
        public ActionResult Delete(int? id) // no es usado
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

        // GET: Calendarios/Details/5
        public ActionResult Details(int? id)  // no es usado
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
        public ActionResult Edit([Bind(Include = "CalendarioId,HoraInicio,HoraFin,Cancelada,Disponible")] Calendario calendario)  // no es usado
        {
            if (ModelState.IsValid)
            {
                db.Entry(calendario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(calendario);
        }

        // GET: Calendarios/Edit/5
        public ActionResult Edit(int? id)  // no es usado
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

        // GET: Calendarios/Create
        public ActionResult Create()  // no es usado
        {
            return View();
        }


    }
}
