using System;
using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.Results;
using DoctorWebServiciosWCF.Models.DAO;
using System.ServiceModel.Web;
using System.Net;
using System.Collections.Generic;

namespace DoctorWebServiciosWCF.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "NotificacionService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione NotificacionService.svc o NotificacionService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ServicioNotificaciones : IServicioNotificaciones
    {
        private readonly NotificacionDAO dao = new NotificacionDAO();

        public ResultadoProceso Borrar(string codigo)
        {
            var resultado = new ResultadoProceso();
            try
            {
                int id = 0;
                if (!int.TryParse(codigo, out id))
                    throw new FormatException("el codigo debe ser un numero.");

                string mensaje = string.Empty;
                resultado.SinProblemas = dao.Borrar(out mensaje, id);
                resultado.Mensaje = mensaje;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        public ResultadoProceso Enviar(string nombre, string correo)
        {
            var resultado = new ResultadoProceso();
            try
            {
                var notificacion = dao.Obtener(nombre);

                IncomingWebRequestContext request = WebOperationContext.Current.IncomingRequest;
                WebHeaderCollection headers = request.Headers;

                var parametros = new Dictionary<string, object>();

                foreach (var key in request.Headers.AllKeys)
                {
                    parametros.Add(key, request.Headers[key]);
                }                

                notificacion.Enviar(correo, parametros);
                resultado.SinProblemas = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }
        
        public ResultadoProceso Guardar(Notificacion notificacion)
        {
            var resultado = new ResultadoProceso();
            try
            {
                string mensaje = string.Empty;
                resultado.SinProblemas = dao.Guardar(notificacion);
                resultado.Mensaje = mensaje;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        public ResultadoServicio<Notificacion> Obtener(string codigo)
        {
            var resultado = new ResultadoServicio<Notificacion>();
            try
            {
                int id = 0;
                if (!int.TryParse(codigo, out id))
                    throw new FormatException("el codigo debe ser un numero.");

                resultado.Contenido = dao.Obtener(id);
                resultado.SinProblemas = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        public ResultadoServicioPaginado<Notificacion> ObtenerTodos(string nombre, int pagina = 0, int numeroFilas = 30)
        {
            var resultado = new ResultadoServicioPaginado<Notificacion>();
            try
            {
                int cantidadPaginas;
                resultado.Contenido = dao.ObtenerTodos(out cantidadPaginas, nombre, pagina, numeroFilas);
                resultado.SinProblemas = true;
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        
    }
}
