using DoctorWebASP.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoctorWebASP.Controllers.Helpers
{
    /// <summary>
    /// Funciones de Utilidad
    /// </summary>
    public class Utilidades
    {
        #region Grupo09
        
        /// <summary>
        /// Permite obtener la Url del servicio que se indique por parametro.
        /// </summary>
        /// <param name="servicio">Servicio que se quiere invocar.</param>
        /// <returns>Retorna la Uri del servicio indicado.</returns>
        internal static Uri ObtenerUrlServicioWeb(string servicio)
        {
            var host = ObtenerClave("WebServiceUrl");
            var builder = Fabrica.CrearStringBuilder(host);
            builder.Path = $"Services/{servicio}.svc/";
            return builder.Uri;
        }

        /// <summary>
        /// Permite obtener el valor asociado a una clave en el archivo de configuracion.
        /// </summary>
        /// <param name="clave">Clave asociada a un valor.</param>
        /// <returns>Retorna el valor asociado a la clave que se indica.</returns>
        internal static string ObtenerClave(string clave)
        {
            var path = ConfigurationManager.AppSettings[clave];
            if (String.IsNullOrEmpty(path))
                throw Fabrica.CrearExcepcion(mensaje: $"No se encontro un valor para '{clave}', en el archivo de configuracion.");
            return path;
        }

        /// <summary>
        /// Returna el actual contexto de la solicitud realizada.
        /// </summary>
        /// <returns>Returna el actual contexto de la solicitud realizada.</returns>
        internal static HttpContext ObtenerContextoHttp()
        {
            return HttpContext.Current;
        }
        #endregion
    }

    public class Age
    {
        public int Years;
        public int Months;
        public int Days;

        public Age(DateTime Bday)
        {
            this.Count(Bday);
        }

        public Age(DateTime Bday, DateTime Cday)
        {
            this.Count(Bday, Cday);
        }

        public Age Count(DateTime Bday)
        {
            return this.Count(Bday, DateTime.Today);
        }

        public Age Count(DateTime Bday, DateTime Cday)
        {
            if ((Cday.Year - Bday.Year) > 0 ||
                (((Cday.Year - Bday.Year) == 0) && ((Bday.Month < Cday.Month) ||
                  ((Bday.Month == Cday.Month) && (Bday.Day <= Cday.Day)))))
            {
                int DaysInBdayMonth = DateTime.DaysInMonth(Bday.Year, Bday.Month);
                int DaysRemain = Cday.Day + (DaysInBdayMonth - Bday.Day);

                if (Cday.Month > Bday.Month)
                {
                    this.Years = Cday.Year - Bday.Year;
                    this.Months = Cday.Month - (Bday.Month + 1) + Math.Abs(DaysRemain / DaysInBdayMonth);
                    this.Days = (DaysRemain % DaysInBdayMonth + DaysInBdayMonth) % DaysInBdayMonth;
                }
                else if (Cday.Month == Bday.Month)
                {
                    if (Cday.Day >= Bday.Day)
                    {
                        this.Years = Cday.Year - Bday.Year;
                        this.Months = 0;
                        this.Days = Cday.Day - Bday.Day;
                    }
                    else
                    {
                        this.Years = (Cday.Year - 1) - Bday.Year;
                        this.Months = 11;
                        this.Days = DateTime.DaysInMonth(Bday.Year, Bday.Month) - (Bday.Day - Cday.Day);
                    }
                }
                else
                {
                    this.Years = (Cday.Year - 1) - Bday.Year;
                    this.Months = Cday.Month + (11 - Bday.Month) + Math.Abs(DaysRemain / DaysInBdayMonth);
                    this.Days = (DaysRemain % DaysInBdayMonth + DaysInBdayMonth) % DaysInBdayMonth;
                }
            }
            else
            {
                throw new ArgumentException("Birthday date must be earlier than current date");
            }
            return this;
        }
    }
}