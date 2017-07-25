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
    public class ResultadoE2Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ResultadoE2
        public ActionResult Index()
        {
            return View(db.ResultadoE2.ToList());
        }

        // GET: ResultadoE2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultadoE2 resultadoE2 = db.ResultadoE2.Find(id);
            if (resultadoE2 == null)
            {
                return HttpNotFound();
            }
            return View(resultadoE2);
        }

        // GET: ResultadoE2/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResultadoE2/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResultadoExamenMedicoID,Comentario,Paciente")] ResultadoE2 resultadoE2)
        {
            if (ModelState.IsValid)
            {
                db.ResultadoE2.Add(resultadoE2);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resultadoE2);
        }

        // GET: ResultadoE2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultadoE2 resultadoE2 = db.ResultadoE2.Find(id);
            if (resultadoE2 == null)
            {
                return HttpNotFound();
            }
            return View(resultadoE2);
        }

        // POST: ResultadoE2/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResultadoExamenMedicoID,Comentario,Paciente")] ResultadoE2 resultadoE2)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resultadoE2).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resultadoE2);
        }

        // GET: ResultadoE2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultadoE2 resultadoE2 = db.ResultadoE2.Find(id);
            if (resultadoE2 == null)
            {
                return HttpNotFound();
            }
            return View(resultadoE2);
        }

        // POST: ResultadoE2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ResultadoE2 resultadoE2 = db.ResultadoE2.Find(id);
            db.ResultadoE2.Remove(resultadoE2);
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
