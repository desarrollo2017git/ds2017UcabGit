using DoctorWebServiciosWCF.Helpers;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.ServiceModel.Web;

namespace DoctorWebServiciosWCF.Controllers.Helpers
{
    /// <summary>
    /// Aqui se definen un conjunto de metodos que son de ayuda para reciclar una tarea.
    /// </summary>
    public class Utilidades : IUtilidades
    {
        /// <summary>
        /// Instancia de Utilidades
        /// </summary>
        private static Utilidades instancia;

        /// <summary>
        /// Instancia de Utilidades
        /// </summary>
        public static Utilidades Instancia
        {
            get {
                SingletonUtilidad();
                return instancia;
            }
            private set { instancia = value; }
        }

        /// <summary>
        /// Definimos el constructor privado para crear mas instancias que la del singleton.
        /// </summary>
        private Utilidades() {

        }

        /// <summary>
        /// Inicializa la instancia si aun es nula.
        /// </summary>
        private static void SingletonUtilidad()
        {
            if (instancia == null)
                instancia = new Utilidades();
        }

        /// <summary>
        /// Inicializa la instancia si aun es nula.
        /// </summary>
        private static void SingletonFabrica()
        {
            if (fabrica == null)
                fabrica = new Fabrica(Instancia);
        }

        /// <summary>
        /// Instancia de Fabrica
        /// </summary>
        private static Fabrica fabrica;

        /// <summary>
        /// Instancia de Fabrica
        /// </summary>
        public IFabrica Fabrica {
            get
            {
                SingletonFabrica();
                return fabrica;
            }
        }
        

        /// <summary>
        /// Permite obtener informacion del archivo de configuracion.
        /// </summary>
        /// <param name="key">Clave necesaria para buscar su valor asociado.</param>
        /// <returns>Valor segun la clave.</returns>
        public string ObtenerClave(string key)
        {
            var path = ConfigurationManager.AppSettings[key];
            if (String.IsNullOrEmpty(path))
                throw new KeyNotFoundException(String.Format("No se encuentro la clave ({0}) en el archivo de configuracion.", key));
            return path;
        }

        /// <summary>
        /// Generar mensaje Debug.
        /// </summary>
        /// <param name="mensaje">Mensaje a guardar.</param>
        public void Debug(string mensaje)
        {
            try
            {
                if (ObtenerClave("GenerarDebugs").Equals("true"))
                {
                    Logger logger = LogManager.GetLogger("GuardarDebug");
                    logger.Debug(mensaje);
                }
            }
            catch { }
        }

        /// <summary>
        /// Obtiene la cabecera actual.
        /// </summary>
        /// <returns>Returna la cabecera actual.</returns>
        public NameValueCollection ObtenerCabeceraActual()
        {
            IncomingWebRequestContext request = WebOperationContext.Current.IncomingRequest;
            return request.Headers;

        }
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