using DoctorWebServiciosWCF.Controllers.Helpers;
using DoctorWebServiciosWCF.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.Command
{
    /// <summary>
    /// Comando para Obtener todos los registros.
    /// </summary>
    /// <typeparam name="T2"></typeparam>
    public class ComandoDAOObtenerTodos<T2> : IComandoDAOConResultado
        where T2 : class
    {
        /// <summary>
        /// Accion de obtener todos registro en base de datos.
        /// </summary>
        /// <typeparam name="T">Tipo de coleccion</typeparam>
        /// <param name="args">Parametros necesarios.</param>
        public T Ejecutar<T>(params object[] args) where T : class
        {
            if (args.Length == 1)
            {
                object coleccion;
                if (args[0] is DbSet<T2>)
                    coleccion = (DbSet<T2>)args[0];
                else
                    throw Utilidades.Instancia.Fabrica.CrearExcepcion(mensaje: "ComandoDAOObtenerTodos, primer parametro no es valido. se espera un DbSet.");

                Utilidades.Instancia.Debug($"Buscando {typeof(T2).Name}.");
                var valor = (T)coleccion;
                if (valor == null)
                    Utilidades.Instancia.Debug($"No se encontraron reistros de {typeof(T2).Name}.");
                else
                    Utilidades.Instancia.Debug($"La busqueda de {typeof(T2).Name} culmino sin problemas.");
                return valor;
            }
            else
                throw Utilidades.Instancia.Fabrica.CrearExcepcion(mensaje: "ComandoDAOObtenerTodos, cantidad de parametros no es valida. se espera 1.");
        }
    }
}