using DoctorWebServiciosWCF.Models.DAO;
using DoctorWebServiciosWCF.Models.Results;
using System;
using System.Collections.Generic;

namespace DoctorWebServiciosWCF.Helpers
{
    public interface IFabrica
    {

        /// <summary>
        /// Permite crear una instancia dao notificaciones.
        /// </summary>
        /// <returns>returna una instancia dao notificaciones.</returns>
        INotificacionDAO CrearNotificacionDAO();

        /// <summary>
        /// Utilidad para crear excepciones
        /// </summary>
        /// <param name="mensaje">Mensaje de la excepcion.</param>
        /// <param name="interna">Excepcion interna.</param>
        /// <returns>Retorna la excepcion generada.</returns>
        Exception CrearExcepcion(string mensaje = "Excepcion no controlada.", Exception interna = null);

        /// <summary>
        /// Permite crear una instancia dao calendarios.
        /// </summary>
        /// <returns>returna una instancia dao calendarios.</returns>
        ICalendariosDAO CrearCalendariosDAO();


        /// <summary>
        /// Permite crear una instancia dao calendarios.
        /// </summary>
        /// <returns>returna una instancia dao calendarios.</returns>
        IReporteDAO CrearReporteDAO();

        ResultadoServicio<T> CrearResultadoDe<T>()
            where T : class;

        ResultadoProceso CrearResultadoProceso();

        ICentroMedicoDAO CrearCentroMedicoDAO();

        IPacienteDAO CrearPacienteDAO();

        ResultadoServicioPaginado<T> CrearResultadoPaginadoDe<T>()
            where T : class;

        Dictionary<T1, T2> CrearDiccionario<T1, T2>();

        IDAO<T> CrearDAO<T>()
            where T : class;
    }
}