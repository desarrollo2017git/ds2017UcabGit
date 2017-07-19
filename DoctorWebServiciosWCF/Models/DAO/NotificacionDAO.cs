using DoctorWebServiciosWCF.Helpers;
using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.ORM;
using DoctorWebServiciosWCF.Models.Results;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.DAO
{
    public class NotificacionDAO : DAO<Notificacion>, INotificacionDAO
    {
        public NotificacionDAO():base()
        {
            coleccion = db.Notificaciones;
        }

        public List<Notificacion> ObtenerTodos(out int cantidadPaginas, string nombre = null, int pagina = 0, int numeroFilas = 30)
        {
            try
            {
                var cantidadRegistros = Contar();
                cantidadPaginas = (int)Math.Ceiling(cantidadRegistros / (double)numeroFilas);

                IQueryable<Notificacion> consulta = ObtenerTodos()
                    .OrderBy(notificacion => notificacion.Nombre);

                if (!String.IsNullOrEmpty(nombre))
                    consulta = consulta.Where(notificaion => notificaion.Nombre.Contains(nombre));

                var notificaciones = consulta.Skip(pagina * numeroFilas)
                    .Take(numeroFilas).ToList();
         
                return notificaciones;                
            }
            catch (DoctorWebException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw Fabrica.CrearExcepcion(interna: e);
            }
        }

        public bool Borrar(out string message, int codigo)
        {
            try
            {
                var notificacion = ObtenerPrimero(codigo);
                message = null;
                if (notificacion != null)
                {
                    Borrar(notificacion);
                    return true;
                }
                else
                    throw Fabrica.CrearExcepcion("No se encontro la notificacion con el codigo indicado.");
            }
            catch (DoctorWebException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw Fabrica.CrearExcepcion(interna: e);
            }
        }

        public bool Guardar(Notificacion notificacion)
        {
            var resultado = new ResultadoProceso();
            try
            {
                if (notificacion.NotificacionId > 0)
                    Actualizar(notificacion, notificacion.NotificacionId);
                else
                    Crear(notificacion);
                return true;                
            }
            catch (DoctorWebException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw Fabrica.CrearExcepcion(interna: e);
            }
        }

        public Notificacion Obtener(string nombre)
        {
            var resultado = new ResultadoServicio<Notificacion>();
            try
            {
                using (var db = new ContextoBD())
                {
                    var notificaciones = ObtenerTodosLosQue(notificaion => notificaion.Nombre == nombre);

                    var notificacion = notificaciones.ToList().First();

                    if (notificacion == null)
                        throw Fabrica.CrearExcepcion(mensaje: "No se encontro el registro que busca");

                    return notificacion;
                }
            }
            catch (DoctorWebException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw Fabrica.CrearExcepcion(interna: e);
            }
        }

        public Notificacion Obtener(int codigo)
        {
            try
            {
                Notificacion notificacion = ObtenerPrimero(codigo);

                if (notificacion == null)
                    throw new Exception("No se encontro el registro que busca");

                return notificacion;
            }
            catch (DoctorWebException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new DoctorWebException(mensaje: ex.Message, interna: ex);
            }
        }

    }
}