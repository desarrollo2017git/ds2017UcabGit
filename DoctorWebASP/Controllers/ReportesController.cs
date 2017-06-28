using DoctorWebASP.Controllers.Helpers;
using DoctorWebASP.Models;
using DoctorWebASP.Models.Service;
using DoctorWebASP.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
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
                if (ex is SqlException || ex is DataException || ex is EntityException)
                    resultadoProcesoR2.Mensaje = "Hay un error de conexión con la base de datos.";
                else
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
                if (ex is SqlException || ex is DataException || ex is EntityException)
                    resultadoProcesoR3.Mensaje = "Hay un error de conexión con la base de datos.";
                else
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
                if (ex is SqlException || ex is DataException || ex is EntityException)
                    resultadoProcesoR5.Mensaje = "Hay un error de conexión con la base de datos.";
                else
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

        #region REPORTE #1 - Cantidad de usuarios registrados en un tiempo determinado
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
        #endregion

        #region REPORTE #2 - Promedio de edad de los pacientes.
        public double getPromedioEdadPaciente()
        {
            var result = from p in db.Personas
                            where (p is Paciente)
                            select p.FechaNacimiento;

            if (result == null)
                throw new DoctorWebException("Hay un problema con la consulta en la base de datos.");

            double total = 0;

            double cantidadPacientes = result.Count();

            if (cantidadPacientes == 0)
                throw new DivideByZeroException("Hay un error de división entre cero.");

            foreach (var r in result.ToList())
            {
                Age age = new Age(r, DateTime.Today.AddDays(1).AddTicks(-1));
                total = total + age.Years;
            }

            double promedio = total / cantidadPacientes;

            if (Double.IsInfinity(promedio) || Double.IsNaN(promedio))
                throw new NotFiniteNumberException("La operación retorna un tipo de dato no válido.");

            return promedio;
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

            double promedio = (double)cantidadCitas / (double)cantidadMedicos;

            if (Double.IsInfinity(promedio) || Double.IsNaN(promedio))
                throw new NotFiniteNumberException("La operación retornó un número no válido.");

            return promedio;
        }
        #endregion

        #region REPORTE #4 - Promedio de recursos disponibles en un tiempo determinado.
        [HttpPost]
        public ActionResult getPromedioRecursosDisponibles(string fechaInicioStr, string fechaFinStr)
        {
            var resultadoProceso = new ResultadoProceso();

            try
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

                double? cantidadRecursos = (from rh in db.RecursosHospitalarios
                                            select rh).Count();

                if (result == null || almacen == null || cantidadRecursos == null)
                    throw new DoctorWebException("Hay un problema con la consulta en la base de datos.");

                if (cantidadRecursos == 0)
                    throw new DivideByZeroException("Hay un error de división entre cero.");

                double? totalCantidadRecursos = 0;

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

                double promedio = (double) totalCantidadRecursos / (double) cantidadRecursos;

                if (Double.IsInfinity(promedio) || Double.IsNaN(promedio))
                    throw new NotFiniteNumberException("La operación retornó un número no válido.");

                resultadoProceso.Inicializar(promedio.ToString());
                //return Json(new { cantidad = totalCantidadRecursos / cantidadRecursos});
                //return (Newtonsoft.Json.JsonConvert.SerializeObject(ResultadoProceso));
                //return JsonConvert.SerializeObject(ResultadoProceso, Formatting.Indented);
                return Json(new { resultadoProceso } );
                //return Json(new { metricas = metrics });
            }
            catch (FormatException)
            {
                resultadoProceso.Mensaje = "Hay un error con el formato de fecha.";
                return Json(new { resultadoProceso });
            }
            catch (SqlException)
            {
                resultadoProceso.Mensaje = "Hay un error de conexión con la base de datos.";
                return Json(new { resultadoProceso });
            }
            catch(Exception ex)
            {
                resultadoProceso.Mensaje = (ex.InnerException == null) ? ex.Message : ex.InnerException.Message;
                return Json(new { resultadoProceso });
            }
        }
        #endregion

        #region REPORTE #5 - Promedio de uso de la aplicación
        public double getPromedioUsoApp()
        {
            double? bitacora = (from b in db.Bitacoras
                               select b).Count();

            double? usuarios = (from u in db.Users
                               select u).Count();

            if (bitacora == null || usuarios == null)
                throw new DoctorWebException("Hay un problema con la consulta en la base de datos.");

            if (usuarios == 0)
                throw new DivideByZeroException("Hay un error de división entre cero.");

            double promedio = (double)bitacora / (double)usuarios;

            if (Double.IsInfinity(promedio) || Double.IsNaN(promedio))
                throw new NotFiniteNumberException("La operación retorna un tipo de dato no válido.");

            return promedio;
        }
        #endregion

        #region REPORTE #6 - Promedio de citas canceladas por médico en un tiempo determinado
        [HttpPost]
        public ActionResult getPromedioCitasCanceladasPorMedico(string fechaInicioStr, string fechaFinStr)
        {
            DateTime dtFechaInicio = DateTime.Parse(fechaInicioStr, CultureInfo.InvariantCulture);
            DateTime dtFechaFin = DateTime.Parse(fechaFinStr, CultureInfo.InvariantCulture);

            double? cantidadCitasCanceladas = (from c in db.Calendarios
                                              where c.Cancelada & c.Disponible == 1 & c.HoraInicio >= dtFechaInicio & c.HoraFin <= dtFechaFin
                                              select c).Count();
            double? cantidadMedicos = (from p in db.Personas
                                      where p is Medico
                                      select p).Count();

            if (cantidadCitasCanceladas == null || cantidadMedicos == null)
                throw new Exception("Hay un problema con la consulta en la base de datos.");

            if (cantidadMedicos == 0)
                throw new DivideByZeroException("Hay un error de división entre cero.");

            return Json(new { cantidad = cantidadCitasCanceladas / cantidadMedicos, fechaInicio = dtFechaInicio.ToString(), fechaFin = dtFechaFin.ToString() });
        }
        #endregion

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

            if (selectedEntities == null)
                throw new Exception("Hay un problema con la consulta en la base de datos.");

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

            if (selectedEntities == null || selectedAttributes == null)
                throw new Exception("Hay un problema con la consulta en la base de datos.");

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

        public int pruebaunitaria()
        {
            var result = 2 + 2;
            return result;
        }
    }
}