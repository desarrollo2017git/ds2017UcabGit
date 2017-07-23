using DoctorWebServiciosWCF.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace DoctorWebServiciosWCF.Models
{
    /// <summary>
    /// Esta clase es una represencacion de una Notificacion segun la estructura de datos.
    /// </summary>
    public class Notificacion
    {
        /// <summary>
        /// Identificador de notificacion.
        /// </summary>
        public int NotificacionId { get; set; }

        /// <summary>
        /// Estado de la notificacion.
        /// </summary>
        [Required]
        public NotificacionEstado Estado { get; set; }
        
        /// <summary>
        /// Nombre de la notificacion.
        /// </summary>
        [Required]
        [StringLength(60)]
        public string Nombre { get; set; }

        /// <summary>
        /// Descripcion de la notificacion.
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Descripcion { get; set; }

        /// <summary>
        /// Asunto de la notificacion.
        /// </summary>
        [Required]
        [StringLength(128)]
        public string Asunto { get; set; }
        
        /// <summary>
        /// Contenido de a notificacion.
        /// </summary>
        [Required]
        public string Contenido { get; set; }

        /// <summary>
        /// Este metodo permite enviar la notificacion indicando el destinatario y de ser necesario los parametros al contenido.
        /// </summary>
        /// <param name="destinatario">A quien se envia la notificacion.</param>
        /// <param name="parametros">Datos a reemplazar en el conenido</param>
        public void Enviar(string destinatario, object parametros = null)
        {
            var direccion = new MailAddress(destinatario);
            this.Enviar(new[] { direccion }, parametros);
        }

        /// <summary>
        /// Este metodo permite enviar la notificacion indicando varios destinatarios y de ser necesario los parametros al contenido.
        /// </summary>
        /// <param name="destinatario">A quienes se envia la notificacion.</param>
        /// <param name="parametros">Datos a reemplazar en el conenido</param>
        public void Enviar(string[] destinatarios, object parametros = null)
        {
            List<MailAddress> direcciones = new List<MailAddress>();
            if (destinatarios.Length == 0)
                throw new ArgumentException("Debe indicar al menos 1 destinatario");
            foreach (var destinatario in destinatarios)
            {
                direcciones.Add(new MailAddress(destinatario));
            }
            this.Enviar(direcciones.ToArray(), parametros);
        }

        /// <summary>
        /// Este metodo permite enviar la notificacion indicando varios destinatarios y de ser necesario los parametros al contenido.
        /// </summary>
        /// <param name="destinatario">A quienes se envia la notificacion.</param>
        /// <param name="parametros">Datos a reemplazar en el conenido</param>
        private void Enviar(MailAddress[] destinatarios, object parametros = null)
        {
            if (Estado == NotificacionEstado.Disponible)
                ThreadPool.QueueUserWorkItem(new WaitCallback((obj) =>
                {
                    try
                    {
                        var host = Utilidades.Instancia.ObtenerClave("SMTPServerHost");
                        var port = Utilidades.Instancia.ObtenerClave("SMTPServerPost");
                        var fromName = Utilidades.Instancia.ObtenerClave("SMTPFromName");
                        var user = Utilidades.Instancia.ObtenerClave("SMTPUserId");
                        var pass = Utilidades.Instancia.ObtenerClave("SMTPUserPassword");

                        if (!String.IsNullOrEmpty(host) &&
                            !String.IsNullOrEmpty(port) &&
                            !String.IsNullOrEmpty(fromName) &&
                            !String.IsNullOrEmpty(user) &&
                            !String.IsNullOrEmpty(pass))
                        {

                            using (SmtpClient client = new SmtpClient(host: host, port: int.Parse(port)))
                            {
                                var credential = new NetworkCredential(userName: user, password: pass);
                                client.UseDefaultCredentials = false;
                                client.Credentials = credential;
                                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                                client.EnableSsl = true;

                                var from = new MailAddress(address: user, displayName: fromName);

                                var message = new MailMessage();
                                message.From = from;
                                foreach (var destinatario in destinatarios)
                                {
                                    message.To.Add(destinatario);
                                }
                                message.Subject = Asunto;

                                var contenido = ColocarParametros(Contenido, parametros);

                                message.Body = contenido;
                                message.IsBodyHtml = true;

                                client.Send(message);
                            }
                        }
                    }
                    catch //(Exception ex)
                    {
                        //File.AppendAllLines("notificaciones.log", new[] { ex.Message, ex.StackTrace });
                    }
                }));
        }

        /// <summary>
        /// Permite reemplacar las claves de un contenido por su valor.
        /// </summary>
        /// <param name="contenido">Contenido con claves {{clave}}.</param>
        /// <param name="parametros">Valores a reemplazar</param>
        /// <returns>Contenido procesado.</returns>
        private string ColocarParametros(string contenido, object parametros)
        {
            StringBuilder resultado = new StringBuilder(contenido);

            if (parametros != null)
            {
                if (parametros is Dictionary<string, object>)
                {
                    foreach (var parametro in parametros as Dictionary<string, object>)
                    {
                        try
                        {
                            var valor = parametro.Value.ToString();
                            var attibuto = parametro.Key;
                            resultado = resultado.Replace($"{{{{{attibuto}}}}}", valor);
                        }
                        catch (Exception) { }
                    }
                }
                else
                    foreach (var parametro in parametros.GetType().GetProperties())
                    {
                        try
                        {
                            var valor = parametro.GetValue(parametros).ToString();
                            var attibuto = parametro.Name;

                            resultado = resultado.Replace($"{{{{{attibuto}}}}}", valor);
                        }
                        catch (Exception) { }
                    }
            }
            resultado = resultado.Replace($"{{{{FechaActual}}}}", DateTime.Now.ToShortDateString());

            return resultado.ToString();
        }
    }

    /// <summary>
    /// Conjunto de valores posibles de Estado de una notificacion.
    /// </summary>
    public enum NotificacionEstado : byte
    {
        Disponible, Borrada
    }

}