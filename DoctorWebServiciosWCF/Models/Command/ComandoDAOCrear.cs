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
    /// Comando para crear registro en base de datos.
    /// </summary>
    public class ComandoDAOCrear : IComandoDAO
    {
        /// <summary>
        /// Accion de crear registro en base de datos.
        /// </summary>
        /// <typeparam name="T">Tipo de coleccion</typeparam>
        /// <param name="args">Parametros necesarios.</param>
        public void Ejecutar<T>(params object[] args)
            where T : class
        {
            if (args.Length == 3)
            {
                ContextoBD db;
                if (args[0] is ContextoBD)
                    db = (ContextoBD)args[0];
                else
                    throw Fabrica.CrearExcepcion(mensaje: "ComandoDAOCrear, primer parametro no es valido. se espera un ContextoBD.");

                DbSet<T> coleccion;
                if (args[1] is DbSet<T>)
                    coleccion = (DbSet<T>)args[1];
                else
                    throw Fabrica.CrearExcepcion(mensaje: "ComandoDAOCrear, segundo parametro no es valido. se espera un DbSet<T>.");

                T datos;
                if (args[2] is T)
                    datos = (T)args[2];
                else
                    throw Fabrica.CrearExcepcion(mensaje: "ComandoDAOCrear, tercer parametro no es valido. se espera un T.");
                // Era necesario el attach
                coleccion.Attach(datos);
                coleccion.Add(datos);
                db.SaveChanges();
            }
            else
                throw Fabrica.CrearExcepcion(mensaje: "ComandoDAOCrear, cantidad de parametros no es valida. se espera 3.");
        }
    }
}