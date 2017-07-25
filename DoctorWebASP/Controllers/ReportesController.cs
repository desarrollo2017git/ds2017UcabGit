using DoctorWebASP.Controllers.Helpers;
using DoctorWebASP.Models.Results;
using DoctorWebASP.Models.Services;
using DoctorWebASP.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DoctorWebASP.Controllers
{
    /// <summary>
    /// Clase controladora para el módulo de reportes. 
    /// </summary>
    public class ReportesController : Controller
    {
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

        #region REPORTES PRESTABLECIDOS
        #region INDEX
        /// <summary>
        /// Método que instancia la interfaz o vista para los reportes preestablecidos.
        /// </summary>
        /// <remarks>Se utiliza el método Http GET.</remarks>
        /// <returns>Interfaz o vista de reportes preestablecidos.</returns>
        // GET: Reportes
        public ActionResult Index()
        {
            var indexViewModel = new ReportesIndexViewModel();
            indexViewModel.resultadoProcesoR2 = getPromedioEdadPaciente();
            indexViewModel.resultadoProcesoR3 = getPromedioCitasPorMedico();
            indexViewModel.resultadoProcesoR5 = getPromedioUsoApp();

            return View(indexViewModel);
        }
        #endregion

        #region REPORTE #1 - Cantidad de usuarios registrados en un tiempo determinado
        /// <summary>
        /// Método utilizado para obtener la cantidad de usuarios registrados durante el periodo de tiempo seleccionado por el usuario.
        /// Recibe los parámetros:
        /// </summary>
        /// <remarks>Se utiliza el método Http Post.</remarks>
        /// <param name="fechaInicioStr">Fecha incicial del periodo seleccionado para el conteo de usuarios registrados.</param>
        /// <param name="fechaFinStr">Fecha final del periodo seleccionado para el conteo de usuarios registrados.</param>
        /// <returns>Retorna un objeto de tipo JSON que contiene la cantidad de usuarios registrados.</returns>
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

        #region REPORTE #2 - Promedio de edad de los pacientes.
        /// <summary>
        /// Método utilizado para obtener el promedio de edad de los pacientes.
        /// </summary>
        /// <returns>Promedio de edad de los pacientes.</returns>
        public ResultadoProceso getPromedioEdadPaciente()
        {
            ResultadoProceso resultado = Fabrica.CrearResultadoProceso();
            try
            {
                resultado = Servicio.getPromedioEdadPaciente();
            }
            catch (Exception ex)
            {

                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }
        #endregion

        #region REPORTE #3 - Promedio de citas por médico.
        /// <summary>
        /// Método utilizado para obtener el promedio de citas por médico.
        /// </summary>
        /// <returns>Promedio de citas canceladas por médico.</returns>
        public ResultadoProceso getPromedioCitasPorMedico()
        {
            ResultadoProceso resultado = Fabrica.CrearResultadoProceso();
            try
            {
                resultado = Servicio.getPromedioCitasPorMedico();
            }
            catch (Exception ex)
            {

                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }
        #endregion

        #region REPORTE #4 - Promedio de recursos disponibles en un tiempo determinado.
        /// <summary>
        /// Método utilizado para obtener el promedio de recursos disponibles en un periodo de tiempo seleccionado por el usuario.
        /// Recibe los parámetros:
        /// </summary>
        /// <param name="fechaInicioStr">Fecha incicial del periodo seleccionado para la revisión de recursos disponibles.</param>
        /// <param name="fechaFinStr">Fecha final del periodo seleccionado para la revisión de recursos disponibles.</param>
        /// <returns>Retorna un objeto de tipo JSON que contiene el promedio de recusos disponibles.</returns>
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

        #region REPORTE #5 - Promedio de uso de la aplicación
        /// <summary>
        /// Método utilizado para obtener el promedio de uso de la aplicación.
        /// </summary>
        /// <returns>Promedio de uso de la aplicación.</returns>
        public ResultadoProceso getPromedioUsoApp()
        {
            ResultadoProceso resultado = Fabrica.CrearResultadoProceso();
            try
            {
                resultado = Servicio.getPromedioUsoApp();
            }
            catch (Exception ex)
            {

                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }
        #endregion

        #region REPORTE #6 - Promedio de citas canceladas por médico en un tiempo determinado
        /// <summary>
        /// Método utilizado para obtener el promedio de citas canceladas por médico en un periodo de tiempo seleccionado por el usuario.
        /// </summary>
        /// <param name="fechaInicioStr">Fecha incicial del periodo seleccionado para la revisión de citas canceladas.</param>
        /// <param name="fechaFinStr">Fecha final del periodo seleccionado para la revisión de citas canceladas.</param>
        /// <returns>Retorna un objeto de tipo JSON que contiene el promedio de citas canceladas por médico.</returns>
        [HttpPost]
        public ActionResult getPromedioCitasCanceladasPorMedico(string fechaInicioStr, string fechaFinStr)
        {
            var resultado = Fabrica.CrearResultadoProceso();

            try
            {
                resultado = Servicio.getPromedioCitasCanceladasPorMedico(fechaInicioStr, fechaFinStr);
            }
            catch (Exception ex)
            {

                resultado.Mensaje = ex.Message;
            }
            return Json(new { resultado });
        }
        #endregion
        #endregion

        #region REPORTES CONFIGURADOS
        #region INDEX CONFIGURADOS
        /// <summary>
        /// Método que instancia la interfaz o vista para los reportes configurados.
        /// </summary>
        /// <returns>Interfaz o vista de reportes configurados.</returns>
        public ActionResult Configurados()
        {
            var result = getEntities();

            return View(result);
        }
        #endregion

        #region PASO #1: Obtener entidades a mostrar.
        /// <summary>
        /// Metodo utilizado para llenar una lista de entidades.
        /// </summary>
        /// <returns>Colección de entidades con su definición y descripción.</returns>
        public Dictionary<string, string> getEntities()
        {
            var entitiesDict = new Dictionary<string, string>
            {
                { "CentroMedico", "Centro Medico" },
                { "Medico", null },
                { "Paciente", null },
                { "RecursoHospitalario", "Recurso Hospitalario" }
            };

            return entitiesDict;
        }
        #endregion

        #region PASO #2: Obtener lista de atributos según las entidades seleccionadas.
        /// <summary>
        /// Método utilizado para llenar una lista de atributos, según el parámetro recibido. 
        /// </summary>
        /// <param name="selectedEntities">Parámetro que indica las entidades seleccionadas.</param>
        /// <returns>Retorna un objeto de tipo JSON que contiene los atibutos de las entidades seleccionadas en el Paso #1.</returns>
        [HttpPost]
        public JsonResult getAttributes(List<string> selectedEntities)
        {
            ResultadoServicio<String> resultado = Fabrica.CrearResultadoDe<String>();
            try
            {
                resultado = Servicio.obtenerAtributos(selectedEntities);
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return Json(new { answer = resultado.Contenido });
        }
        #endregion

        #region PASO #3: Obtener reportes según la selección del usuario.
        [HttpPost]
        public JsonResult getReport(Dictionary<string, Dictionary<string,Dictionary<string,string>>> queryObj)
        {
            return Json(new { answer = queryObj });
        }
        #endregion
        #endregion
    }
}