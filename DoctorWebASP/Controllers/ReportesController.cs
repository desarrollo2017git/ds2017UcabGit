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
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace DoctorWebASP.Controllers
{
    public class ReportesController : Controller
    {
        private ApplicationDbContext db;

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
            // REPORTE #1
            /*string dateString = "02-06-2017";
            DateTime date = DateTime.Parse(dateString);

            var indexViewModel = new ReportesIndexViewModel();
            indexViewModel.cantidadUsuariosRegistrados = getCantidadUsuariosRegistrados(date);*/

            var indexViewModel = new ReportesIndexViewModel();

            // REPORTE #2 - Promedio de edad de los pacientes
            indexViewModel.promedioEdadPacientes = getPromedioEdadPaciente();

            // REPORTE #3 - Promedio de citas por médico
            indexViewModel.promedioCitasPorMedico = getPromedioCitasPorMedico();

            // REPORTE #5 - Promedio de uso de la aplicación

            return View(indexViewModel);
        }

        [HttpPost]
        public ActionResult Prueba()
        {
            return Json(new { id = 1, value = "new" });
        }

        [HttpPost]
        public ActionResult getCantidadUsuariosRegistrados(string fechaInicioStr, string fechaFinStr)
        {
            DateTime fechaInicio = DateTime.Parse(fechaInicioStr, CultureInfo.InvariantCulture);
            DateTime fechaFin = DateTime.Parse(fechaFinStr, CultureInfo.InvariantCulture);

            var result = from p in db.Personas
                        where p.FechaCreacion >= fechaInicio & p.FechaCreacion <= fechaFin
                        select p;

            return Json(new { cantidad = result.Count(), fechaInicio = fechaInicio.ToString(), fechaFin = fechaFin.ToString() } );
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

            return total/result.Count();
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
            DateTime dtFechaInicio = DateTime.Parse(fechaInicioStr, CultureInfo.InvariantCulture);
            DateTime dtFechaFin = DateTime.Parse(fechaFinStr, CultureInfo.InvariantCulture);

            var result = from ur in db.UsoRecursos
                         join ci in db.Citas on ur.Cita equals ci
                         join ca in db.Calendarios on ci.Evento equals ca
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

            return Json(new { cantidad = totalCantidadRecursos/cantidadRecursos, fechaInicio = dtFechaInicio.ToString(), fechaFin = dtFechaFin.ToString() });
        }
    }
}