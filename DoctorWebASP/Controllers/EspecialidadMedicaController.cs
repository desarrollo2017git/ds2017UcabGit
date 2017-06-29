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
    public class EspecialidadMedicaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EspecialidadMedica
        public ActionResult Index()
        {
            return View(db.EspecialidadesMedicas.ToList());
        }

        // GET: EspecialidadMedica/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EspecialidadMedica especialidadMedica = db.EspecialidadesMedicas.Find(id);
            if (especialidadMedica == null)
            {
                return HttpNotFound();
            }
            return View(especialidadMedica);
        }

        // GET: EspecialidadMedica/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EspecialidadMedica/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EspecialidadMedicaId,Nombre")] EspecialidadMedica especialidadMedica)
        {
            if (ModelState.IsValid)
            {
                db.EspecialidadesMedicas.Add(especialidadMedica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(especialidadMedica);
        }

        // GET: EspecialidadMedica/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EspecialidadMedica especialidadMedica = db.EspecialidadesMedicas.Find(id);
            if (especialidadMedica == null)
            {
                return HttpNotFound();
            }
            return View(especialidadMedica);
        }

        // POST: EspecialidadMedica/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EspecialidadMedicaId,Nombre")] EspecialidadMedica especialidadMedica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(especialidadMedica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(especialidadMedica);
        }

        // GET: EspecialidadMedica/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EspecialidadMedica especialidadMedica = db.EspecialidadesMedicas.Find(id);
            if (especialidadMedica == null)
            {
                return HttpNotFound();
            }
            return View(especialidadMedica);
        }

        // POST: EspecialidadMedica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EspecialidadMedica especialidadMedica = db.EspecialidadesMedicas.Find(id);
            db.EspecialidadesMedicas.Remove(especialidadMedica);
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
