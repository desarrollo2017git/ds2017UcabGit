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
    /// Comando para borrar registro en base de datos.
    /// </summary>
    public class ComandoDAOBorrar : IComandoDAO
    {
        /// <summary>
        /// Accion de borrar registro en base de datos.
        /// </summary>
        /// <typeparam name="T">Tipo de coleccion</typeparam>
        /// <param name="args">Parametros necesarios.</param>
        public void Ejecutar<T>(params object[] args) where T : class
        {
            if (args.Length == 3)
            {
                ContextoBD db;
                if (args[0] is ContextoBD)
                    db = (ContextoBD)args[0];
                else
                    throw Fabrica.CrearExcepcion(mensaje: "ComandoDAOBorrar, primer parametro no es valido. se espera un ContextoBD.");

                DbSet<T> coleccion;
                if (args[1] is DbSet<T>)
                    coleccion = (DbSet<T>)args[1];
                else
                    throw Fabrica.CrearExcepcion(mensaje: "ComandoDAOBorrar, segundo parametro no es valido. se espera un DbSet<T>.");

                T datos;
                if (args[2] is T)
                    datos = (T)args[2];
                else
                    throw Fabrica.CrearExcepcion(mensaje: "ComandoDAOBorrar, tercer parametro no es valido. se espera un T.");

                coleccion.Remove(datos);
                db.SaveChanges();
            }
            else
                throw Fabrica.CrearExcepcion(mensaje: "ComandoDAOBorrar, cantidad de parametros no es valida. se espera 3.");            
        }
    }
}