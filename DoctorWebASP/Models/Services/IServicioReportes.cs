using DoctorWebASP.Models.Results;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWebASP.Models.Services
{
    /// <summary>
    /// Esta interfaz tiene las propiedades necesarias para la interacción con los servicios.
    /// </summary>
    public interface IServicioReportes
    {
        #region REPORTES PREESTABLECIDOS
        /// <summary>
        /// Método utilizado para obtener la cantidad de usuarios registrados durante el periodo de tiempo seleccionado por el usuario.
        /// </summary>
        /// <param name="fechaInicioStr">Fecha incicial del periodo seleccionado para el conteo de usuarios registrados.</param>
        /// <param name="fechaFinStr">Fecha final del periodo seleccionado para el conteo de usuarios registrados.</param>
        /// <returns>Retorna un objeto "resultado" que indica si fue fue exitosa o fallida la operación.</returns>
        ResultadoProceso getCantidadUsuariosRegistrados(string fechaInicioStr, string fechaFinStr);
        /// <summary>
        /// Método utilizado para obtener la edad promedio de los pacientes.
        /// </summary>
        /// <returns>Retorna un objeto "resultado" que indica si fue fue exitosa o fallida la operación.</returns>
        ResultadoProceso getPromedioEdadPaciente();
        /// <summary>
        /// Método utilizado para obtener el promedio de citas atendidas por médico.
        /// </summary>
        /// <returns>Retorna un objeto "resultado" que indica si fue fue exitosa o fallida la operación.</returns>
        ResultadoProceso getPromedioCitasPorMedico();
        /// <summary>
        /// Método utilizado para obtener el promedio de recursos disponibles en un periodo de tiempo seleccionado por el usuario.
        /// </summary>
        /// <param name="fechaInicioStr">Fecha incicial del periodo seleccionado para la revisión de recursos disponibles.</param>
        /// <param name="fechaFinStr">Fecha final del periodo seleccionado para la revisión de recursos disponibles.</param>
        /// <returns>Retorna un objeto "resultado" que indica si fue fue exitosa o fallida la operación.</returns>
        ResultadoProceso getPromedioRecursosDisponibles(string fechaInicioStr, string fechaFinStr);
        /// <summary>
        /// Método utilizado para obtener el promedio de uso de la aplicación.
        /// </summary>
        /// <returns>Retorna un objeto "resultado" que indica si fue fue exitosa o fallida la operación.</returns>
        ResultadoProceso getPromedioUsoApp();
        /// <summary>
        /// Método utilizado para obtener el promedio de citas canceladas por médico en un periodo de tiempo seleccionado por el usuario.
        /// </summary>
        /// <param name="fechaInicioStr">Fecha incicial del periodo seleccionado para la revisión de citas canceladas.</param>
        /// <param name="fechaFinStr">Fecha final del periodo seleccionado para la revisión de citas canceladas.</param>
        /// <returns>Retorna un objeto "resultado" que indica si fue fue exitosa o fallida la operación.</returns>
        ResultadoProceso getPromedioCitasCanceladasPorMedico(string fechaInicioStr, string fechaFinStr);
        #endregion

        #region REPORTES CONFIGURADOS
        /// <summary>
        /// Método utilizado para obtener los atributos de la(s) entidad(es) seleccionada(s) por el usuario.
        /// </summary>
        /// <param name="selectedEntities">Parámetro que indica las entidades seleccionadas.</param>
        /// <returns>Retorna un objeto "resultado" que indica si fue fue exitosa o fallida la operación.</returns>
        ResultadoServicio<String> obtenerAtributos(List<string> selectedEntities);

        /// <summary>
        /// Método utilizado para obtener los atributos de la(s) entidad(es) seleccionada(s) por el usuario.
        /// </summary>
        /// <param name="selectedEntities">Parámetro que indica las entidades seleccionadas.</param>
        /// <returns>Retorna un objeto "resultado" que indica si fue fue exitosa o fallida la operación.</returns>
        ResultadoServicio<string> procesarQuery(List<DatosConfigurados> datosConfigurados);
        #endregion
    }
}
