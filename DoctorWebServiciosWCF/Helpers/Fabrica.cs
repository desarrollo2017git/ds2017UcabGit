using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Helpers
{
    public class Fabrica
    {
        internal static INotificacionDAO CrearNotificacionDAO()
        {
            return new NotificacionDAO();
        }

        public static Exception CrearExcepcion(string mensaje = "Excepcion no controlada.", Exception interna = null)
        {
            return new DoctorWebException(mensaje, interna);
        }

        internal static ICalendariosDAO CrearCalendariosDAO()
        {
            return new CalendariosDAO();
        }
    }
}