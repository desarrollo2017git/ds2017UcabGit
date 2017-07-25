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
    public class ObservacionMedicaE2Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public IServicioObservacionMedicaE2 consulta { get; set; }
        public ObservacionMedicaE2Controller(): this(new ServicioObservacionMedicaE2()) {
        }

        public ObservacionMedicaE2Controller(IServicioObservacionMedicaE2 db)
        {
            this.consulta = db;
        }

        // GET: ObservacionMedicaE2
        /// <summary>
        /// Metodo que llama a la interfaz de consulta de observaciones medica
        /// </summary>
        /// <returns> Interfaz de consulta de observaciones medicas </returns>
        [Authorize]
        public ActionResult Index()
        {
            if (consulta != null)
            {


                return View(consulta.ObtenerSelectListObservacionMedicaE2());
            }
            return View(db.ObservacionMedicaE2.ToList());


        }

        // GET: ObservacionMedicaE2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObservacionMedicaE2 observacionMedicaE2 = db.ObservacionMedicaE2.Find(id);
            if (observacionMedicaE2 == null)
            {
                return HttpNotFound();
            }
            return View(observacionMedicaE2);
        }

        // GET: ObservacionMedicaE2/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ObservacionMedicaE2/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ObservacionMedicaId,Diagnostico,Indicacion,Paciente")] ObservacionMedicaE2 observacionMedicaE2)
        {
            if (ModelState.IsValid)
            {
                db.ObservacionMedicaE2.Add(observacionMedicaE2);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(observacionMedicaE2);
        }

        // GET: ObservacionMedicaE2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObservacionMedicaE2 observacionMedicaE2 = db.ObservacionMedicaE2.Find(id);
            if (observacionMedicaE2 == null)
            {
                return HttpNotFound();
            }
            return View(observacionMedicaE2);
        }

        // POST: ObservacionMedicaE2/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ObservacionMedicaId,Diagnostico,Indicacion,Paciente")] ObservacionMedicaE2 observacionMedicaE2)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(observacionMedicaE2).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(observacionMedicaE2);
        }

        // GET: ObservacionMedicaE2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ObservacionMedicaE2 observacionMedicaE2 = db.ObservacionMedicaE2.Find(id);
            if (observacionMedicaE2 == null)
            {
                return HttpNotFound();
            }
            return View(observacionMedicaE2);
        }

        // POST: ObservacionMedicaE2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ObservacionMedicaE2 observacionMedicaE2 = db.ObservacionMedicaE2.Find(id);
            //db.ObservacionMedicas.Remove(observacionMedica);
            consulta.EliminarObservacionMedicaE2(observacionMedicaE2);
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
