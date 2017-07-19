using DoctorWebASP.Controllers.Helpers;
using DoctorWebASP.Models.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Models
{
    /// <summary>
    /// Esta clase es una represencacion de una Notificacion segun la estructura de datos.
    /// </summary>
    public class Notificacion
    {
        #region Instancia

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
        #endregion
    }
}