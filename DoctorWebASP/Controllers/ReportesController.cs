﻿using DoctorWebASP.Controllers.Helpers;
using DoctorWebASP.Models;
using DoctorWebASP.Models.Service;
using DoctorWebASP.ViewModels;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core;
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
            var resultadoProcesoR2 = new ResultadoProceso();
            var resultadoProcesoR3 = new ResultadoProceso();
            var resultadoProcesoR5 = new ResultadoProceso();

            try
            {
                // REPORTE #2 - Promedio de edad de los pacientes
                indexViewModel.promedioEdadPacientes = getPromedioEdadPaciente();
                resultadoProcesoR2.SinProblemas = true;
            }
            catch (Exception ex)
            {
                resultadoProcesoR2.Mensaje = (ex.InnerException == null) ? ex.Message : ex.InnerException.Message;
            }

            try
            {
                // REPORTE #3 - Promedio de citas por médico
                indexViewModel.promedioCitasPorMedico = getPromedioCitasPorMedico();
                resultadoProcesoR3.SinProblemas = true;
            }
            catch (Exception ex)
            {
                resultadoProcesoR3.Mensaje = (ex.InnerException == null) ? ex.Message : ex.InnerException.Message;
            }

            try
            {
                // REPORTE #5 - Promedio de uso de la aplicación
                indexViewModel.promedioUsoApp = getPromedioUsoApp();
                resultadoProcesoR5.SinProblemas = true;
            }
            catch (Exception ex)
            {
                resultadoProcesoR5.Mensaje = (ex.InnerException == null) ? ex.Message : ex.InnerException.Message;
            }

            indexViewModel.resultadoProcesoR2 = resultadoProcesoR2;
            indexViewModel.resultadoProcesoR3 = resultadoProcesoR3;
            indexViewModel.resultadoProcesoR5 = resultadoProcesoR5;

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

        #region REPORTE #2 - Promedio de edad de los pacientes.
        public double getPromedioEdadPaciente()
        {
            var result = from p in db.Personas
                            where (p is Paciente)
                            select p.FechaNacimiento;

            if (result == null)
                throw new Exception("Hay un problema con la consulta en la base de datos.");

            double total = 0;

            double cantidadPacientes = result.Count();

            if (cantidadPacientes == 0)
                throw new DoctorWebException("Hay un error de división entre cero.");

            foreach (var r in result.ToList())
            {
                Age age = new Age(r, DateTime.Today.AddDays(1).AddTicks(-1));
                total = total + age.Years;
            }

            return total / cantidadPacientes;
        }
        #endregion

        #region REPORTE #3 - Promedio de citas por médico.
        public double getPromedioCitasPorMedico()
        {
            double? cantidadCitas = (from c in db.Calendarios
                                 where !c.Cancelada & c.Disponible == 0
                                 select c).Count();
            double? cantidadMedicos = (from p in db.Personas
                                   where p is Medico
                                   select p).Count();
            if (cantidadMedicos == null || cantidadCitas == null)
                throw new DoctorWebException("Hay un problema con la consulta en la base de datos.");

            if (cantidadMedicos == 0)
                throw new DivideByZeroException("Hay un error de división entre cero.");

            return ((double) cantidadCitas / (double) cantidadMedicos);
        }
        #endregion

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

            entities.Add("Centro Medico");
            entities.Add("Medico");
            entities.Add("Paciente");
            entities.Add("Recurso Hospitalario");

            return entities;
        }

        [HttpPost]
        public ActionResult getAttributes(List<string> selectedEntities)
        {
            List<string> attributes = new List<string>();
            object entity = null;

            foreach (var se in selectedEntities)
            {
                if (se.Equals("Centro Medico"))
                {
                    entity = new CentroMedico();

                    foreach (PropertyInfo prop in entity.GetType().GetProperties())
                    {
                        if (prop.Name.Equals("Nombre"))
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

                if (se.Equals("Paciente"))
                {
                    entity = new Paciente();

                    foreach (PropertyInfo prop in entity.GetType().GetProperties())
                    {
                        if (prop.Name.Equals("Nombre") || prop.Name.Equals("Apellido") || prop.Name.Equals("TipoSangre"))
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

                
            }

            return Json(new { atributos = attributes });
        }

        [HttpPost]
        public ActionResult getMetrics(List<string> selectedEntities, List<string> selectedAttributes)
        {
            List<string> metrics = new List<string>();

            if (selectedEntities.Count() == 2)
            {
                if (selectedEntities.Contains("Medico") & selectedEntities.Contains("Paciente"))
                {
                    if (selectedAttributes.Contains("Medico.Nombre") & selectedAttributes.Contains("Medico.Apellido") & selectedAttributes.Contains("Paciente.Nombre") || selectedAttributes.Contains("Paciente.Apellido"))
                    {
                        metrics.Add("Lista de pacientes por medico.");
                        metrics.Add("Lista de medicos por paciente.");
                    }
                    else
                    {
                        metrics = null;
                    }
                }
            }

            return Json(new { metricas = metrics });
        }

        [HttpPost]
        public string getReport(string selectedMetric)
        {
            dynamic query = null;

            if (selectedMetric.Equals("Lista de pacientes por medico."))
            {
                query = from m in db.Personas
                        where m is Medico
                        join ca in db.Calendarios
                        on m equals ca.Medico
                        join ci in db.Citas
                        on ca equals ci.Calendario
                        join pa in db.Personas
                        on ci.Paciente equals pa
                        where pa is Paciente
                        select new
                        {
                            Medico = m.Nombre + " " + m.Apellido,
                            Paciente = pa.Nombre + " " + pa.Apellido
                        };
            }

            return (Newtonsoft.Json.JsonConvert.SerializeObject(query));
        }

        public int pruebaunitaria()
        {
            var result = 2 + 2;
            return result;
        }
    }
}