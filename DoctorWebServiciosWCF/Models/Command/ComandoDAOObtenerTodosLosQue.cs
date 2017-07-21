using DoctorWebServiciosWCF.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace DoctorWebServiciosWCF.Models.Command
{
    public class ComandoDAOObtenerTodosLosQue<T2> : IComandoDAOConResultado
        where T2 : class
    {
        public T Ejecutar<T>(params object[] args) where T : class
        {
            if (args.Length == 2)
            {
                DbSet<T2> coleccion;
                if (args[0] is DbSet)
                    coleccion = (DbSet<T2>)args[0];
                else
                    throw Fabrica.CrearExcepcion(mensaje: "ComandoDAOObtenerTodosLosQue, primer parametro no es valido, se espera un DbSet.");

                Expression<Func<T2, bool>> exprecion;
                if (args[1] is Expression<Func<T2, bool>>)
                    exprecion = (Expression<Func<T2, bool>>)args[1];
                else
                    throw Fabrica.CrearExcepcion(mensaje: "ComandoDAOObtenerTodosLosQue, segundo parametro no es valido, se espera un Expression<Func<T2, bool>>.");


                return (T)coleccion.Where(exprecion);
            }
            else
                throw Fabrica.CrearExcepcion(mensaje: "ComandoDAOObtenerTodosLosQue, cantidad de parametros no es valida, se espera 2.");
        }
    }
}