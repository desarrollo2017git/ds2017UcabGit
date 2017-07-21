using System.Collections.Generic;

namespace DoctorWebServiciosWCF.Models.DAO
{
    /// <summary>
    /// Interfaz con las primitivas que debe implementar la clase DAO de Notificaciones.
    /// </summary>
    public interface INotificacionDAO
    {
        /// <summary>
        /// Este medoto permite obtener las notificaciones paginando segun se indique y filtrando por el nombre si es necesario.
        /// </summary>
        /// <param name="cantidadPaginas">Cantidad de paginas segun la cantidad de filas.</param>
        /// <param name="nombre">Permite filtrar los datos usando el nombre.</param>
        /// <param name="pagina">Numero de pagina que se esta solicitando.</param>
        /// <param name="cantidadFilas">Cantidad de registros por pagina.</param>
        /// <returns>Lista de notificaciones, puede estar vacia en caso de no encontrar.</returns>
        List<Notificacion> ObtenerTodos(out int cantidadPaginas, string nombre = null, int pagina = 0, int numeroFilas = 30);

        /// <summary>
        /// Este metodo permite obtener una notificacion a partir del codigo que se le indique.
        /// </summary>
        /// <param name="codigo">Codigo de la notificacion.</param>
        /// <returns>Retorna la notificacion en caso de encontrar registro, si no es nulo.</returns>
        Notificacion Obtener(int codigo);

        /// <summary>
        /// Este metodo permite obtener una notificacion a partir del nombre que se le indique.
        /// </summary>
        /// <param name="codigo">Nombre de la notificacion.</param>
        /// <returns>Retorna la notificacion en caso de encontrar registro, si no es nulo.</returns>
        Notificacion Obtener(string nombre);

        /// <summary>
        /// Este metodo permite guardar los cambios de la notificacion que se indica.
        /// </summary>
        /// <param name="notificacion">Notificacion a guardar</param>
        /// <returns>Indica si finalizo correctamente o no.</returns>
        bool Guardar(Notificacion notificacion);
        
        /// <summary>
        /// Este metodo permite borrarla notificacion que conindicide con el codigo indicado.
        /// </summary>
        /// <param name="mensaje">Mensaje de respuesta del servicio.</param>
        /// <param name="codigo">Codigo de notificacion a borrar</param>
        /// <returns>Indica si finalizo correctamente o no.</returns>
        bool Borrar(out string message, int codigo);
    }
}
