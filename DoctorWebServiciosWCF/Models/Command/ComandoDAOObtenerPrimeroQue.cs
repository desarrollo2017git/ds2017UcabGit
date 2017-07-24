using DoctorWebServiciosWCF.Helpers;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DoctorWebServiciosWCF.Models.Command
{
    /// <summary>
    /// Obtener primer elemento segun una condicion.
    /// </summary>
    public class ComandoDAOObtenerPrimeroQue : IComandoDAOConResultado
    {
        /// <summary>
        /// Accion de obtener primer registro en base de datos segun una condicion.
        /// </summary>
        /// <typeparam name="T">Tipo de coleccion</typeparam>
        /// <param name="args">Parametros necesarios.</param>
        public T Ejecutar<T>(params object[] args) where T : class
        {
            if (args.Length == 2)
            {
                DbSet<T> coleccion;
                if (args[0] is DbSet<T>)
                    coleccion = (DbSet<T>)args[0];
                else
                    throw Utilidades.Instancia.Fabrica.CrearExcepcion(mensaje: "ComandoDAOObtenerPrimero, primer parametro no es valido. se espera un DbSet.");

                Expression<Func<T, bool>> exprecion;
                if (args[1] is Expression<Func<T, bool>>)
                    exprecion = (Expression<Func<T, bool>>)args[1];
                else
                    throw Utilidades.Instancia.Fabrica.CrearExcepcion(mensaje: "ComandoDAOObtenerPrimero, segundo parametro no es valido, se espera un Expression<Func<T2, bool>>.");

                Utilidades.Instancia.Debug($"Buscando {typeof(T).Name}.");
                var registro = coleccion.Single(exprecion);
                if(registro == null)
                    Utilidades.Instancia.Debug($"No se encontro registro de {typeof(T).Name}.");
                else
                    Utilidades.Instancia.Debug($"La busqueda de {typeof(T).Name} culmino sin problemas.");

                return registro;
            }
            else
                throw Utilidades.Instancia.Fabrica.CrearExcepcion(mensaje: "ComandoDAOObtenerPrimero, cantidad de parametros no es valida. se espera 2.");
        }
    }
}