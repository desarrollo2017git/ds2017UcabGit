using DoctorWebServiciosWCF.Controllers.Helpers;
using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.DAO;
using DoctorWebServiciosWCF.Models.Results;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Helpers
{
    /// <summary>
    /// Implementacion del patron Utilidades.Instancia.Fabrica.
    /// </summary>
    public class Fabrica: IFabrica
    {
        /// <summary>
        /// Instancia de Utilidades
        /// </summary>
        private static IFabrica instancia;
        private IUtilidades utils;

        /// <summary>
        /// Instancia de Utilidades
        /// </summary>
        public static IFabrica Instancia
        {
            get
            {
                Singleton();
                return instancia;
            }
            set { instancia = value; }
        }

        /// <summary>
        /// Definimos el constructor privado para crear mas instancias que la del singleton.
        /// </summary>
        private Fabrica(): this(Utilidades.Instancia)
        {

        }

        public Fabrica(IUtilidades utils)
        {
            this.utils = utils;
        }

        /// <summary>
        /// Inicializa la instancia si aun es nula.
        /// </summary>
        private static void Singleton()
        {
            if (instancia == null)
                instancia = new Fabrica();
        }

        /// <summary>
        /// Permite crear una instancia dao notificaciones.
        /// </summary>
        /// <returns>returna una instancia dao notificaciones.</returns>
        public INotificacionDAO CrearNotificacionDAO()
        {
            return new NotificacionDAO();
        }

        /// <summary>
        /// Utilidad para crear excepciones
        /// </summary>
        /// <param name="mensaje">Mensaje de la excepcion.</param>
        /// <param name="interna">Excepcion interna.</param>
        /// <returns>Retorna la excepcion generada.</returns>
        public Exception CrearExcepcion(string mensaje = "Excepcion no controlada.", Exception interna = null)
        {
            var encapsulado = new DoctorWebException(mensaje, interna);
            try
            {
                if (utils.ObtenerClave("GenerarLogs").Equals("true"))
                {
                    Logger logger = LogManager.GetLogger("GuardarLog");                    
                    logger.Error(encapsulado, mensaje);
                }
            }
            catch { }

            return encapsulado;
        }

        /// <summary>
        /// Permite crear una instancia dao calendarios.
        /// </summary>
        /// <returns>returna una instancia dao calendarios.</returns>
        public ICalendariosDAO CrearCalendariosDAO()
        {
            return new CalendariosDAO();
        }

        /// <summary>
        /// Permite crear una instancia resultado del servicio.
        /// </summary>
        /// <returns>returna una instancia resultado del servicio.</returns>
        public ResultadoServicio<T> CrearResultadoDe<T>()
            where T : class
        {
            return new ResultadoServicio<T>();
        }

        /// <summary>
        /// Permite crear una instancia resultado del servicio.
        /// </summary>
        /// <returns>returna una instancia resultado del servicio.</returns>
        public ResultadoProceso CrearResultadoProceso()
        {
            return new ResultadoProceso();
        }

        /// <summary>
        /// Permite crear una instancia dao centro medico.
        /// </summary>
        /// <returns>returna una instancia dao centro medico.</returns>
        public ICentroMedicoDAO CrearCentroMedicoDAO()
        {
            return new CentroMedicoDAO();
        }

        /// <summary>
        /// Permite crear una instancia resultado paginado del servicio.
        /// </summary>
        /// <returns>returna una instancia resultado paginado del servicio.</returns>
        public ResultadoServicioPaginado<T> CrearResultadoPaginadoDe<T>()
            where T : class
        {
            return new ResultadoServicioPaginado<T>();
        }

        /// <summary>
        /// Permite crear una instancia dictionario.
        /// </summary>
        /// <returns>returna una instancia dao dictionario.</returns>
        public Dictionary<T1, T2> CrearDiccionario<T1, T2>()
        {
            return new Dictionary<T1, T2>();
        }

        /// <summary>
        /// Permite crear una instancia DAO Generica sobre el modelo T indicado, esta instancia permite utilizar el CRUD del modelo T.
        /// </summary>
        /// <typeparam name="T">Modelo</typeparam>
        /// <returns>Retorna una instancia DAO Generica.</returns>
        public IDAO<T> CrearDAO<T>()
            where T : class
        {
            return new DAO<T>();
        }
    }
}