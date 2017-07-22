using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models.Results
{
    /// <summary>
    /// Clase que representa el resultado de un servicio.
    /// </summary>
    public class ResultadoProceso
    {
        /// <summary>
        /// Indica si se genero o no un error.
        /// </summary>
        public bool SinProblemas { get; set; }

        /// <summary>
        /// Indica en caso de error algun mensaje.
        /// </summary>
        public string Mensaje { get; set; }

        /// <summary>
        /// Constructur base.
        /// </summary>
        public ResultadoProceso()
        {
            SinProblemas = false;
            Mensaje = null;
        }

        /// <summary>
        /// Permite inicializar el resultado cuando no se presenta algun error en el proceso.
        /// </summary>
        /// <param name="mensaje"></param>
        internal void Inicializar(string mensaje)
        {
            this.SinProblemas = true;
            this.Mensaje = mensaje;
        }
    }
}