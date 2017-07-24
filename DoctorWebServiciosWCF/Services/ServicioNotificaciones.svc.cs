using System;
using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.Results;
using DoctorWebServiciosWCF.Models.DAO;
using DoctorWebServiciosWCF.Helpers;

namespace DoctorWebServiciosWCF.Services
{
    /// <summary>
    /// Clase Servicio de Notificaciones, una instancia representa los servicios web.
    /// </summary>
    public class ServicioNotificaciones : IServicioNotificaciones
    {
        /// <summary>
        /// Instancia dao para interactuar con la Base de datos.
        /// </summary>
        private INotificacionDAO dao = null;
        private IUtilidades utils = null;

        /// <summary>
        /// Constructor Base
        /// </summary>
        public ServicioNotificaciones() : this(Utilidades.Instancia.Fabrica.CrearNotificacionDAO(), Utilidades.Instancia) { }
        
        /// <summary>
        /// Constructor que permite cambiar el dao utilizado por el servicio.
        /// </summary>
        /// <param name="notificacionDAO">Instrancia de dao a utilizar en el servicio.</param>
        public ServicioNotificaciones(INotificacionDAO notificacionDAO, IUtilidades utils)
        {
            this.dao = notificacionDAO;
            this.utils = utils;
        }

        /// <summary>
        /// Este metodo permite borrarla notificacion que conindicide con el codigo indicado.
        /// </summary>
        /// <param name="codigo">Codigo de notificacion a borrar</param>
        /// <returns>Indica el resultado del proceso</returns>
        public ResultadoProceso Borrar(string codigo)
        {
            var resultado = utils.Fabrica.CrearResultadoProceso();
            try
            {
                int id = 0;
                if (!int.TryParse(codigo, out id))
                    throw utils.Fabrica.CrearExcepcion(mensaje: "el codigo debe ser un numero.");

                string mensaje = string.Empty;
                dao.Borrar(out mensaje, id);
                resultado.Inicializar(mensaje);
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        /// <summary>
        /// Este metodo permite ejecutar una notificacion e indicar el contenido a travez de la cabecera de la solucitud.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="correo"></param>
        /// <returns>Indica el resultado del proceso</returns>
        public ResultadoProceso Enviar(string nombre, string correo)
        {
            var resultado = utils.Fabrica.CrearResultadoProceso();
            try
            {
                var notificacion = dao.Obtener(nombre);
                
                var parametros = utils.Fabrica.CrearDiccionario<string, object>();

                var cabecera = utils.ObtenerCabeceraActual();

                foreach (var key in cabecera.AllKeys)
                {
                    parametros.Add(key, cabecera[key]);
                }

                notificacion.Enviar(correo, parametros);
                resultado.SinProblemas = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        /// <summary>
        /// Este metodo permite guardar los cambios de la notificacion que se indica.
        /// </summary>
        /// <param name="notificacion">Notificacion a guardar</param>
        /// <returns>Indica el resultado del proceso</returns>
        public ResultadoProceso Guardar(Notificacion notificacion)
        {
            var resultado = utils.Fabrica.CrearResultadoProceso();
            try
            {
                string mensaje = string.Empty;
                dao.Guardar(notificacion);
                resultado.Inicializar(mensaje);
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        /// <summary>
        /// Este metodo permite obtener una notificacion a partir del codigo que se le indique.
        /// </summary>
        /// <param name="codigo">Codigo de la notificacion.</param>
        /// <returns>Indica el resultado del proceso</returns>
        public ResultadoServicio<Notificacion> Obtener(string codigo)
        {
            var resultado = utils.Fabrica.CrearResultadoDe<Notificacion>();
            try
            {
                int id = 0;
                if (!int.TryParse(codigo, out id))
                    throw utils.Fabrica.CrearExcepcion(mensaje: "el codigo debe ser un numero.");

                var datos = dao.Obtener(id);
                resultado.Inicializar(datos);
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        /// <summary>
        /// Este medoto permite obtener las notificaciones paginando segun se indique y filtrando por el nombre si es necesario.
        /// </summary>
        /// <param name="nombre">Permite filtrar los datos usando el nombre.</param>
        /// <param name="pagina">Numero de pagina que se esta solicitando.</param>
        /// <param name="cantidadFilas">Cantidad de registros por pagina.</param>
        /// <returns>Indica el resultado del proceso</returns>
        public ResultadoServicioPaginado<Notificacion> ObtenerTodos(string nombre, int pagina = 0, int numeroFilas = 30)
        {
            var resultado = utils.Fabrica.CrearResultadoPaginadoDe<Notificacion>();
            try
            {
                int cantidadPaginas;
                var datos = dao.ObtenerTodos(out cantidadPaginas, nombre, pagina, numeroFilas);
                resultado.Inicializar(pagina, numeroFilas, cantidadPaginas, datos);
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
