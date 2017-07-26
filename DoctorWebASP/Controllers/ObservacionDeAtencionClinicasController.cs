using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DoctorWebASP.Models;
using DoctorWebASP.ViewModels;
using PagedList;
using Microsoft.AspNet.Identity;
using DoctorWebASP.Models.Services;

namespace DoctorWebASP.Controllers
{
    /// <summary>
    /// Clase controladora de Clase ObservacionMedica
    /// </summary>
    public class ObservacionDeAtencionClinicasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public IServicioObservacionDeAtencionClinica consulta { get; set; }
        public ObservacionDeAtencionClinicasController() : this(new ServicioObservacionDeAtencionClinica())
        {
        }

        public ObservacionDeAtencionClinicasController(IServicioObservacionDeAtencionClinica db)
        {
            this.consulta = db;
        }

        // GET: ObservacionDeAtencionClinica
        /// <summary>
        /// Metodo que llama a la interfaz de consulta de Observaciones de Atencion Clinica. Muestra lista almacenada
        /// </summary>
        /// <returns> Interfaz para la consulta de Observaciones Clinicas </returns>
        [Authorize]
        public ActionResult Index()
        {
            if (consulta != null)
            {


                return View(consulta.ObtenerSelectListObservacionDeAtencionClinica());
            }
            return View(db.ObservacionDeAtencionClincas.ToList());


        }

      
        /// <summary>
        /// Metodo para consulta del detalle de una Observacion clinica
        /// </summary>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObservacionDeAtencionClinica observacionDeAtencionClinica = db.ObservacionDeAtencionClincas.Find(id);
            if (observacionDeAtencionClinica == null)
            {
                return HttpNotFound();
            }
            return View(observacionDeAtencionClinica);
        }

       
        /// <summary>
        /// Metodo para la agregacion de una Observacion Medica con Base de Datos local
        /// </summary>
        public ActionResult Create()
        {
            return View();
        }

        // POST: ObservacionDeAtencionClinicas/Create
        /// <summary>
        /// Metodo para la agregacion de una Observacion Clinica en Base de Datos Remota via Web Service
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ObservacionDeAtencionMedicaId,Observacion,Comentario,tipo")] ObservacionDeAtencionClinica observacionDeAtencionClinica)
        {
            if (ModelState.IsValid)
            {
                // db.ObservacionMedicas.Add(observacionMedica);
                consulta.GuardarObservacionDeAtencionClinica(observacionDeAtencionClinica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(observacionDeAtencionClinica);
        }
        /// <summary>
        /// Metodo para editar de una Observacion Clinica con Base de Datos local
        /// </summary>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObservacionDeAtencionClinica observacionDeAtencionClinica = db.ObservacionDeAtencionClincas.Find(id);
            if (observacionDeAtencionClinica == null)
            {
                return HttpNotFound();
            }
            return View(observacionDeAtencionClinica);
        }

        // POST: ObservacionDeAtencionClinicas/Edit/id
        /// <summary>
        /// Metodo para editar de una Observacion Clinica con Base de Datos remota via WebService
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ObservacionDeAtencionMedicaId,Observacion,Comentario,tipo")] ObservacionDeAtencionClinica observacionDeAtencionClinica)
        {
            if (ModelState.IsValid)
            {
               // db.Entry(observacionDeAtencionClinica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(observacionDeAtencionClinica);
        }
        /// <summary>
        /// Metodo para eliminar de una Observacion Medica con Base de Datos local
        /// </summary>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObservacionDeAtencionClinica observacionDeAtencionClinica = db.ObservacionDeAtencionClincas.Find(id);
            if (observacionDeAtencionClinica == null)
            {
                return HttpNotFound();
            }
            return View(observacionDeAtencionClinica);
        }

        // POST: ObservacionDeAtencionClinicas/Delete/id
        /// <summary>
        /// Metodo para eliminar de una Observacion Clinica con Base de Datos remota via WebService
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ObservacionDeAtencionClinica observacionDeAtencionClinica = db.ObservacionDeAtencionClincas.Find(id);
       
            consulta.EliminarObservacionDeAtencionClinica(observacionDeAtencionClinica);
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
    }
}
