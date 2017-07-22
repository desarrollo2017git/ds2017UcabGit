using DoctorWebServiciosWCF.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.Command
{
    /// <summary>
    /// Comando para obtener primer elemento segun claves.
    /// </summary>
    public class ComandoDAOObtenerPrimero : IComandoDAOConResultado
    {
        /// <summary>
        /// Accion de obtener primer registro en base de datos segun claves.
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
                    throw Fabrica.CrearExcepcion(mensaje: "ComandoDAOObtenerPrimero, primer parametro no es valido. se espera un DbSet.");

                object[] keys = null;
                if (args[1] is object[])
                    keys = (object[])args[1];
                else
                    throw Fabrica.CrearExcepcion(mensaje: "ComandoDAOObtenerPrimero, segundo parametro no es valido. se espera un object[].");

                return coleccion.Find(keys);
            }
            else
                throw Fabrica.CrearExcepcion(mensaje: "ComandoDAOObtenerPrimero, cantidad de parametros no es valida. se espera 2.");
        }
    }
}