using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoctorWebServiciosWCF.Models;

namespace DoctorWebServiciosWCF.Models.Results
{
    /// <summary>
    /// Permite encapsular la informacion resultante de un servicio paginando los datos.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultadoServicioPaginado<T> where T : class
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
        /// Indica la pagina que se solicito.
        /// </summary>
        public int PaginaActual { get; set; }

        /// <summary>
        /// Indica la cantidad de registros por pagina.
        /// </summary>
        public int CantidadFilas { get; set; }

        /// <summary>
        /// Indica la cantidad de paginas segun las filas.
        /// </summary>
        public int CantidadPaginas { get; set; }

        /// <summary>
        /// Contenido del servicio del tipo que se indique.
        /// </summary>
        public IEnumerable<T> Contenido { get; set; }

        /// <summary>
        /// Constructur base.
        /// </summary>
        public ResultadoServicioPaginado() {
            SinProblemas = false;
            Contenido = null;
        }

        /// <summary>
        /// Permite inicializar el resultado cuando no se presenta algun error en el proceso.
        /// </summary>
        /// <param name="PaginaActual">Pagina actual solicitada.</param>
        /// <param name="CantidadFilas">Cantidad de filas por pagina.</param>
        /// <param name="CantidadPaginas">Cantidad de paginas.</param>
        /// <param name="Contenido">Valores a encapsular en el resultado.</param>
        internal void Inicializar(int PaginaActual, int CantidadFilas, int CantidadPaginas, List<T> Contenido)
        {
            this.SinProblemas = true;
            this.PaginaActual = PaginaActual;
            this.CantidadFilas = CantidadFilas;
            this.CantidadPaginas = CantidadPaginas;
            this.Contenido = Contenido;
        }
    }
}