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
    public class ResultadoE2Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public IServicioResultadoE2 consulta { get; set; }
        public ResultadoE2Controller(): this(new ServicioResultadoE2()) {
        }

        public ResultadoE2Controller(IServicioResultadoE2 db)
        {
            this.consulta = db;
        }

        // GET: ResultadoE2
        /// <summary>
        /// Metodo que llama a la interfaz de consulta de resultados
        /// </summary>
        /// <returns> Interfaz de consulta de resultados de examenes </returns>
        [Authorize]
        public ActionResult Index()
        {
            if (consulta != null)
            {


                return View(consulta.ObtenerSelectListResultadoE2());
            }
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
                //db.Entry(resultadoE2).State = EntityState.Modified;
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
            //db.ObservacionMedicas.Remove(observacionMedica);
            consulta.EliminarResultadoE2(resultadoE2);
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
