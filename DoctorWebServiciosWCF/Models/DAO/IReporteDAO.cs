using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWebServiciosWCF.Models.DAO
{
    /// <summary>
    /// Interfaz con las propiedades que debe implementar la clase DAO para el modulo de Reportes.
    /// </summary>
    public interface IReporteDAO
    {
        #region REPORTES PREESTABLECIDOS
        /// <summary>
        /// Método utilizado para obtener la cantidad de usuarios registrados durante el periodo de tiempo seleccionado por el usuario.
        /// </summary>
        /// <param name="fechaInicioStr">Fecha incicial para el periodo de conteo de registro de usuarios.</param>
        /// <param name="fechaFinStr">Fecha incicial para el periodo de conteo de registro de usuarios.</param>
        /// <returns>Cantidad de usuarios registrados en un periodo de tiempo determinado.</returns>
        int getCantidadUsuariosRegistrados(string fechaInicioStr, string fechaFinStr);
        /// <summary>
        /// Método utilizado para obtener la edad promedio de los pacientes.
        /// </summary>
        /// <returns>Promedio de edad de los pacientes.</returns>
        double getPromedioEdadPaciente();
        /// <summary>
        /// Método utilizado para obtener el promedio de citas atendidas por médico.
        /// </summary>
        /// <returns>Promedio de citas atendidas por médico</returns>
        double getPromedioCitasPorMedico();
        /// <summary>
        /// Método utilizado para obtener el promedio de recursos disponibles en un periodo de tiempo seleccionado por el usuario.
        /// </summary>
        /// <param name="fechaInicioStr">Fecha incicial para el periodo de conteo de registro de usuarios.</param>
        /// <param name="fechaFinStr">Fecha incicial para el periodo de conteo de registro de usuarios.</param>
        /// <returns>Promedio de recursos disponibles en un periodo de tiempo determinado.</returns>
        double getPromedioRecursosDisponibles(string fechaInicioStr, string fechaFinStr);
        /// <summary>
        /// Método utilizado para obtener el promedio de uso de la aplicación.
        /// </summary>
        /// <returns>Promedio de uso de la aplicación.</returns>
        double getPromedioUsoApp();
        /// <summary>
        /// Método utilizado para obtener el promedio de citas canceladas por médico en un periodo de tiempo seleccionado por el usuario.
        /// </summary>
        /// <param name="fechaInicioStr">Fecha incicial para el periodo de conteo de registro de usuarios.</param>
        /// <param name="fechaFinStr">Fecha incicial para el periodo de conteo de registro de usuarios.</param>
        /// <returns>Promedio de citas canceladas en un periodo de tiempo determinado.</returns>
        double getPromedioCitasCanceladasPorMedico(string fechaInicioStr, string fechaFinStr);
		#endregion
        Dictionary<string, object> obtenerAtributos(List<String> entidades);
        string generarReporteConfigurado(List<DatosConfigurados> datosConfigurados);
    }
}
