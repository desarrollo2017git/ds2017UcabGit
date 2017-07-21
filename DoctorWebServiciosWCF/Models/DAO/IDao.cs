using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWebServiciosWCF.Models.DAO
{
    /// <summary>
    /// Clase DAO con las primitivas necesarias para manipular la base de datos.
    /// </summary>
    /// <typeparam name="T">Clase base a la cual se le desea aplicar el DAO.</typeparam>
    public interface IDAO<T>
    {
        /// <summary>
        /// Este metodo ejecuta un comando para obtener la coleccion principal del DAO.
        /// </summary>
        /// <returns>Retorna la coleccion utilizada en la instancia.</returns>
        IQueryable<T> ObtenerTodos();

        /// <summary>
        /// Este metodo ejecuta un comando para obtener la coleccion principal del DAO filtrando segun una condicion.
        /// </summary>
        /// <param name="condicion">Funcion utilizada como criterio para filtrar.</param>
        /// <returns>Retorna la coleccion una vez filtrada.</returns>
        IQueryable<T> ObtenerTodosLosQue(Expression<Func<T, bool>> condicion);

        /// <summary>
        /// Este metodo ejecuta un comando para obtener una instancia filtrando con las claves indicadas.
        /// </summary>
        /// <param name="claves">Utilizada para filtrar</param>
        /// <returns>Retorna una instancia de la clase base.</returns>
        T ObtenerPrimero(params object[] keys);

        /// <summary>
        /// Este metodo ejecuta un comando para obtener una instancia filtrando con condicion indicada.
        /// </summary>
        /// <param name="condicion">Criterio para filtar los datos.</param>
        /// <returns>Retorna una instancia de la clase base.</returns>
        T ObtenerPrimeroQue(Expression<Func<T, bool>> condicion);

        /// <summary>
        /// Este metodo permite obtener la cantidad de registros en la base de datos.
        /// </summary>
        /// <returns>La cantidad de registro.</returns>
        int Contar();

        /// <summary>
        /// Este metodo permite borrar la instancia indicada de la base de datos.
        /// </summary>
        /// <param name="datos">Instancia a borrar de la base de datos.</param>
        void Borrar(T datos);
        
        /// <summary>
        /// Este metodo permite guardar la instancia indicada en la base de datos.
        /// </summary>
        /// <param name="datos">Instancia a guardar en la base de datos.</param>
        void Crear(T datos);

        /// <summary>
        /// Este metodo permite actualizar la instancia indicada en la base de datos.
        /// </summary>
        /// <param name="datos">Instancia a actualizar enla base de datos.</param>
        void Actualizar(object datos, params object[] keys);
    }
}
