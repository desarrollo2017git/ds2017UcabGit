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

        // GET: ResultadoExamenMedico
        /// <summary>
        /// Metodo que llama a la interfaz de consulta de Resultados. Muestra lista almacenada
        /// </summary>
        /// <returns> Interfaz para la consulta de resultados </returns>
        [Authorize]
            public ActionResult Index()
            {
                if (consulta != null)
                {


                    return View(consulta.ObtenerSelectListResultadoExamenMedico());
                }
                return View(db.ResultadoExamenMedicos.ToList());


            }

        /// <summary>
        /// Metodo para consulta del detalle de un resultado
        /// </summary>
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

        /// <summary>
        /// Metodo para la agregacion de un Resultado con Base de Datos local
        /// </summary>
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResultadoExamenMedico/Create
        /// <summary>
        /// Metodo para la agregacion de un resultado en Base de Datos Remota via Web Service
        /// </summary>
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

        /// <summary>
        /// Metodo para editar de un Resultado con Base de Datos local
        /// </summary>
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

        // POST: ResultadoExamenMedicoes/Edit/id
        /// <summary>
        /// Metodo para editar de un Resultado con Base de Datos remota via WebService
        /// </summary>
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
        /// <summary>
        /// Metodo para eliminar de un Resultado con Base de Datos local
        /// </summary>
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
        /// <summary>
        /// Metodo para eliminar de un Resultado con Base de Datos remota via WebService
        /// </summary>
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
