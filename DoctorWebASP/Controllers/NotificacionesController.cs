using DoctorWebASP.Controllers.Helpers;
using DoctorWebASP.Models;
using DoctorWebASP.Models.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DoctorWebASP.Controllers
{

    /// <summary>
    /// Esta clase es el controlador de notificaciones y atienen las solicitudes para procesarlar segun se requiera.
    /// </summary>
    public class NotificacionesController : Controller
    {
        #region Instancia NotificacionesController
        /// <summary>
        /// Instancia que da acceso a los servicios web.
        /// </summary>
        private IServicioNotificaciones Servicio { get; set; }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public NotificacionesController() : this(Fabrica.CrearServicioNotificaciones())
        {
        }

        /// <summary>
        /// Constructor para indicar una implementacion diferente para los servicios web.
        /// </summary>
        /// <param name="servicio"></param>
        public NotificacionesController(IServicioNotificaciones servicio) : base()
        {
            this.Servicio = servicio;
        }
        #endregion

        /// <summary>
        /// Esta accion solicita las notificaciones al servicio y returna la interfaz mostrando la informacion.
        /// </summary>
        /// <param name="nombre">Filtrar por nombre.</param>
        /// <param name="indice">Pagina a solicitar.</param>
        /// <param name="filas">Numero de filas.</param>
        /// <returns>Retorna la vista con la informacion solicitada.</returns>
        public ActionResult Index(string nombre = null, int indice = 0, int filas = 5)
        {
            var cantidadPaginas = 0;
            List<Notificacion> notificaciones = null;
            try
            {
                notificaciones = Servicio.ObtenerTodos(out cantidadPaginas, nombre, indice, filas);
            }
            catch (Exception ex)
            {
                if (Session != null)
                    Session.IndicarNotificacion(ex.Message, EnumDoctorWebTipoNotificacion.danger);
                notificaciones = Fabrica.CrearListaNotificaciones();
            }


            if (ViewData != null)
            {

                ViewData.IndicarNotificacionNombre(nombre);
                ViewData.IndicarFilas(filas);
                ViewData.IndicarPermitirSiguiente(indice < cantidadPaginas - 1);
                ViewData.IndicarPermitirAnterior(indice > 0);
                ViewData.IndicarSiguienteIndice(indice + 1);
                ViewData.IndicarAnteriorIndice(indice - 1);
                ViewData.IndicarTotalPaginas(cantidadPaginas);
            }
            return View(model: notificaciones);
        }

        /// <summary>
        /// Esta accion muestra el formulario para registrar una notificacion o ver el detalle de alguna existente.
        /// </summary>
        /// <param name="codigo">Codigo de Notificacion.</param>
        /// <returns>Retorna la vista con el formulario con la informacion de la notificacion.</returns>
        public ActionResult Detail(int codigo)
        {
            Notificacion model = null;
            if (codigo != 0)
            {
                try
                {
                    model = Servicio.Obtener(codigo);
                }
                catch (Exception ex)
                {
                    if (Session != null)
                        Session.IndicarNotificacion(ex.Message, EnumDoctorWebTipoNotificacion.danger);
                }
            }
            else
            {
                model = Fabrica.CrearNotificaciones();
            }
            if (model != null)
            {
                return View(model: model);
            }
            else
                return RedirectToAction("Index");
        }

        /// <summary>
        /// Permite crear una notificacion.
        /// </summary>
        /// <param name="collection">Conjunto de datos enviados por el formulario.</param>
        /// <returns>Redirecciona al index una vez concluye.</returns>
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(FormCollection collection)
        {
            var model = Fabrica.CrearNotificaciones();
            try
            {
                model.NotificacionId = 0;
                model.Actualizar(collection);
                var mensaje = String.Empty;
                var sinProblemas = Servicio.Guardar(out mensaje, model);
                if (sinProblemas && Session != null)
                {
                    Session.IndicarNotificacion("Se ha creado la notificacion sin problemas.", EnumDoctorWebTipoNotificacion.success);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (Session != null)
                    Session.IndicarNotificacion(ex.Message, EnumDoctorWebTipoNotificacion.danger);
                return View("Detail", model);
            }
        }

        /// <summary>
        /// Permite editar una notificacion.
        /// </summary>
        /// <param name="collection">Conjunto de datos enviados por el formulario.</param>
        /// <returns>Redirecciona al index una vez concluye.</returns>
        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(FormCollection collection)
        {
            var model = Fabrica.CrearNotificaciones();
            try
            {
                model.Actualizar(collection);
                var mensaje = String.Empty;
                var sinProblemas = Servicio.Guardar(out mensaje, model);
                if (sinProblemas && Session != null)
                {
                    Session.IndicarNotificacion("Se ha actualizado la notificacion sin problemas.", EnumDoctorWebTipoNotificacion.success);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (model == null)
                    return RedirectToAction("Index");

                if (Session != null)
                    Session.IndicarNotificacion(ex.Message, EnumDoctorWebTipoNotificacion.danger);
                return View("Detail", model);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public ActionResult Delete(int codigo)
        {
            try
            {
                var model = Servicio.Obtener(codigo);
                if (model != null)
                    return View(model: model);
            }
            catch (Exception ex)
            {
                if (Session != null)
                    Session.IndicarNotificacion(ex.Message, EnumDoctorWebTipoNotificacion.danger);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Permite borrar una notificacion.
        /// </summary>
        /// <param name="collection">Conjunto de datos enviados por el formulario.</param>
        /// <returns>Redirecciona al index una vez concluye.</returns>
        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            try
            {
                var codigo = int.Parse(collection["NotificacionId"]);
                var mensaje = String.Empty;
                var sinProblemas = Servicio.Borrar(out mensaje, codigo);
                if (sinProblemas && Session != null)
                {
                    Session.IndicarNotificacion("Se ha borrado la notificacion sin problemas.", EnumDoctorWebTipoNotificacion.success);
                }
            }
            catch (Exception ex)
            {
                if (Session != null)
                    Session.IndicarNotificacion(ex.Message, EnumDoctorWebTipoNotificacion.danger);
            }
            return RedirectToAction("Index");
        }
    }
}