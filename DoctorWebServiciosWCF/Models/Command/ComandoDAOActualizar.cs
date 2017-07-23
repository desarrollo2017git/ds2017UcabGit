using DoctorWebServiciosWCF.Controllers.Helpers;
using DoctorWebServiciosWCF.Helpers;
using DoctorWebServiciosWCF.Models.ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace DoctorWebServiciosWCF.Models.Command
{
    /// <summary>
    /// Este comando permite actualizar usando el ORM.
    /// </summary>
    public class ComandoDAOActualizar : IComandoDAO
    {
        /// <summary>
        /// Accion de actualizar registro en base de datos.
        /// </summary>
        /// <typeparam name="T">Tipo de coleccion</typeparam>
        /// <param name="args">Parametros necesarios.</param>
        public void Ejecutar<T>(params object[] args)
            where T : class
        {
            if (args.Length == 4)
            {
                ContextoBD db;
                if (args[0] is ContextoBD)
                    db = (ContextoBD)args[0];
                else
                    throw Utilidades.Instancia.Fabrica.CrearExcepcion(mensaje: "ComandoDAOActualizar, primer parametro no es valido. se espera un ContextoBD.");

                DbSet<T> coleccion;
                if (args[1] is DbSet<T>)
                    coleccion = (DbSet<T>)args[1];
                else
                    throw Utilidades.Instancia.Fabrica.CrearExcepcion(mensaje: "ComandoDAOActualizar, segundo parametro no es valido. se espera un DbSet<T>.");

                object datos = args[2];

                Expression<Func<T, bool>> exprecion;
                if (args[3] is Expression<Func<T, bool>>)
                    exprecion = (Expression<Func<T, bool>>)args[3];
                else
                    throw Utilidades.Instancia.Fabrica.CrearExcepcion(mensaje: "ComandoDAOActualizar, cuarto parametro no es valido, se espera un Expression<Func<T2, bool>>.");

                Utilidades.Instancia.Debug($"Actualizando {typeof(T).Name}.");
                var registro = coleccion.Single(exprecion);
                if (registro != null)
                {
                    db.Entry(registro).CurrentValues.SetValues(datos);
                    db.SaveChanges();
                    Utilidades.Instancia.Debug($"Registro {typeof(T).Name} actualizado.");
                }
                else
                    Utilidades.Instancia.Debug($"No se encontro registros {typeof(T).Name}.");
            }
            else
                throw Utilidades.Instancia.Fabrica.CrearExcepcion(mensaje: "ComandoDAOActualizar, cantidad de parametros no es valida. se espera 3.");
        }
    }
}