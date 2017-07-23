using DoctorWebServiciosWCF.Controllers.Helpers;
using DoctorWebServiciosWCF.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace DoctorWebServiciosWCF.Models.Command
{
    /// <summary>
    /// Comando para obtener todos los registros que cumplen con una condicion.
    /// </summary>
    /// <typeparam name="T2"></typeparam>
    public class ComandoDAOObtenerTodosLosQue<T2> : IComandoDAOConResultado
        where T2 : class
    {
        /// <summary>
        /// Accion de obtener todos los registro en base de datos segun una condicion.
        /// </summary>
        /// <typeparam name="T">Tipo de coleccion</typeparam>
        /// <param name="args">Parametros necesarios.</param>
        public T Ejecutar<T>(params object[] args) where T : class
        {
            if (args.Length == 2)
            {
                DbSet<T2> coleccion;
                if (args[0] is DbSet<T2>)
                    coleccion = (DbSet<T2>)args[0];
                else
                    throw Utilidades.Instancia.Fabrica.CrearExcepcion(mensaje: "ComandoDAOObtenerTodosLosQue, primer parametro no es valido, se espera un DbSet.");

                Expression<Func<T2, bool>> exprecion;
                if (args[1] is Expression<Func<T2, bool>>)
                    exprecion = (Expression<Func<T2, bool>>)args[1];
                else
                    throw Utilidades.Instancia.Fabrica.CrearExcepcion(mensaje: "ComandoDAOObtenerTodosLosQue, segundo parametro no es valido, se espera un Expression<Func<T2, bool>>.");

                Utilidades.Instancia.Debug($"Buscando {typeof(T2).Name}.");
                var valor = (T)coleccion.Where(exprecion);
                if (valor == null)
                    Utilidades.Instancia.Debug($"No se encontro registros de {typeof(T2).Name}.");
                else
                    Utilidades.Instancia.Debug($"La busqueda de {typeof(T).Name} culmino sin problemas.");
                return valor;
            }
            else
                throw Utilidades.Instancia.Fabrica.CrearExcepcion(mensaje: "ComandoDAOObtenerTodosLosQue, cantidad de parametros no es valida, se espera 2.");
        }
    }
}