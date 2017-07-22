using DoctorWebASP.Controllers.Helpers;
using DoctorWebASP.Models;
using DoctorWebASP.Models.Results;
using DoctorWebASP.Models.Services;
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

        #region Instancia ReportesController
        /// <summary>
        /// Instancia que da acceso a los servicios web.
        /// </summary>
        private IServicioReportes Servicio { get; set; }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public ReportesController() : this(Fabrica.CrearServicioReportes())
        {
        }

        /// <summary>
        /// Constructor para indicar una implementacion diferente para los servicios web.
        /// </summary>
        /// <param name="servicio"></param>
        public ReportesController(IServicioReportes servicio) : base()
        {
            this.Servicio = servicio;
        }
        #endregion

        private string lastTimeOnDay = "11:59:59 PM";
        private string firstTimeOnDay = "12:00:00 AM";

        #region REPORTES PRESTABLECIDOS
        /// <summary>
        /// Método que instancia la interfaz o vista para los reportes preestablecidos.
        /// </summary>
        /// <returns>Retorna un objeto de tipo View</returns>
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
                //indexViewModel.promedioEdadPacientes = getPromedioEdadPaciente();
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
                //indexViewModel.promedioCitasPorMedico = getPromedioCitasPorMedico();
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
                //indexViewModel.promedioUsoApp = getPromedioUsoApp();
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

        #region REPORTE #1 - Cantidad de usuarios registrados en un tiempo determinado
        /// <summary>
        /// Método utilizado para obtener la cantidad de usuarios registrados durante el periodo de tiempo seleccionado por el usuario.
        /// Recibe los parámetros:
        /// </summary>
        /// <param name="fechaInicioStr">Fecha incicial para el periodo de conteo de registro de usuarios.</param>
        /// <param name="fechaFinStr">Fecha incicial para el periodo de conteo de registro de usuarios.</param>
        /// <returns>Retorna un objeto de tipo JSon</returns>
        [HttpPost]
        public ActionResult getCantidadUsuariosRegistrados(string fechaInicioStr, string fechaFinStr)
        {
            ResultadoProceso resultado = Fabrica.CrearResultadoProceso();
            try
            {
                resultado = Servicio.getCantidadUsuariosRegistrados(fechaInicioStr, fechaFinStr);
            } catch (Exception ex)
            {

                resultado.Mensaje = ex.Message;
            }
            return Json(new { resultado });
        }
        #endregion

        #region REPORTE #4 - Promedio de recursos disponibles en un tiempo determinado.
        /// <summary>
        /// Método utilizado para obtener el promedio de recursos disponibles en un periodo de tiempo seleccionado por el usuario.
        /// Recibe los parámetros:
        /// </summary>
        /// <param name="fechaInicioStr">Fecha incicial para el periodo de conteo de registro de usuarios.</param>
        /// <param name="fechaFinStr">Fecha incicial para el periodo de conteo de registro de usuarios.</param>
        /// <returns>Retorna un objeto de tipo JSON</returns>
        [HttpPost]
        public ActionResult getPromedioRecursosDisponibles(string fechaInicioStr, string fechaFinStr)
        {
            var resultado = Fabrica.CrearResultadoProceso();

            try
            {
                resultado = Servicio.getPromedioRecursosDisponibles(fechaInicioStr, fechaFinStr);
            }
            catch (Exception ex)
            {

                resultado.Mensaje = ex.Message;
            }
            return Json(new { resultado });
        }
        #endregion
        #endregion
    }
}