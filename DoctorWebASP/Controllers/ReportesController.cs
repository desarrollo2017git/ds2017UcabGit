﻿using DoctorWebASP.Controllers.Helpers;
using DoctorWebASP.Models;
using DoctorWebASP.ViewModels;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace DoctorWebASP.Controllers
{
    public class ReportesController : Controller
    {
        private ApplicationDbContext db;
        private string lastTimeOnDay = "11:59:59 PM";
        private string firstTimeOnDay = "12:00:00 AM";


        public ReportesController()
        {
            db = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        // GET: Reportes
        public ActionResult Index()
        {
            var indexViewModel = new ReportesIndexViewModel();

            // REPORTE #2 - Promedio de edad de los pacientes
            indexViewModel.promedioEdadPacientes = getPromedioEdadPaciente();

            // REPORTE #3 - Promedio de citas por médico
            indexViewModel.promedioCitasPorMedico = getPromedioCitasPorMedico();

            // REPORTE #5 - Promedio de uso de la aplicación
            indexViewModel.promedioUsoApp = getPromedioUsoApp();

            return View(indexViewModel);
        }

        public ActionResult Configurados()
        {
            IEnumerable<string> result = getEntities();

            return View(result);
        }

        [HttpPost]
        public ActionResult getCantidadUsuariosRegistrados(string fechaInicioStr, string fechaFinStr)
        {
            DateTime fechaInicio = DateTime.Parse(fechaInicioStr + " " + firstTimeOnDay, CultureInfo.InvariantCulture);
            DateTime fechaFin = DateTime.Parse(fechaFinStr + " " + lastTimeOnDay, CultureInfo.InvariantCulture);

            var result = from p in db.Personas
                         where p.FechaCreacion >= fechaInicio & p.FechaCreacion <= fechaFin
                         select p;

            return Json(new { cantidad = result.Count(), fechaInicio = fechaInicio.ToString(), fechaFin = fechaFin.ToString() });
        }

        public double getPromedioEdadPaciente()
        {
            var result = from p in db.Personas
                         where (p is Paciente)
                         select p.FechaNacimiento;

            int total = 0;

            foreach (var r in result.ToList())
            {
                Age age = new Age(r, DateTime.Today.AddDays(1).AddTicks(-1));
                total = total + age.Years;
            }

            return total / result.Count();

        }

        public double getPromedioCitasPorMedico()
        {
            double cantidadCitas = (from c in db.Calendarios
                                    where !c.Cancelada & c.Disponible == 0
                                    select c).Count();
            double cantidadMedicos = (from p in db.Personas
                                      where p is Medico
                                      select p).Count();

            return cantidadCitas / cantidadMedicos;
        }

        [HttpPost]
        public ActionResult getPromedioRecursosDisponibles(string fechaInicioStr, string fechaFinStr)
        {
            DateTime dtFechaInicio = DateTime.Parse(fechaInicioStr + " " + firstTimeOnDay, CultureInfo.InvariantCulture);
            DateTime dtFechaFin = DateTime.Parse(fechaFinStr + " " + lastTimeOnDay, CultureInfo.InvariantCulture);

            var result = from ur in db.UsoRecursos
                         join ci in db.Citas on ur.Cita equals ci
                         join ca in db.Calendarios on ci.Calendario equals ca
                         where ca.HoraInicio >= dtFechaInicio & ca.HoraInicio <= dtFechaFin & !ca.Cancelada
                         select ur;

            var almacen = (from a in db.Almacenes
                           select a);

            double cantidadRecursos = (from rh in db.RecursosHospitalarios
                                       select rh).Count();

            double totalCantidadRecursos = 0;

            foreach (var a in almacen.ToList())
            {
                foreach (var ur in result.ToList())
                {
                    if (a.RecursoHospitalario == ur.RecursoHospitalario)
                    {
                        if (a.Disponible - ur.Cantidad >= 0)
                        {
                            totalCantidadRecursos = totalCantidadRecursos + (a.Disponible - ur.Cantidad);
                        }
                    }
                }
            }

            return Json(new { cantidad = totalCantidadRecursos / cantidadRecursos, fechaInicio = dtFechaInicio.ToString(), fechaFin = dtFechaFin.ToString() });
        }

        [HttpPost]
        public ActionResult getPromedioCitasCanceladasPorMedico(string fechaInicioStr, string fechaFinStr)
        {
            DateTime dtFechaInicio = DateTime.Parse(fechaInicioStr, CultureInfo.InvariantCulture);
            DateTime dtFechaFin = DateTime.Parse(fechaFinStr, CultureInfo.InvariantCulture);

            double cantidadCitasCanceladas = (from c in db.Calendarios
                                              where c.Cancelada & c.Disponible == 1 & c.HoraInicio >= dtFechaInicio & c.HoraFin <= dtFechaFin
                                              select c).Count();
            double cantidadMedicos = (from p in db.Personas
                                      where p is Medico
                                      select p).Count();

            return Json(new { cantidad = cantidadCitasCanceladas / cantidadMedicos, fechaInicio = dtFechaInicio.ToString(), fechaFin = dtFechaFin.ToString() });
        }

        public double getPromedioUsoApp()
        {
            double bitacora = (from b in db.Bitacoras
                               select b).Count();

            double usuarios = (from u in db.Users
                               select u).Count();

            return bitacora / usuarios;
        }

        public IEnumerable<string> getEntities()
        {
            List<string> entities = new List<string>();

            entities.Add("Paciente");
            entities.Add("Medico");
            entities.Add("Recurso Hospitalario");
            entities.Add("Centro Medico");

            return entities;
        }

        [HttpPost]
        public ActionResult getAttributes(List<string> selectedEntities)
        {
            List<string> attributes = new List<string>();
            object entity = null;

            foreach (var se in selectedEntities)
            {
                if (se.Equals("Paciente"))
                {
                    entity = new Paciente();

                    foreach (PropertyInfo prop in entity.GetType().GetProperties())
                    {
                        if (prop.Name.Equals("Nombre") || prop.Name.Equals("Apellido") || prop.Name.Equals("TipoSangre"))
                            attributes.Add(se + "." + prop.Name);
                    }
                }

                if (se.Equals("Medico"))
                {
                    entity = new Medico();

                    foreach (PropertyInfo prop in entity.GetType().GetProperties())
                    {
                        if (prop.Name.Equals("Nombre") || prop.Name.Equals("Apellido") || prop.Name.Equals("Sueldo"))
                            attributes.Add(se + "." + prop.Name);
                    }
                }

                if (se.Equals("Recurso Hospitalario"))
                {
                    entity = new RecursoHospitalario();

                    foreach (PropertyInfo prop in entity.GetType().GetProperties())
                    {
                        if (prop.Name.Equals("Nombre") || prop.Name.Equals("Tipo"))
                            attributes.Add(se + "." + prop.Name);
                    }
                }

                if (se.Equals("Centro Medico"))
                {
                    entity = new CentroMedico();

                    foreach (PropertyInfo prop in entity.GetType().GetProperties())
                    {
                        if (prop.Name.Equals("Nombre"))
                            attributes.Add(se + "." + prop.Name);
                    }
                }
            }

            return Json(new { atributos = attributes });
        }

        [HttpPost]
        public ActionResult getMetrics(List<string> selectedEntities, List<string> selectedAttributes)
        {
            List<string> metrics = new List<string>();

            if (selectedEntities.Count() == 2)
            {
                if (selectedEntities.Contains("Paciente") & selectedEntities.Contains("Medico"))
                {
                    metrics.Add("Lista de pacientes por medico.");
                    metrics.Add("Lista de medicos por paciente.");
                }
            }

            return Json(new { metricas = metrics });
        }

        [HttpPost]
        public string getReport(string selectedMetric)
        {

            dynamic pacientesPorMedico = from m in db.Personas
                                         where m is Medico
                                         join ca in db.Calendarios
                                         on m equals ca.Medico
                                         join ci in db.Citas
                                         on ca equals ci.Calendario
                                         /*select new
                                         {
                                             m.Nombre,
                                             ca.HoraInicio,
                                             ca.HoraFin
                                         };*/
                                         join pa in db.Personas
                                         on ci.Paciente equals pa
                                         where pa is Paciente
                                         select new
                                         {
                                             Medico = m.Nombre + " " + m.Apellido,
                                             Paciente = pa.Nombre + " " + pa.Apellido
                                         };

            //if (selectedMetric.Equals("Lista de pacientes por medico."))
            //{
            /*var pacientesPorMedico = from m in db.Personas
                                     where m is Medico
                                     let cal = (from ca in db.Calendarios
                                                where ca.Medico == m
                                                let cit = (from c in db.Citas
                                                           where c.Calendario == ca
                                                           let pac = (from pa in db.Personas
                                                                      where pa is Paciente & c.Paciente == pa
                                                                      select pa.NombreCompleto)
                                                           select pac)
                                                select cit)
                                     select new
                                     {
                                         m.NombreCompleto, cal
                                     };*/


            //dynamic pacientesPorMedico = (from m in db.Personas
            //                                   where m is Medico
            //                                   select new { m.Nombre, m.Email });

                /*join ci in db.Citas on p equals ci.Paciente
                join m in db.Personas
                where (p is Paciente)
                select ci.CentroMedico = m.*/

                                     //return Json(new { reporte = "1" });
            return (Newtonsoft.Json.JsonConvert.SerializeObject(pacientesPorMedico));
            //}

            //return Json(new { reporte = selectedMetric });
        }

        public int pruebaunitaria()
        {
            var result = 2 + 2;
            return result;
        }
    }
}