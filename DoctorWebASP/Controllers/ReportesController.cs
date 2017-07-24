﻿using DoctorWebASP.Controllers.Helpers;
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
        /// <summary>
        /// Método que instancia la interfaz o vista para los reportes preestablecidos.
        /// </summary>
        /// <returns>Retorna un objeto de tipo View</returns>
        // GET: Reportes
        public ActionResult Index()
        {
            var indexViewModel = new ReportesIndexViewModel();
            indexViewModel.resultadoProcesoR2 = getPromedioEdadPaciente();
            indexViewModel.resultadoProcesoR3 = getPromedioCitasPorMedico();
            indexViewModel.resultadoProcesoR5 = getPromedioUsoApp();

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

        #region REPORTE #2 - Promedio de edad de los pacientes.
        /// <summary>
        /// Método utilizado para obtener el promedio de edad de los pacientes.
        /// </summary>
        /// <returns>Retorna un tipo de dato double.</returns>
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
        /// <returns>Retorna un tipo de dato double.</returns>
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

        #region REPORTE #5 - Promedio de uso de la aplicación
        /// <summary>
        /// Método utilizado para obtener el promedio de uso de la aplicación.
        /// </summary>
        /// <returns>Returna un tipo de dato double.</returns>
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
        /// <param name="fechaInicioStr">Fecha incicial para el periodo de conteo de registro de usuarios.</param>
        /// <param name="fechaFinStr">Fecha incicial para el periodo de conteo de registro de usuarios.</param>
        /// <returns>Retorna un objeto de tipo JSON</returns>
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
        /// <summary>
        /// Método que instancia la interfaz o vista para los reportes configurados.
        /// </summary>
        /// <returns>Retorna un objeto de tipo View</returns>
        public ActionResult Configurados()
        {
            var result = getEntities();

            return View(result);
        }

        /// <summary>
        /// Metodo utilizado para llenar una lista de entidades.
        /// </summary>
        /// <returns>Retorna un tipo de dato IEnumerable </returns>
        public Dictionary<string,string> getEntities()
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

        /// <summary>
        /// Método utilizado para llenar una lista de atributos, según el parámetro recibido. 
        /// </summary>
        /// <param name="selectedEntities">Parámetro que indica las entidades seleccionadas.</param>
        /// <returns>Retorna un objeto de tipo JSON</returns>
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
    }
}