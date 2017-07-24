using DoctorWebServiciosWCF.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.Command
{
    /// <summary>
    /// Comando para contar registros en la base de datos.
    /// </summary>
    /// <typeparam name="T2"></typeparam>
    public class ComandoDAOContar<T2> : IComandoDAOConResultado
        where T2 : class
    {
        /// <summary>
        /// Accion de contar registros en base de datos.
        /// </summary>
        /// <typeparam name="T">Tipo de coleccion</typeparam>
        /// <param name="args">Parametros necesarios.</param>
        public T Ejecutar<T>(params object[] args) where T : class
        {
            if (args.Length == 1)
            {
                DbSet<T2> coleccion;
                if (args[0] is DbSet<T2>)
                    coleccion = (DbSet<T2>)args[0];
                else
                    throw Fabrica.CrearExcepcion(mensaje: "ComandoDAOContar, primer parametro no es valido. se espera un DbSet.");

                object c  = ""+coleccion.Count();
                return (T)c;
            }
            else
                throw Fabrica.CrearExcepcion(mensaje: "ComandoDAOContar, cantidad de parametros no es valida. se espera 1.");
        }
    }
}