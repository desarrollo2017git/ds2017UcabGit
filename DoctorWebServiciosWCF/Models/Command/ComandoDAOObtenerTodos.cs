using DoctorWebServiciosWCF.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.Command
{
    public class ComandoDAOObtenerTodos : IComandoDAOConResultado
    {
        public T Ejecutar<T>(params object[] args) where T : class
        {
            if (args.Length == 1)
            {
                T coleccion;
                if (args[0] is DbSet)
                    coleccion = (T)args[0];
                else
                    throw Fabrica.CrearExcepcion(mensaje: "ComandoDAOObtenerTodos, primer parametro no es valido. se espera un DbSet.");
                
                return coleccion;
            }
            else
                throw Fabrica.CrearExcepcion(mensaje: "ComandoDAOObtenerTodos, cantidad de parametros no es valida. se espera 1.");
        }
    }
}