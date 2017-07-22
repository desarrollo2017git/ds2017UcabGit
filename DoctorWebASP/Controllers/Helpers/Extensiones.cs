using DoctorWebASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWebASP.Controllers.Helpers
{
    /// <summary>
    /// En esta clase se define las extensiones a las clases que ya estan definidas.
    /// </summary>
    public static class Extensiones
    {
        #region Extension : Session
        /// <summary>
        /// Primitiva para comprobar si existe un valor para la clave indicada en la variable de sesion.
        /// </summary>
        /// <typeparam name="T">Tipo de dato a esperado del valor.</typeparam>
        /// <param name="session">Instancia de session a quien se le hace la extencion.</param>
        /// <param name="tag">Clave utilizada como identificador.</param>
        /// <param name="value">Valor a comparar en caso de que si tenga informacion previa.</param>
        /// <returns>Respuesta tras comparar.</returns>
        private static bool HayValor<T>(this HttpSessionStateBase session, EnumDoctorWebSession tag, T value)
        {
            var currentValue = (T)session[tag.ToString()];
            if (currentValue != null && currentValue is T && currentValue.Equals(value))
                return true;
            return false;
        }

        /// <summary>
        /// Permite colocar el valor en la session con la clave indicada.
        /// </summary>
        /// <typeparam name="T">Tipo de dato a esperado del valor.</typeparam>
        /// <param name="session">Instancia de session a quien se le hace la extencion.</param>
        /// <param name="tag">Clave utilizada como identificador.</param>
        /// <param name="value">Valor a guardar.</param>
        private static void IndicarValor<T>(this HttpSessionStateBase session, EnumDoctorWebSession tag, T value)
        {
            session[tag.ToString()] = value;
        }

        /// <summary>
        /// Permite obtener el valor almacenado.
        /// </summary>
        /// <typeparam name="T">Tipo de dato a esperado del valor.</typeparam>
        /// <param name="session">Instancia de session a quien se le hace la extencion.</param>
        /// <param name="tag">Clave utilizada como identificador.</param>
        /// <returns>Valor almacenado o valor por defecto.</returns>
        private static T ObtenerValor<T>(this HttpSessionStateBase session, EnumDoctorWebSession tag)
        {
            object valor = session[tag.ToString()];
            if (valor != null && valor is T)
                return (T)valor;
            return default(T);
        }

        /// <summary>
        /// Permite comprobar si hay notificaciones.
        /// </summary>
        /// <param name="session">Instancia de session a quien se le hace la extencion.</param>
        /// <returns>Respuesta tras comparar.</returns>
        public static bool HayNotificacion(this HttpSessionStateBase session)
        {
            return !String.IsNullOrEmpty(session.ObtenerValor<string>(EnumDoctorWebSession.Notificacion));
        }

        /// <summary>
        /// Obtiene el mensaje o notificacion almacenado.
        /// </summary>
        /// <param name="session">Instancia de session a quien se le hace la extencion.</param>
        /// <returns>Retorna el mensaje o notificacion.</returns>
        public static string Notificacion(this HttpSessionStateBase session)
        {
            return session.ObtenerValor<string>(EnumDoctorWebSession.Notificacion);
        }

        /// <summary>
        /// Obtiene el tipo de notificacion asociado al mensaje.
        /// </summary>
        /// <param name="session">Instancia de session a quien se le hace la extencion.</param>
        /// <returns>Retorna el tipo de notificacion.</returns>
        public static string TipoNotificacion(this HttpSessionStateBase session)
        {
            return session.ObtenerValor<string>(EnumDoctorWebSession.TipoNotificacion);
        }

        /// <summary>
        /// Permite guardar la notificacion en las variables de sesion.
        /// </summary>
        /// <param name="session">Instancia de session a quien se le hace la extencion.</param>
        /// <param name="value">Mensaje o Notificacion a guardar.</param>
        /// <param name="tipo">Tipo de notificacion.</param>
        public static void IndicarNotificacion(this HttpSessionStateBase session, string value, EnumDoctorWebTipoNotificacion tipo)
        {
            session.IndicarValor(EnumDoctorWebSession.Notificacion, value);
            session.IndicarValor(EnumDoctorWebSession.TipoNotificacion, tipo.ToString());
        }
        #endregion

        #region Extension : ViewDataDictionary
        /// <summary>
        /// Primitiva para comprobar si existe un valor para la clave indicada en la variable de datos de vista.
        /// </summary>
        /// <typeparam name="T">Tipo de dato a esperado del valor.</typeparam>
        /// <param name="bag">Instancia de datos de vista a quien se le hace la extencion.</param>
        /// <param name="tag">Clave utilizada como identificador.</param>
        /// <param name="value">Valor a comparar en caso de que si tenga informacion previa.</param>
        /// <returns>Respuesta tras comparar.</returns>
        private static bool HayValor<T>(this ViewDataDictionary bag, EnumDoctorWebViewBag tag, T value)
        {
            var currentValue = (T)bag[tag.ToString()];
            if (currentValue != null && currentValue is T && currentValue.Equals(value))
                return true;
            return false;
        }

        /// <summary>
        /// Permite colocar el valor en la variable datos de vista con la clave indicada.
        /// </summary>
        /// <typeparam name="T">Tipo de dato a esperado del valor.</typeparam>
        /// <param name="bag">Instancia de datos de vista a quien se le hace la extencion.</param>
        /// <param name="tag">Clave utilizada como identificador.</param>
        /// <param name="value">Valor a guardar.</param>
        private static void IndicarValor<T>(this ViewDataDictionary bag, EnumDoctorWebViewBag tag, T value)
        {
            bag[tag.ToString()] = value;
        }

        /// <summary>
        /// Permite obtener el valor almacenado.
        /// </summary>
        /// <typeparam name="T">Tipo de dato a esperado del valor.</typeparam>
        /// <param name="bag">Instancia de datos de vista a quien se le hace la extencion.</param>
        /// <param name="tag">Clave utilizada como identificador.</param>
        /// <returns>Valor almacenado o valor por defecto.</returns>
        public static T ObtenerValor<T>(this ViewDataDictionary bag, EnumDoctorWebViewBag tag)            
        {
            object valor = bag[tag.ToString()];
            if (valor != null && valor is T)
                return (T)valor;
            return default(T);
        }

        /// <summary>
        /// Permite comprobar si hay notificaciones.
        /// </summary>
        /// <param name="bag">Instancia de datos de vista a quien se le hace la extencion.</param>
        /// <returns>Respuesta tras comparar.</returns>
        public static bool HayNotificacion(this ViewDataDictionary bag)
        {
            var valor = Utilidades.ObtenerContextoHttp().Session[EnumDoctorWebSession.Notificacion.ToString()];
            if (valor == null || !(valor is string))
                return false;
            return !String.IsNullOrEmpty((string)valor);
        }

        /// <summary>
        /// Obtiene el mensaje o notificacion almacenado.
        /// </summary>
        /// <param name="bag">Instancia de datos de vista a quien se le hace la extencion.</param>
        /// <returns>Retorna el mensaje o notificacion.</returns>
        public static string Notificacion(this ViewDataDictionary bag)
        {
            var valor = Utilidades.ObtenerContextoHttp().Session[EnumDoctorWebSession.Notificacion.ToString()];
            if (valor == null || !(valor is string))
                return null;
            Utilidades.ObtenerContextoHttp().Session[EnumDoctorWebSession.Notificacion.ToString()] = null;
            return (string)valor;
        }

        /// <summary>
        /// Obtiene el tipo de notificacion asociado al mensaje.
        /// </summary>
        /// <param name="bag">Instancia de datos de vista a quien se le hace la extencion.</param>
        /// <returns>Retorna el tipo de notificacion.</returns>
        public static string TipoNotificacion(this ViewDataDictionary bag)
        {
            var valor = Utilidades.ObtenerContextoHttp().Session[EnumDoctorWebSession.TipoNotificacion.ToString()];
            if (valor == null || !(valor is string))
                return null;
            Utilidades.ObtenerContextoHttp().Session[EnumDoctorWebSession.TipoNotificacion.ToString()] = null;
            return (string)valor;
        }

        /// <summary>
        /// Permite colocar la pagina activa en el menu.
        /// </summary>
        /// <param name="bag">Instancia de datos de vista a quien se le hace la extencion.</param>
        /// <param name="menu">Representa el identificador de la pagina actual.</param>
        public static void IndicarPaginaActual(this ViewDataDictionary bag, EnumDoctorWebPagina menu)
        {
            bag.IndicarValor(EnumDoctorWebViewBag.PaginaActual, menu);
        }

        /// <summary>
        /// Permite comparar la pagina actual con una indicada.
        /// </summary>
        /// <param name="bag">Instancia de datos de vista a quien se le hace la extencion.</param>
        /// <param name="menu">Representa el identificador de la pagina actual.</param>
        /// <returns>Retorna el resultado de comparar la pagina guardada con la recibida por parametros.</returns>
        public static bool EsPaginaActual(this ViewDataDictionary bag, EnumDoctorWebPagina menu)
        {
            var paginaActual = bag.ObtenerValor<EnumDoctorWebPagina>(EnumDoctorWebViewBag.PaginaActual);
            if (paginaActual == menu)
                return true;
            return false;
        }
        #endregion

        #region Grupo09
        /// <summary>
        /// Retorna el combo de estado de notificaciones.
        /// </summary>
        /// <param name="bag">Instancia de datos de vista a quien se le hace la extencion.</param>
        /// <param name="model">Instancia para comlocar el valor actual.</param>
        /// <returns>Returna los datos necesarios para llenar un DropDownList.</returns>
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

        /// <summary>
        /// Colocar filtro para las notificaciones.
        /// </summary>
        /// <param name="bag">Instancia de datos de vista a quien se le hace la extencion.</param>
        /// <param name="valor">Nombre para filtrar.</param>
        public static void IndicarNotificacionNombre(this ViewDataDictionary bag, string valor)
        {
            bag.IndicarValor(EnumDoctorWebViewBag.NotificacionNombre, valor);
        }

        /// <summary>
        /// Permite obtener el filtro utilizado con las notificaciones.
        /// </summary>
        /// <param name="bag">Instancia de datos de vista a quien se le hace la extencion.</param>
        /// <returns>Retorna el Nombre usado para filtrar.</returns>
        public static string NotificacionNombre(this ViewDataDictionary bag)
        {
            return bag.ObtenerValor<string>(EnumDoctorWebViewBag.NotificacionNombre);
        }

        /// <summary>
        /// Permite guardar el numero de filas usado para paginar.
        /// </summary>
        /// <param name="bag">Instancia de datos de vista a quien se le hace la extencion.</param>
        /// <param name="valor">Es el valor a guardar en los datos de vista.</param>
        public static void IndicarFilas(this ViewDataDictionary bag, int valor)
        {
            bag.IndicarValor(EnumDoctorWebViewBag.Filas, valor);
        }

        /// <summary>
        /// Obtiene la lista de elementos usadar en el filtro por fila.
        /// </summary>
        /// <param name="bag">Instancia de datos de vista a quien se le hace la extencion.</param>
        /// <returns>Obtiene la lista de elementos usadar en el filtro por fila.</returns>
        public static List<SelectListItem> DropDowmListFilas(this ViewDataDictionary bag)
        {
            return new List<SelectListItem>(new SelectListItem[] {
                new SelectListItem { Value = "5", Text = "5", Selected = bag.Filas().ToString() == "5" },
                new SelectListItem { Value = "10", Text = "10", Selected = bag.Filas().ToString() == "10" },
                new SelectListItem { Value = "15", Text = "15", Selected = bag.Filas().ToString() == "15" },
                new SelectListItem { Value = "20", Text = "20", Selected = bag.Filas().ToString() == "20" }
            });
        }
        /// <summary>
        /// Obtiene el numero de filas utilizado para filtrar.
        /// </summary>
        /// <param name="bag">Instancia de datos de vista a quien se le hace la extencion.</param>
        /// <returns>Obtiene el numero de filas utilizado para filtrar.</returns>
        public static int Filas(this ViewDataDictionary bag)
        {
            return bag.ObtenerValor<int>(EnumDoctorWebViewBag.Filas);
        }

        /// <summary>
        /// Guarda la respuesta sobre si se puede paginar hacia adelante o no.
        /// </summary>
        /// <param name="bag">Instancia de datos de vista a quien se le hace la extencion.</param>
        /// <param name="valor">Es el valor a guardar en los datos de vista.</param>
        public static void IndicarPermitirSiguiente(this ViewDataDictionary bag, bool valor)
        {
            bag.IndicarValor(EnumDoctorWebViewBag.PermitirSiguiente, valor);
        }
        
        /// <summary>
        /// Permite saber si es posible paginar hacea adelante.
        /// </summary>
        /// <param name="bag">Instancia de datos de vista a quien se le hace la extencion.</param>
        /// <returns>Returna la respuesta de si se puede o no paginar hacia adelante.</returns>
        public static bool PermitirSiguiente(this ViewDataDictionary bag)
        {
            return bag.ObtenerValor<bool>(EnumDoctorWebViewBag.PermitirSiguiente);
        }

        /// <summary>
        /// Guarda la respuesta sobre si se puede paginar hacia atras o no.
        /// </summary>
        /// <param name="bag">Instancia de datos de vista a quien se le hace la extencion.</param>
        /// <param name="valor">Es el valor a guardar en los datos de vista.</param>
        public static void IndicarPermitirAnterior(this ViewDataDictionary bag, bool valor)
        {
            bag.IndicarValor(EnumDoctorWebViewBag.PermitirAnterior, valor);
        }

        /// <summary>
        /// Permite saber si es posible paginar hacea atras.
        /// </summary>
        /// <param name="bag">Instancia de datos de vista a quien se le hace la extencion.</param>
        /// <returns>Returna la respuesta de si se puede o no paginar hacia atras.</returns>
        public static bool PermitirAnterior(this ViewDataDictionary bag)
        {
            return bag.ObtenerValor<bool>(EnumDoctorWebViewBag.PermitirAnterior);
        }

        /// <summary>
        /// Guarda el siguiente indice.
        /// </summary>
        /// <param name="bag">Instancia de datos de vista a quien se le hace la extencion.</param>
        /// <param name="valor">Es el valor a guardar en los datos de vista.</param>
        public static void IndicarSiguienteIndice(this ViewDataDictionary bag, int valor)
        {
            bag.IndicarValor(EnumDoctorWebViewBag.SiguienteIndice, valor);
        }

        /// <summary>
        /// Retorna el siguiente indice.
        /// </summary>
        /// <param name="bag">Instancia de datos de vista a quien se le hace la extencion.</param>
        /// <returns>Retorna el siguiente indice.</returns>
        public static int SiguienteIndice(this ViewDataDictionary bag)
        {
            return bag.ObtenerValor<int>(EnumDoctorWebViewBag.SiguienteIndice);
        }

        /// <summary>
        ///  Guarda el indice anterior.
        /// </summary>
        /// <param name="bag">Instancia de datos de vista a quien se le hace la extencion.</param>
        /// <param name="valor">Es el valor a guardar en los datos de vista.</param>
        public static void IndicarAnteriorIndice(this ViewDataDictionary bag, int valor)
        {
            bag.IndicarValor(EnumDoctorWebViewBag.AnteriorIndice, valor);
        }

        /// <summary>
        /// Obtiene el indice anterior.
        /// </summary>
        /// <param name="bag">Instancia de datos de vista a quien se le hace la extencion.</param>
        /// <returns>Obtiene el indice anterior.</returns>
        public static int AnteriorIndice(this ViewDataDictionary bag)
        {
            return bag.ObtenerValor<int>(EnumDoctorWebViewBag.AnteriorIndice);
        }

        /// <summary>
        /// Guarda la cantidad total de paginas.
        /// </summary>
        /// <param name="bag">Instancia de datos de vista a quien se le hace la extencion.</param>
        /// <param name="valor">Es el valor a guardar en los datos de vista.</param>
        public static void IndicarTotalPaginas(this ViewDataDictionary bag, int valor)
        {
            bag.IndicarValor(EnumDoctorWebViewBag.TotalPaginas, valor);
        }

        /// <summary>
        /// Obtiene la cantidad total de paginas.
        /// </summary>
        /// <param name="bag">Instancia de datos de vista a quien se le hace la extencion.</param>
        /// <returns>Obtiene la cantidad total de paginas.</returns>
        public static int TotalPaginas(this ViewDataDictionary bag)
        {
            return bag.ObtenerValor<int>(EnumDoctorWebViewBag.TotalPaginas);
        }

        /// <summary>
        /// Permite actualiza una instancia Notificacion con la informacion capturada en el formulario.
        /// </summary>
        /// <param name="model">Instancia a actualizar.</param>
        /// <param name="collection">Datos del formulario.</param>
        internal static void Actualizar(this Notificacion model, FormCollection collection)
        {
            if (!collection.AllKeys.Contains("NotificacionId") || string.IsNullOrEmpty(collection["NotificacionId"]))
                throw Fabrica.CrearExcepcion(mensaje: "Es necesario indicar Id de Notificacion.");
            model.NotificacionId = int.Parse(collection["NotificacionId"]);

            if (!collection.AllKeys.Contains("Estado") || string.IsNullOrEmpty(collection["Estado"]))
                throw Fabrica.CrearExcepcion(mensaje: "Es necesario indicar Estado de Notificacion.");
            model.Estado = (NotificacionEstado)Enum.Parse(typeof(NotificacionEstado), collection["Estado"]);

            if (!collection.AllKeys.Contains("Nombre") || string.IsNullOrEmpty(collection["Nombre"]))
                throw Fabrica.CrearExcepcion(mensaje: "Es necesario indicar Nombre de Notificacion.");
            model.Nombre = collection["Nombre"];

            if (!collection.AllKeys.Contains("Descripcion") || string.IsNullOrEmpty(collection["Descripcion"]))
                throw Fabrica.CrearExcepcion(mensaje: "Es necesario indicar Descripcion de Notificacion.");
            model.Descripcion = collection["Descripcion"];

            if (!collection.AllKeys.Contains("Contenido") || string.IsNullOrEmpty(collection["Contenido"]))
                throw Fabrica.CrearExcepcion(mensaje: "Es necesario indicar Contenido de Notificacion.");
            model.Contenido = collection["Contenido"];

            if (!collection.AllKeys.Contains("Asunto") || string.IsNullOrEmpty(collection["Asunto"]))
                throw Fabrica.CrearExcepcion(mensaje: "Es necesario indicar Asunto de Notificacion.");
            model.Asunto = collection["Asunto"];
        }

        /// <summary>
        /// Permite comparar y segun sea la respuesta retornar una cadena u otra.
        /// </summary>
        /// <param name="bag">Instancia de datos de vista a quien se le hace la extencion.</param>
        /// <param name="expresion">resultado tras comparar.</param>
        /// <param name="si">En caso de ser afirmativo.</param>
        /// <param name="sino">En caso de ser negativo.</param>
        /// <returns>Retorna la cadena segun la expresion.</returns>
        public static string AgregarClaseSi(this HtmlHelper bag, bool expresion, string si, string sino = "")
        {
            if (expresion)
                return si;
            return sino;
        }
        #endregion
    }
}