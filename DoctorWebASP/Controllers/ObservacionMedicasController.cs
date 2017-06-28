using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoctorWebASP.Models;

namespace DoctorWebASP.Controllers
{
    public class ObservacionMedicasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ObservacionMedicas
        public ActionResult Index()
        {
            return View(db.ObservacionMedicas.ToList());
        }

        // GET: ObservacionMedicas/Details/5
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: ObservacionMedicas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ObservacionMedicaId,Diagnostico,Indicacion")] ObservacionMedica observacionMedica)
        {
            if (ModelState.IsValid)
            {
                db.ObservacionMedicas.Add(observacionMedica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(observacionMedica);
        }

        // GET: ObservacionMedicas/Edit/5
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

        // POST: ObservacionMedicas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ObservacionMedicaId,Diagnostico,Indicacion")] ObservacionMedica observacionMedica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(observacionMedica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(observacionMedica);
        }

        // GET: ObservacionMedicas/Delete/5
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

        // POST: ObservacionMedicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ObservacionMedica observacionMedica = db.ObservacionMedicas.Find(id);
            db.ObservacionMedicas.Remove(observacionMedica);
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
