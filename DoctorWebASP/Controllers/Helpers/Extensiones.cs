using DoctorWebASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace DoctorWebASP.Controllers.Helpers
{
    /// <summary>
    /// En esta clase se define las extensiones a las clases que ya estan definidas.
    /// </summary>
    public static class Extensiones
    {
        #region Extension : Session
        private static bool HayValor<T>(this HttpSessionStateBase session, EnumDoctorWebSession tag, T value)
        {
            var currentValue = (T)session[tag.ToString()];
            if (currentValue != null && currentValue is T && currentValue.Equals(value))
                return true;
            return false;
        }

        private static void IndicarValor<T>(this HttpSessionStateBase session, EnumDoctorWebSession tag, T value)
        {
            session[tag.ToString()] = value;
        }

        private static T ObtenerValor<T>(this HttpSessionStateBase session, EnumDoctorWebSession tag)
        {
            return (T)session[tag.ToString()];
        }

        private static HttpSessionStateBase Base(this HttpSessionStateWrapper session)
        {
            return session;
        }

        public static bool HayError(this HttpSessionStateBase session)
        {
            return String.IsNullOrEmpty(session.ObtenerValor<string>(EnumDoctorWebSession.Error));
        }

        public static string Error(this HttpSessionStateBase session)
        {
            return session.ObtenerValor<string>(EnumDoctorWebSession.Error);
        }

        public static void IndicarError(this HttpSessionStateBase session, string value)
        {
            session.IndicarValor<string>(EnumDoctorWebSession.Error, value);
        }
        #endregion

        #region Extension : ViewDataDictionary
        private static bool HayValor<T>(this ViewDataDictionary bag, EnumDoctorWebViewBag tag, T value)
        {
            var currentValue = (T)bag[tag.ToString()];
            if (currentValue != null && currentValue is T && currentValue.Equals(value))
                return true;
            return false;
        }

        private static void IndicarValor<T>(this ViewDataDictionary bag, EnumDoctorWebViewBag tag, T value)
        {
            bag[tag.ToString()] = value;
        }

        private static T ObtenerValor<T>(this ViewDataDictionary bag, EnumDoctorWebViewBag tag)
        {
            return (T)bag[tag.ToString()];
        }

        public static bool HayError(this ViewDataDictionary bag)
        {
            var valor = Utilidades.ObtenerContextoHttp().Session[EnumDoctorWebSession.Error.ToString()];
            if (valor == null || !(valor is string))
                return false;
            return String.IsNullOrEmpty((string)valor);
        }

        public static string Error(this ViewDataDictionary bag)
        {
            var valor = Utilidades.ObtenerContextoHttp().Session[EnumDoctorWebSession.Error.ToString()];
            if (valor == null || !(valor is string))
                return null;
            return (string)valor;
        }

        public static void IndicarPaginaActual(this ViewDataDictionary bag, EnumDoctorWebPagina menu)
        {
            bag.IndicarValor<EnumDoctorWebPagina>(EnumDoctorWebViewBag.PaginaActual, menu);
        }

        public static bool EsPaginaActual(this ViewDataDictionary bag, EnumDoctorWebPagina menu)
        {
            var paginaActual = bag.ObtenerValor<EnumDoctorWebPagina>(EnumDoctorWebViewBag.PaginaActual);
            if (paginaActual == menu)
                return true;
            return false;
        }
        #endregion

        #region Grupo09

        public static SelectList ComboNotificacionEstado(this ViewDataDictionary bag, Notificacion model)
        {
            return new SelectList(
                      new List<Object>{
                           new { value = NotificacionEstado.Disponible , text = NotificacionEstado.Disponible.ToString()  },
                           new { value = NotificacionEstado.Borrada , text = NotificacionEstado.Borrada.ToString()  }
                        },
                      "value",
                      "text",
                      model.Estado
                );
        }

        public static void IndicarNotificacionNombre(this ViewDataDictionary bag, string valor)
        {
            bag.IndicarValor<string>(EnumDoctorWebViewBag.NotificacionNombre, valor);
        }

        public static string NotificacionNombre(this ViewDataDictionary bag)
        {
            return bag.ObtenerValor<string>(EnumDoctorWebViewBag.NotificacionNombre);
        }

        public static void IndicarFilas(this ViewDataDictionary bag, int valor)
        {
            bag.IndicarValor<int>(EnumDoctorWebViewBag.Filas, valor);
        }

        public static List<SelectListItem> DropDowmListFilas(this ViewDataDictionary bag)
        {
            return new List<SelectListItem>(new SelectListItem[] {
                new SelectListItem { Value = "5", Text = "5", Selected = bag.Filas().ToString() == "5" },
                new SelectListItem { Value = "10", Text = "10", Selected = bag.Filas().ToString() == "10" },
                new SelectListItem { Value = "15", Text = "15", Selected = bag.Filas().ToString() == "15" },
                new SelectListItem { Value = "20", Text = "20", Selected = bag.Filas().ToString() == "20" }
            });
        }

        public static int Filas(this ViewDataDictionary bag)
        {
            return bag.ObtenerValor<int>(EnumDoctorWebViewBag.Filas);
        }

        public static void IndicarPermitirSiguiente(this ViewDataDictionary bag, bool valor)
        {
            bag.IndicarValor<bool>(EnumDoctorWebViewBag.PermitirSiguiente, valor);
        }

        public static bool PermitirSiguiente(this ViewDataDictionary bag)
        {
            return bag.ObtenerValor<bool>(EnumDoctorWebViewBag.PermitirSiguiente);
        }

        public static void IndicarPermitirAnterior(this ViewDataDictionary bag, bool valor)
        {
            bag.IndicarValor<bool>(EnumDoctorWebViewBag.PermitirAnterior, valor);
        }

        public static bool PermitirAnterior(this ViewDataDictionary bag)
        {
            return bag.ObtenerValor<bool>(EnumDoctorWebViewBag.PermitirAnterior);
        }

        public static void IndicarSiguienteIndice(this ViewDataDictionary bag, int valor)
        {
            bag.IndicarValor<int>(EnumDoctorWebViewBag.SiguienteIndice, valor);
        }

        public static int SiguienteIndice(this ViewDataDictionary bag)
        {
            return bag.ObtenerValor<int>(EnumDoctorWebViewBag.SiguienteIndice);
        }

        public static void IndicarAnteriorIndice(this ViewDataDictionary bag, int valor)
        {
            bag.IndicarValor<int>(EnumDoctorWebViewBag.AnteriorIndice, valor);
        }

        public static int AnteriorIndice(this ViewDataDictionary bag)
        {
            return bag.ObtenerValor<int>(EnumDoctorWebViewBag.AnteriorIndice);
        }

        public static void IndicarTotalPaginas(this ViewDataDictionary bag, int valor)
        {
            bag.IndicarValor<int>(EnumDoctorWebViewBag.TotalPaginas, valor);
        }

        public static int TotalPaginas(this ViewDataDictionary bag)
        {
            return bag.ObtenerValor<int>(EnumDoctorWebViewBag.TotalPaginas);
        }

        internal static void Actualizar(this Notificacion model, FormCollection collection)
        {

            if (collection.AllKeys.Contains("NotificacionId"))
                model.NotificacionId = int.Parse(collection["NotificacionId"]);
            model.Estado = (NotificacionEstado)Enum.Parse(typeof(NotificacionEstado), collection["Estado"]);
            model.Nombre = collection["Nombre"];
            model.Descripcion = collection["Descripcion"];
            model.Contenido = collection["Contenido"];
            model.Asunto = collection["Asunto"];
        }

        public static string AgregarClaseSi(this HtmlHelper bag, bool expresion, string si, string sino = "")
        {
            /*var paginaActual = bag[$"{EnumViewBagItems.PaginaActual}"];
            if (paginaActual != null && paginaActual is EnumMenuItems && ((EnumMenuItems)paginaActual) == menu)
                return true;
            return false;*/
            if (expresion)
                return si;
            return sino;
        }
        #endregion
    }
}