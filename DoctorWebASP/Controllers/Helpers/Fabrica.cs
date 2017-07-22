using DoctorWebASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoctorWebASP.Models.Services;
using DoctorWebASP.Models.Results;

namespace DoctorWebASP.Controllers.Helpers
{
    /// <summary>
    /// Implementacion del Patron Fabrica.
    /// </summary>
    public class Fabrica
    {
        /// <summary>
        /// Utilidad para crear excepciones
        /// </summary>
        /// <param name="mensaje">Mensaje de la excepcion.</param>
        /// <param name="interna">Excepcion interna.</param>
        /// <returns>Retorna la excepcion generada.</returns>
        internal static Exception CrearExcepcion(string mensaje = "Excepcion no controlada.", Exception interna = null)
        {
            return new DoctorWebException(mensaje, interna);
        }

        /// <summary>
        /// Permite crear una instancia resultado del servicio.
        /// </summary>
        /// <returns>returna una instancia resultado del servicio.</returns>
        internal static ResultadoProceso CrearResultadoProceso()
        {
            return new ResultadoProceso();
        }

        /// <summary>
        /// Permite crear una instancia de servicios notificaciones.
        /// </summary>
        /// <returns>Retorna la instancia para consumir los servicios</returns>
        internal static IServicioNotificaciones CrearServicioNotificaciones()
        {
            return new ServicioNotificaciones();
        }

        /// <summary>
        /// Permite crear una instancia de servicios reportes.
        /// </summary>
        /// <returns>Retorna la instancia para consumir los servicios</returns>
        internal static IServicioReportes CrearServicioReportes()
        {
            return new ServicioReportes();
        }

        /// <summary>
        /// Permite crear una lista vacia de notificaciones.
        /// </summary>
        /// <returns>Retorna la lista de notificaciones.</returns>
        internal static List<Notificacion> CrearListaNotificaciones()
        {
            return new List<Notificacion>();
        }

        /// <summary>
        /// Permite crear una notificacion.
        /// </summary>
        /// <returns>retorna la notificacion creada.</returns>
        internal static Notificacion CrearNotificaciones()
        {
            return new Notificacion();
        }

        /// <summary>
        /// Permite crear un constructur de url.
        /// </summary>
        /// <param name="host">Url para base.</param>
        /// <returns>retorna el constructor inicializado.</returns>
        internal static UriBuilder CrearStringBuilder(string host)
        {
            return new UriBuilder(host);
        }
    }
}