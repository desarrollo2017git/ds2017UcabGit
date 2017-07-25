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
    public class ResultadoExamenMedicoesController : Controller
    {

    
            private ApplicationDbContext db = new ApplicationDbContext();
            public IServicioResultadoExamenMedico consulta { get; set; }
            public ResultadoExamenMedicoesController() : this(new ServicioResultadoExamenMedico())
            {
            }

            public ResultadoExamenMedicoesController(IServicioResultadoExamenMedico db)
            {
                this.consulta = db;
            }

            // GET: Citas
            /// <summary>
            /// Metodo que llama a la interfaz de consulta de citas principal, 
            /// Si el usuario conectado actual es medico se llama a IndexDoctor
            /// </summary>
            /// <returns> Interfaz de consulta de citas agendadas de paciente </returns>
            [Authorize]
            public ActionResult Index()
            {
                if (consulta != null)
                {


                    return View(consulta.ObtenerSelectListResultadoExamenMedico());
                }
                return View(db.ResultadoExamenMedicos.ToList());


            }

            // GET: ResultadoExamenMedicoes/Details/5
            public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultadoExamenMedico resultadoExamenMedico = db.ResultadoExamenMedicos.Find(id);
            if (resultadoExamenMedico == null)
            {
                return HttpNotFound();
            }
            return View(resultadoExamenMedico);
        }

        // GET: ResultadoExamenMedicoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResultadoExamenMedicoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResultadoExamenMedicoID,Comentario")] ResultadoExamenMedico resultadoExamenMedico)
        {
            if (ModelState.IsValid)
            {
                // db.ObservacionMedicas.Add(observacionMedica);
                consulta.GuardarResultadoExamenMedico(resultadoExamenMedico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resultadoExamenMedico);
        }

        // GET: ResultadoExamenMedicoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultadoExamenMedico resultadoExamenMedico = db.ResultadoExamenMedicos.Find(id);
            if (resultadoExamenMedico == null)
            {
                return HttpNotFound();
            }
            return View(resultadoExamenMedico);
        }

        // POST: ResultadoExamenMedicoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResultadoExamenMedicoID,Comentario")] ResultadoExamenMedico resultadoExamenMedico)
        {
            if (ModelState.IsValid)
            {
               // db.Entry(resultadoExamenMedico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resultadoExamenMedico);
        }

        // GET: ResultadoExamenMedicoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultadoExamenMedico resultadoExamenMedico = db.ResultadoExamenMedicos.Find(id);
            if (resultadoExamenMedico == null)
            {
                return HttpNotFound();
            }
            return View(resultadoExamenMedico);
        }

        // POST: ResultadoExamenMedicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ResultadoExamenMedico resultadoExamenMedico = db.ResultadoExamenMedicos.Find(id);
            //db.ObservacionMedicas.Remove(observacionMedica);
            consulta.EliminarResultadoExamenMedico(resultadoExamenMedico);
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
