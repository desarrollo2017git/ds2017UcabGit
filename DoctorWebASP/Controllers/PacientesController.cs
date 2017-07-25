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
    public class PacienteController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        public IServicioPaciente consulta { get; set; }
        public CitasController(): this(new ServicioCitas()) {
        }

        public CitasController(IServicioCitas db)
        {
            this.consulta = db;
        }





















        /*
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Paciente
        public ActionResult Index()
        {
            return View(db.Pacientes.ToList());
        }
        [HttpPost]
        public ActionResult Index(Paciente paciente)
        {
            string cedula = Request.Form["tCedula"].ToString();
            int i = db.Pacientes.Where(p => p.Cedula == cedula).Count();
            if (i > 0)
                return View(db.Pacientes.ToList().Where(p => p.Cedula == cedula));
            else
            {
                ViewBag.MensajeError = "No existe un paciente con ese número de cédula.";
                return View(db.Pacientes.ToList());
            }
        }
        [Authorize]
        public ActionResult Create()
        {
            Paciente pac = new Paciente();
            List<SelectListItem> ListSeguros = new List<SelectListItem>();
            foreach (Seguro seguros_in in db.Seguros)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Text = seguros_in.Nombre.ToString(),
                    Value = seguros_in.Id.ToString(),
                    Selected = false
                };
                ListSeguros.Add(selectListItem);
            }
            ViewBag.List = ListSeguros;
            return View(pac);
        }

        public ApplicationUser GetAppUser()
        {
            string userid = this.HttpContext.User.Identity.GetUserId();
            var store = new UserStore<ApplicationUser>(db);
            ApplicationUser UserAPP = store.FindByIdAsync(userid).Result;
            return UserAPP;
        }

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

        [HttpPost]
        public ActionResult Create(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                paciente.ApplicationUser = GetAppUser();
                paciente = AddSeguros(paciente);
                db.Pacientes.Add(paciente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<SelectListItem> ListSeguros = new List<SelectListItem>();
            foreach (Seguro seguros_in in db.Seguros)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Text = seguros_in.Nombre.ToString(),
                    Value = seguros_in.Id.ToString(),
                    Selected = false
                };
                ListSeguros.Add(selectListItem);
            }
            ViewBag.List = ListSeguros;
            return View();
        }
        public bool Selected(Paciente paciente, Seguro seguro)
        {
            if (paciente.Seguros.Contains(seguro))
                return true;
            else
                return false;
        }
        public ActionResult Edit(int id)
        {
            Paciente paciente = db.Pacientes.FirstOrDefault(x => x.PersonaId == id);
            List<SelectListItem> ListSeguros = new List<SelectListItem>();
            foreach (Seguro seguros_in in db.Seguros.ToList())
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Text = seguros_in.Nombre.ToString(),
                    Value = seguros_in.Id.ToString(),
                    Selected = Selected(paciente, seguros_in)
                };
                ListSeguros.Add(selectListItem);
            }
            ViewBag.List = ListSeguros;
            return View(paciente);
        }
        [HttpPost]
        public ActionResult Edit(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paciente).State = EntityState.Modified;
                db.SaveChanges();
                //Paciente db_paciente = db.Pacientes.FirstOrDefault(g => g.PersonaId == paciente.PersonaId);
                //paciente = AddSegurosEdit(db_paciente);
                return RedirectToAction("Index");
            }
            else
            {
                List<SelectListItem> ListSeguros = new List<SelectListItem>();
                foreach (Seguro seguros_in in db.Seguros.ToList())
                {
                    SelectListItem selectListItem = new SelectListItem()
                    {
                        Text = seguros_in.Nombre.ToString(),
                        Value = seguros_in.Id.ToString(),
                        Selected = Selected(paciente, seguros_in)
                    };
                    ListSeguros.Add(selectListItem);
                }
                ViewBag.List = ListSeguros;
                return View(paciente);
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
    }*/
    }