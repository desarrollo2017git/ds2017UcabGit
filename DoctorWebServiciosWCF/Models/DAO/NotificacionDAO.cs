using DoctorWebServiciosWCF.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoctorWebServiciosWCF.Models.DAO
{
    /// <summary>
    /// Clase DAO de Notificaciones, una instancia permite interactuar con la Base de Datos.
    /// </summary>
    public class NotificacionDAO : DAO<Notificacion>, INotificacionDAO
    {
        /// <summary>
        /// Inicializa la una instancia NotificacionDAO con la coleccion adecuada.
        /// </summary>
        public NotificacionDAO():base()
        {
            coleccion = db.Notificaciones;
        }

        /// <summary>
        /// Este medoto permite obtener las notificaciones paginando segun se indique y filtrando por el nombre si es necesario.
        /// </summary>
        /// <param name="cantidadPaginas">Cantidad de paginas segun la cantidad de filas.</param>
        /// <param name="nombre">Permite filtrar los datos usando el nombre.</param>
        /// <param name="pagina">Numero de pagina que se esta solicitando.</param>
        /// <param name="cantidadFilas">Cantidad de registros por pagina.</param>
        /// <returns>Lista de notificaciones, puede estar vacia en caso de no encontrar.</returns>
        public List<Notificacion> ObtenerTodos(out int cantidadPaginas, string nombre = null, int pagina = 0, int numeroFilas = 30)
        {
            try
            {
                var cantidadRegistros = Contar();
                cantidadPaginas = (int)Math.Ceiling(cantidadRegistros / (double)numeroFilas);

                IQueryable<Notificacion> consulta = ObtenerTodos()
                    .OrderBy(notificacion => notificacion.Nombre);

                if (!String.IsNullOrEmpty(nombre))
                    consulta = consulta.Where(notificaion => notificaion.Nombre.Contains(nombre));

                var notificaciones = consulta.Skip(pagina * numeroFilas)
                    .Take(numeroFilas).ToList();
         
                return notificaciones;                
            }
            catch (DoctorWebException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw Fabrica.CrearExcepcion(interna: e);
            }
        }

        /// <summary>
        /// Este metodo permite borrarla notificacion que conindicide con el codigo indicado.
        /// </summary>
        /// <param name="mensaje">Mensaje de respuesta del servicio.</param>
        /// <param name="codigo">Codigo de notificacion a borrar</param>
        /// <returns>Indica si finalizo correctamente o no.</returns>
        public bool Borrar(out string message, int codigo)
        {
            try
            {
                var notificacion = ObtenerPrimero(codigo);
                message = null;
                if (notificacion != null)
                {
                    Borrar(notificacion);
                    return true;
                }
                else
                    throw Fabrica.CrearExcepcion("No se encontro la notificacion con el codigo indicado.");
            }
            catch (DoctorWebException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw Fabrica.CrearExcepcion(interna: e);
            }
        }

        /// <summary>
        /// Este metodo permite guardar los cambios de la notificacion que se indica.
        /// </summary>
        /// <param name="notificacion">Notificacion a guardar</param>
        /// <returns>Indica si finalizo correctamente o no.</returns>
        public bool Guardar(Notificacion notificacion)
        {
            try
            {
                if (notificacion.NotificacionId > 0)
                    Actualizar(notificacion, notificacion.NotificacionId);
                else
                    Crear(notificacion);
                return true;                
            }
            catch (DoctorWebException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw Fabrica.CrearExcepcion(interna: e);
            }
        }

        /// <summary>
        /// Este metodo permite obtener una notificacion a partir del nombre que se le indique.
        /// </summary>
        /// <param name="codigo">Nombre de la notificacion.</param>
        /// <returns>Retorna la notificacion en caso de encontrar registro, si no es nulo.</returns>
        public Notificacion Obtener(string nombre)
        {
            try
            {
                var notificacion = ObtenerPrimeroQue(notificaion => notificaion.Nombre == nombre);

                if (notificacion == null)
                    throw Fabrica.CrearExcepcion(mensaje: "No se encontro el registro que busca");

                return notificacion;
            }
            catch (DoctorWebException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw Fabrica.CrearExcepcion(interna: e);
            }
        }

        /// <summary>
        /// Este metodo permite obtener una notificacion a partir del codigo que se le indique.
        /// </summary>
        /// <param name="codigo">Codigo de la notificacion.</param>
        /// <returns>Retorna la notificacion en caso de encontrar registro, si no es nulo.</returns>
        public Notificacion Obtener(int codigo)
        {
            try
            {
                Notificacion notificacion = ObtenerPrimero(codigo);

                if (notificacion == null)
                    throw Fabrica.CrearExcepcion(mensaje: "No se encontro el registro que busca");

                return notificacion;
            }
            catch (DoctorWebException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw Fabrica.CrearExcepcion(interna: e);
            }
        }

    }
}