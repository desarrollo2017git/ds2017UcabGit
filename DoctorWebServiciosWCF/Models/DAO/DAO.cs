using DoctorWebServiciosWCF.Helpers;
using DoctorWebServiciosWCF.Models.Command;
using DoctorWebServiciosWCF.Models.ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DoctorWebServiciosWCF.Models.DAO
{
    /// <summary>
    /// Clase DAO con las primitivas necesarias para manipular la base de datos.
    /// </summary>
    /// <typeparam name="T">Clase base a la cual se le desea aplicar el DAO.</typeparam>
    public class DAO<T> : IDAO<T>
        where T : class
    {
        /// <summary>
        /// Contexto utilizado para trabajar con Entity Framework (ORM).
        /// </summary>
        public ContextoBD db { get; set; }

        /// <summary>
        /// Referencia a la que se aplican las primitivas definidas.
        /// </summary>
        private DbSet<T> coleccion { get; set; }

        /// <summary>
        /// Constructor por defecto de la clase DAO.
        /// </summary>
        public DAO()
        {
            db = new ContextoBD();

            foreach (var property in db.GetType().GetProperties())
            {
                var genericTypes = property.PropertyType.GenericTypeArguments;
                if (genericTypes != null && genericTypes.Contains(typeof(T)))
                {
                    this.coleccion = (DbSet<T>)property.GetValue(db);
                    break;
                }

            }
            if (this.coleccion == null)
                throw Fabrica.CrearExcepcion($"No se detecto un DbSet asociado a {typeof(T).FullName}");
        }

        /// <summary>
        /// Este metodo ejecuta un comando para obtener la coleccion principal del DAO.
        /// </summary>
        /// <returns>Retorna la coleccion utilizada en la instancia.</returns>
        public IQueryable<T> ObtenerTodos()
        {
            IComandoDAOConResultado comando = new ComandoDAOObtenerTodos<T>();
            return comando.Ejecutar<IQueryable<T>>(coleccion);
        }

        /// <summary>
        /// Este metodo ejecuta un comando para obtener la coleccion principal del DAO filtrando segun una condicion.
        /// </summary>
        /// <param name="condicion">Funcion utilizada como criterio para filtrar.</param>
        /// <returns>Retorna la coleccion una vez filtrada.</returns>
        public IQueryable<T> ObtenerTodosLosQue(Expression<Func<T, bool>> condicion)
        {
            IComandoDAOConResultado comando = new ComandoDAOObtenerTodosLosQue<T>();
            return comando.Ejecutar<IQueryable<T>>(coleccion, condicion);
        }
        
        /// <summary>
        /// Este metodo ejecuta un comando para obtener una instancia filtrando con condicion indicada.
        /// </summary>
        /// <param name="condicion">Criterio para filtar los datos.</param>
        /// <returns>Retorna una instancia de la clase base.</returns>
        public T ObtenerPrimeroQue(Expression<Func<T, bool>> condicion)
        {
            IComandoDAOConResultado comando = new ComandoDAOObtenerPrimeroQue();
            return comando.Ejecutar<T>(coleccion, condicion);
        }

        /// <summary>
        /// Este metodo permite obtener la cantidad de registros en la base de datos.
        /// </summary>
        /// <returns>La cantidad de registro.</returns>
        public int Contar()
        {            
            IComandoDAOConResultado comando = new ComandoDAOContar<T>();
            var resultado = comando.Ejecutar<string>(coleccion);
            return int.Parse(resultado);
        }

        /// <summary>
        /// Este metodo permite borrar la instancia indicada de la base de datos.
        /// </summary>
        /// <param name="datos">Instancia a borrar de la base de datos.</param>
        public void Borrar(T datos)
        {
            IComandoDAO crear = new ComandoDAOBorrar();
            crear.Ejecutar<T>(db, coleccion, datos);
        }

        /// <summary>
        /// Este metodo permite guardar la instancia indicada en la base de datos.
        /// </summary>
        /// <param name="datos">Instancia a guardar en la base de datos.</param>
        public void Crear(T datos)
        {
            IComandoDAO crear = new ComandoDAOCrear();
            crear.Ejecutar<T>(db, coleccion, datos);
        }

        /// <summary>
        /// Este metodo permite actualizar la instancia indicada en la base de datos.
        /// </summary>
        /// <param name="datos">Instancia a actualizar enla base de datos.</param>
        public void Actualizar(object datos, Expression<Func<T, bool>> condicion)
        {
            IComandoDAO actualizar = new ComandoDAOActualizar();
            actualizar.Ejecutar<T>(db, coleccion, datos, condicion);
        }
    }
}