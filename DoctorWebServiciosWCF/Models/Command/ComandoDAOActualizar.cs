using DoctorWebServiciosWCF.Helpers;
using DoctorWebServiciosWCF.Models.ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
                    throw Fabrica.CrearExcepcion(mensaje: "ComandoDAOActualizar, primer parametro no es valido. se espera un ContextoBD.");

                DbSet<T> coleccion;
                if (args[1] is DbSet<T>)
                    coleccion = (DbSet<T>)args[1];
                else
                    throw Fabrica.CrearExcepcion(mensaje: "ComandoDAOActualizar, segundo parametro no es valido. se espera un DbSet<T>.");

                object datos = args[2];
                
                object[] keys = null;
                if (args[3] is object[])
                    keys = (object[])args[3];
                else
                    throw Fabrica.CrearExcepcion(mensaje: "ComandoDAOActualizar, cuarto parametro no es valido. se espera un arreglo de objetos.");

                var registro = db.Notificaciones.Find(keys);
                if (registro != null)
                {
                    db.Entry(registro).CurrentValues.SetValues(datos);
                    db.SaveChanges();
                }
            }
            else
                throw Fabrica.CrearExcepcion(mensaje: "ComandoDAOActualizar, cantidad de parametros no es valida. se espera 3.");
        }
    }
}