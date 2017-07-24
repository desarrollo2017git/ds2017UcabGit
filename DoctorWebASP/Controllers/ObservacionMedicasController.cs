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
    public class ObservacionMedicasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public IServicioObservacionMedica consulta { get; set; }
        public ObservacionMedicasController(): this(new ServicioObservacionMedica()) {
        }

        public ObservacionMedicasController(IServicioObservacionMedica db)
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
            if (consulta != null){

                
                return View(consulta.ObtenerSelectListObservacionMedica());
            }
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
        public ActionResult Create([Bind(Include = "ObservacionMedicaId,Diagnostico,Indicacion,Paciente")] ObservacionMedica observacionMedica)
        {
            if (ModelState.IsValid)
            {
                // db.ObservacionMedicas.Add(observacionMedica);
                consulta.GuardarObservacionMedica(observacionMedica);
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
        public ActionResult Edit([Bind(Include = "ObservacionMedicaId,Diagnostico,Indicacion,Paciente")] ObservacionMedica observacionMedica)
        {
            if (ModelState.IsValid)
            {
              //  db.Entry(observacionMedica).State = EntityState.Modified;
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
