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
    public class ObservacionMedicasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public IServicioObservacionMedica consulta { get; set; }
        public ObservacionMedicasController() : this(new ServicioObservacionMedica())
        {
        }

        public ObservacionMedicasController(IServicioObservacionMedica db)
        {
            this.consulta = db;
        }

        // GET: ObservacionMedica
        /// <summary>
        /// Metodo que llama a la interfaz de consulta de Observaciones. Muestra lista almacenada
        /// </summary>
        /// <returns> Interfaz para la consulta de Observaciones </returns>
        [Authorize]
        public ActionResult Index()
        {
            if (consulta != null)
            {


                return View(consulta.ObtenerSelectListObservacionMedica());
            }
            return View(db.ObservacionMedicas.ToList());


        }

        // GET: ObservacionMedicas/Details/id
        /// <summary>
        /// Metodo para consulta del detalle de una Observacion Medica
        /// </summary>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObservacionMedica observacionMedica = db.ObservacionMedicas.Find(id);
            if (observacionMedica == null)
            {
                return HttpNotFound();
            }
            return View(observacionMedica);
        }

        // GET: ObservacionMedicas/Create
        /// <summary>
        /// Metodo para la agregacion de una Observacion Medica con Base de Datos local
        /// </summary>
        public ActionResult Create()
        {
            return View();
        }

        // POST: ObservacionMedicas/Create
        /// <summary>
        /// Metodo para la agregacion de una Observacion Medica en Base de Datos Remota via Web Service
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ObservacionMedicaId,Diagnostico,Indicacion")] ObservacionMedica observacionMedica)
        {
            if (ModelState.IsValid)
            {
                consulta.GuardarObservacionMedica(observacionMedica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(observacionMedica);
        }
  
        /// <summary>
        /// Metodo para editar de una Observacion Medica con Base de Datos local
        /// </summary>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObservacionMedica observacionMedica = db.ObservacionMedicas.Find(id);
            if (observacionMedica == null)
            {
                return HttpNotFound();
            }
            return View(observacionMedica);
        }

        // GET: ObservacionMedicas/Edit/id    
        /// <summary>
        /// Metodo para editar de una Observacion Medica con Base de Datos remota via WebService
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ObservacionMedicaId,Diagnostico,Indicacion")] ObservacionMedica observacionMedica)
        {
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(observacionMedica);
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
            ObservacionMedica observacionMedica = db.ObservacionMedicas.Find(id);
            if (observacionMedica == null)
            {
                return HttpNotFound();
            }
            return View(observacionMedica);
        }

        // POST: ObservacionMedicas/Delete/id
        /// <summary>
        /// Metodo para eliminar de una Observacion Medica con Base de Datos remota via WebService
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ObservacionMedica observacionMedica = db.ObservacionMedicas.Find(id);
            //db.ObservacionMedicas.Remove(observacionMedica);
            consulta.EliminarObservacionMedica(observacionMedica);
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
