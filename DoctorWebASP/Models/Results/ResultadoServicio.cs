using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models.Results
{
    /// <summary>
    /// Resultado de servico que tiene un contenido del tipo que se indique.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultadoServicio<T> where T : class
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
        /// Contenido del servicio del tipo que se indique.
        /// </summary>
        public T Contenido { get; set; }

        /// <summary>
        /// Constructur base.
        /// </summary>
        public ResultadoServicio()
        {
            SinProblemas = false;
            Contenido = null;
        }

        /// <summary>
        /// Permite inicializar el resultado cuando no se presenta algun error en el proceso.
        /// </summary>
        /// <param name="Contenido">Valor a encapsular en el resultado.</param>
        internal void Inicializar(T Contenido)
        {
            this.SinProblemas = true;
            this.Contenido = Contenido;
        }
    }
}