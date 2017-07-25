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
using DoctorWebASP.Controllers.Helpers;
using System.Data.Entity;
using System.Collections.Generic;

namespace DoctorWebASP.Controllers
{
    public class PacientesController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        IServicioPacientes consulta { get; set; }
        public PacientesController() : this(new ServicioPacientes())
        {
        }

        PacientesController(IServicioPacientes db)
        {
            this.consulta = db;
        }

        /// <summary>
        /// Obtiene la lista de pacientes
        /// </summary>
        /// <returns></returns>
        // GET: Paciente
        [Authorize]
        public ActionResult Index()
        {
            return View(consulta.ObtenerPacientesList());
        }

        /// <summary>
        /// Metodo que invoca index
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Index</returns>
        [HttpPost]
        [Authorize]
        public ActionResult Index(string userId)
        {

            var paciente = consulta.ObtenerPacientesList();
            if (paciente == null)
            {
                string mensaje = "No existe Paciente con esa Cedula";
                return RedirectToAction("SadFace", "Pacientes", new { mensaje });
            }
            else
            {
                var viewModel = new PacientesViewModel
                {
                    Paciente = paciente
                };
                return View("Index", viewModel);

            }
        }

        /// <summary>
        /// Metodo para desplegar la ventana de crear
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Create()
        {
            //Se crea el paciente
            var model = Fabrica.CrearPaciente();
            var seguros = new SelectList("");

            try
            {
                //se obtienen la lista de los seguros
                seguros = consulta.ObtenerSeguros();
                ViewBag.List = seguros;
                return View(model);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                string mensaje = "Ha ocurrido un error con la base de datos de la aplicacion";
                return RedirectToAction("SadFace", "Pacientes", new { mensaje });
            }

        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var model = Fabrica.CrearPaciente();
            var seguros = new SelectList("");

            try
            {
                if (ModelState.IsValid)
                {
                    //le asigno los seguros al paciente
                    model = AddSeguros(model);
                    //guardo el paciente
                    consulta.GuardarPaciente(model);
                    return RedirectToAction("Index");
                }
                //se obtienen la lista de los seguros
                seguros = consulta.ObtenerSeguros();                
                ViewBag.List = seguros;
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                string mensaje = "Ha ocurrido un error con la base de datos de la aplicacion";
                return RedirectToAction("SadFace", "Pacientes", new { mensaje });
            }

        }
        [HttpPost]
        public ActionResult Edit(int id)
        {
            var model = Fabrica.CrearPaciente();
            var seguros = new SelectList("");

            try
            {
                model = consulta.ObtenerPaciente(id.ToString());
                seguros = consulta.ObtenerSeguros();

                ViewBag.List = seguros;
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                string mensaje = "Ha ocurrido un error con la base de datos de la aplicacion";
                return RedirectToAction("SadFace", "Pacientes", new { mensaje });

            }

        }

        public ActionResult Delete(int id)
        {
            Paciente db_paciente = db.Pacientes.FirstOrDefault(g => g.PersonaId == id);
            if (db_paciente != null)
            {
                foreach (Seguro Paciente_Seguro in db_paciente.Seguros.ToList())
                {
                    db_paciente.Seguros.Remove(Paciente_Seguro);
                }
                db.SaveChanges();
                db.Pacientes.Remove(db_paciente);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }




        /*


        public ActionResult Delete(int id)
        {
            Paciente db_paciente = db.Pacientes.FirstOrDefault(g => g.PersonaId == id);
            if (db_paciente != null)
            {
                foreach (Seguro Paciente_Seguro in db_paciente.Seguros.ToList())
                {
                    db_paciente.Seguros.Remove(Paciente_Seguro);
                }
                db.SaveChanges();
                db.Pacientes.Remove(db_paciente);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }*/


        /******************FALTA POR CAMBIAR********************/
        public Paciente AddSeguros(Paciente paciente)
        {
            if (Request.Form["ListSeguros"] == null)
                return paciente;

            string list_seguros = Request.Form["ListSeguros"].ToString();
            string[] seguros = list_seguros.Split(',');
            foreach (string seguro_in in seguros)
            {
                Seguro seguro_found;
                seguro_found = db.Seguros.Find(Convert.ToInt32(seguro_in));
                if (seguro_found != null)
                    paciente.Seguros.Add(seguro_found);
            }
            return paciente;
        }

        public Paciente AddSegurosEdit(Paciente paciente)
        {
            if (Request.Form["ListSeguros"] == null)
                return paciente;

            string list_seguros = Request.Form["ListSeguros"].ToString();
            string[] seguros = list_seguros.Split(',');
            foreach (Seguro seguro in paciente.Seguros.ToList())
            {
                paciente.Seguros.Remove(seguro);
                db.SaveChanges();
            }
            paciente.Seguros.Clear();
            foreach (string seguro_in in seguros)
            {
                Seguro seguro_found;
                seguro_found = db.Seguros.Find(Convert.ToInt32(seguro_in));
                if (seguro_found != null && !paciente.Seguros.Contains(seguro_found))
                {
                    paciente.Seguros.Add(seguro_found);
                    db.Entry(paciente).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return paciente;
        }

        public bool Selected(Paciente paciente, Seguro seguro)
        {
            if (paciente.Seguros.Contains(seguro))
                return true;
            else
                return false;
        }


        /***************************************************************************************************/




    }
}