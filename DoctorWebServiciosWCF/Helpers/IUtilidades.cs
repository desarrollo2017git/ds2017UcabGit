using DoctorWebServiciosWCF.Helpers;
using System.Collections.Specialized;

namespace DoctorWebServiciosWCF.Controllers.Helpers
{
    /// <summary>
    /// Interfaz que debe aplicar toda utilidad.
    /// </summary>
    public interface IUtilidades
    {
        IFabrica Fabrica { get; }

        /// <summary>
        /// Permite obtener informacion del archivo de configuracion.
        /// </summary>
        /// <param name="key">Clave necesaria para buscar su valor asociado.</param>
        /// <returns>Valor segun la clave.</returns>
        string ObtenerClave(string key);

        /// <summary>
        /// Generar mensaje Debug.
        /// </summary>
        /// <param name="mensaje">Mensaje a guardar.</param>
        void Debug(string mensaje);

        /// <summary>
        /// Obtiene la cabecera actual.
        /// </summary>
        /// <returns>Returna la cabecera actual.</returns>
        NameValueCollection ObtenerCabeceraActual();
    }
}