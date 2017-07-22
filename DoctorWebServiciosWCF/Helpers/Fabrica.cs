using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.DAO;
using DoctorWebServiciosWCF.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Helpers
{
    /// <summary>
    /// Implementacion del patron fabrica.
    /// </summary>
    public class Fabrica
    {
        /// <summary>
        /// Permite crear una instancia dao notificaciones.
        /// </summary>
        /// <returns>returna una instancia dao notificaciones.</returns>
        internal static INotificacionDAO CrearNotificacionDAO()
        {
            return new NotificacionDAO();
        }

        /// <summary>
        /// Utilidad para crear excepciones
        /// </summary>
        /// <param name="mensaje">Mensaje de la excepcion.</param>
        /// <param name="interna">Excepcion interna.</param>
        /// <returns>Retorna la excepcion generada.</returns>
        public static Exception CrearExcepcion(string mensaje = "Excepcion no controlada.", Exception interna = null)
        {
            return new DoctorWebException(mensaje, interna);
        }

        /// <summary>
        /// Permite crear una instancia dao calendarios.
        /// </summary>
        /// <returns>returna una instancia dao calendarios.</returns>
        internal static ICalendariosDAO CrearCalendariosDAO()
        {
            return new CalendariosDAO();
        }

        /// <summary>
        /// Permite crear una instancia resultado del servicio.
        /// </summary>
        /// <returns>returna una instancia resultado del servicio.</returns>
        internal static ResultadoServicio<T> CrearResultadoDe<T>()
            where T : class
        {
            return new ResultadoServicio<T>();
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
        /// Permite crear una instancia dao centro medico.
        /// </summary>
        /// <returns>returna una instancia dao centro medico.</returns>
        internal static ICentroMedicoDAO CrearCentroMedicoDAO()
        {
            return new CentroMedicoDAO();
        }

        /// <summary>
        /// Permite crear una instancia resultado paginado del servicio.
        /// </summary>
        /// <returns>returna una instancia resultado paginado del servicio.</returns>
        internal static ResultadoServicioPaginado<T> CrearResultadoPaginadoDe<T>()
            where T : class
        {
            return new ResultadoServicioPaginado<T>();
        }

        /// <summary>
        /// Permite crear una instancia dictionario.
        /// </summary>
        /// <returns>returna una instancia dao dictionario.</returns>
        internal static Dictionary<T1, T2> CrearDiccionario<T1, T2>()
        {
            return new Dictionary<T1, T2>();
        }

        /// <summary>
        /// Permite crear una instancia DAO Generica sobre el modelo T indicado, esta instancia permite utilizar el CRUD del modelo T.
        /// </summary>
        /// <typeparam name="T">Modelo</typeparam>
        /// <returns>Retorna una instancia DAO Generica.</returns>
        internal static IDAO<T> CrearDAO<T>()
            where T : class
        {
            return new DAO<T>();
        }
    }
}