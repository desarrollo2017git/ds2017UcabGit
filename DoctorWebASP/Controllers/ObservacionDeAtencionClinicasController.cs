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
    public class ObservacionDeAtencionClinicasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ObservacionDeAtencionClinicas
        public ActionResult Index()
        {
            return View(db.ObservacionDeAtencionClincas.ToList());
        }

        // GET: ObservacionDeAtencionClinicas/Details/5
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

        // GET: ObservacionDeAtencionClinicas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ObservacionDeAtencionClinicas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ObservacionDeAtencionMedicaId,Observacion,Comentario,tipo")] ObservacionDeAtencionClinica observacionDeAtencionClinica)
        {
            if (ModelState.IsValid)
            {
                db.ObservacionDeAtencionClincas.Add(observacionDeAtencionClinica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(observacionDeAtencionClinica);
        }

        // GET: ObservacionDeAtencionClinicas/Edit/5
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

        // POST: ObservacionDeAtencionClinicas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ObservacionDeAtencionMedicaId,Observacion,Comentario,tipo")] ObservacionDeAtencionClinica observacionDeAtencionClinica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(observacionDeAtencionClinica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(observacionDeAtencionClinica);
        }

        // GET: ObservacionDeAtencionClinicas/Delete/5
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

        // POST: ObservacionDeAtencionClinicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ObservacionDeAtencionClinica observacionDeAtencionClinica = db.ObservacionDeAtencionClincas.Find(id);
            db.ObservacionDeAtencionClincas.Remove(observacionDeAtencionClinica);
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
