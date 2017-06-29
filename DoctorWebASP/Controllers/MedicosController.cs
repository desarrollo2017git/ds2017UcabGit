using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoctorWebASP.Models;
using Microsoft.AspNet.Identity;
using DoctorWebASP.ViewModels;

namespace DoctorWebASP.Controllers
{
    public class MedicosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Medicos
        public ActionResult Index()
        {
            return View(db.Personas.OfType<Medico>().ToList());
        }

        // GET: Medicos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = db.Personas.OfType<Medico>().Where(m => m.PersonaId == id).Single();
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // GET: Medicos/Create
        public ActionResult Create()
        {
            var viewModel = new MedicoBackdoorViewModel
            {
                EspecialidadesMedicas = new SelectList(db.EspecialidadesMedicas.ToList(), "EspecialidadMedicaId", "Nombre"),
                CentrosMedicos = new SelectList(db.CentrosMedicos.ToList(), "CentroMedicoId", "Nombre")
            };
            return View(viewModel);
        }

        // POST: Medicos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonaId,Nombre,Apellido,Cedula,Genero,Telefono,FechaNacimiento,FechaCreacion,Email,Direccion,Sueldo")] Medico medico,
                                    [Bind(Prefix  = "CentroMedicoId")] int? cMedId,
                                    [Bind(Prefix = "EspecialidadMedicaId")] int? espMdId)
        {
            string userId = User.Identity.GetUserId();
            ApplicationUser user = db.Users.Single(u => u.Id == userId);
            EspecialidadMedica espMedica = db.EspecialidadesMedicas.Single(e => e.EspecialidadMedicaId == espMdId);
            CentroMedico centroMedico = db.CentrosMedicos.Single(c => c.CentroMedicoId == cMedId);

            if (ModelState.IsValid)
            {
                medico.ApplicationUser = user;
                medico.EspecialidadMedica = espMedica;
                medico.CentroMedico = centroMedico;
                db.Personas.Add(medico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(medico);
        }

        // GET: Medicos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = db.Personas.OfType<Medico>().Where(m => m.PersonaId == id).Single();
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // POST: Medicos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonaId,Nombre,Apellido,Cedula,Genero,Telefono,FechaNacimiento,FechaCreacion,Email,Direccion,Sueldo")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medico);
        }

        // GET: Medicos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medico medico = db.Personas.OfType<Medico>().Where(m => m.PersonaId == id).Single();
            if (medico == null)
            {
                return HttpNotFound();
            }
            return View(medico);
        }

        // POST: Medicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medico medico = db.Personas.OfType<Medico>().Where(m => m.PersonaId == id).Single();
            db.Personas.Remove(medico);
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
