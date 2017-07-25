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
    public class ObservacionClinicaE2Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public IServicioObservacionClinicaE2 consulta { get; set; }
        public ObservacionClinicaE2Controller(): this(new ServicioObservacionClinicaE2()) {
        }

        public ObservacionClinicaE2Controller(IServicioObservacionClinicaE2 db)
        {
            this.consulta = db;
        }

        // GET: ObservacionClinicaE2
        /// <summary>
        /// Metodo que llama a la interfaz de consulta de observaciones clinicas
        /// </summary>
        /// <returns> Interfaz de consulta de observaciones clinicas </returns>
        [Authorize]
        public ActionResult Index()
        {
            if (consulta != null)
            {


                return View(consulta.ObtenerSelectListObservacionClinicaE2());
            }
            return View(db.ObservacionClinicaE2.ToList());


        }

        // GET: ObservacionClinicaE2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObservacionClinicaE2 observacionClinicaE2 = db.ObservacionClinicaE2.Find(id);
            if (observacionClinicaE2 == null)
            {
                return HttpNotFound();
            }
            return View(observacionClinicaE2);
        }

        // GET: ObservacionClinicaE2/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ObservacionClinicaE2/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ObservacionDeAtencionMedicaId,Observacion,Comentario,Tipo,Paciente")] ObservacionClinicaE2 observacionClinicaE2)
        {
            if (ModelState.IsValid)
            {
                db.ObservacionClinicaE2.Add(observacionClinicaE2);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(observacionClinicaE2);
        }

        // GET: ObservacionClinicaE2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObservacionClinicaE2 observacionClinicaE2 = db.ObservacionClinicaE2.Find(id);
            if (observacionClinicaE2 == null)
            {
                return HttpNotFound();
            }
            return View(observacionClinicaE2);
        }

        // POST: ObservacionClinicaE2/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ObservacionDeAtencionMedicaId,Observacion,Comentario,Tipo,Paciente")] ObservacionClinicaE2 observacionClinicaE2)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(observacionClinicaE2).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(observacionClinicaE2);
        }

        // GET: ObservacionClinicaE2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObservacionClinicaE2 observacionClinicaE2 = db.ObservacionClinicaE2.Find(id);
            if (observacionClinicaE2 == null)
            {
                return HttpNotFound();
            }
            return View(observacionClinicaE2);
        }

        // POST: ObservacionClinicaE2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ObservacionClinicaE2 observacionClinicaE2 = db.ObservacionClinicaE2.Find(id);
            //db.ObservacionMedicas.Remove(observacionMedica);
            consulta.EliminarObservacionClinicaE2(observacionClinicaE2);
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
