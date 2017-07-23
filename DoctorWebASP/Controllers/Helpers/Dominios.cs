using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebASP.Controllers.Helpers
{
    #region Grupo 01
    #endregion

    #region Grupo 02
    #endregion

    #region Grupo 03
    #endregion

    #region Grupo 04
    #endregion

    #region Grupo 05
    #endregion

    #region Grupo 06
    #endregion

    #region Grupo 07
    #endregion

    #region Grupo 08
    #endregion

    #region Grupo 09
    public enum NotificacionEstado : byte
    {
        Disponible, Borrada
    }
    #endregion


    /// <summary>
    /// Etiquetas usadas en los tipos de notificaciones.
    /// </summary>
    public enum EnumDoctorWebTipoNotificacion
    {
        info,
        danger,
        success,
        warning
    }

    /// <summary>
    /// Etiquetas usadas en las variables de sesion.
    /// </summary>
    public enum EnumDoctorWebSession
    {
        #region Grupo 01
        #endregion
        #region Grupo 02
        #endregion
        #region Grupo 03
        #endregion
        #region Grupo 04
        #endregion
        #region Grupo 05
        #endregion
        #region Grupo 06
        #endregion
        #region Grupo 07
        #endregion
        #region Grupo 08
        #endregion
        #region Grupo 09
        #endregion
        #region Comun
        Notificacion,
        TipoNotificacion
        #endregion
    }

    /// <summary>
    /// Etiquetas usadas en el ViewBag.
    /// </summary>
    public enum EnumDoctorWebViewBag
    {
        #region Grupo 01
        #endregion
        #region Grupo 02
        #endregion
        #region Grupo 03
        #endregion
        #region Grupo 04
        #endregion
        #region Grupo 05
        #endregion
        #region Grupo 06
        #endregion
        #region Grupo 07
        #endregion
        #region Grupo 08
        #endregion
        #region Grupo 09
        NotificacionNombre,
        Filas,
        PermitirSiguiente,
        PermitirAnterior,
        SiguienteIndice,
        AnteriorIndice,
        TotalPaginas,
        #endregion
        #region Comun
        PaginaActual
        #endregion
    }

    /// <summary>
    /// Etiquetas usadas para determinar una pagina.
    /// </summary>
    public enum EnumDoctorWebPagina
    {
        #region Grupo 01
        #endregion
        #region Grupo 02
        #endregion
        #region Grupo 03
        VerPacientes,
        CrearPaciente,
        #endregion
        #region Grupo 04
        #endregion
        #region Grupo 05
        #endregion
        #region Grupo 06
        #endregion
        #region Grupo 07
        #endregion
        #region Grupo 08
        Reportes, Configurados,
        #endregion
        #region Grupo 09
        Notificaciones,
        NotificacionGuardar,
        #endregion
        #region Comun
        PorAsignar,
        Inicio
        #endregion
    }
}