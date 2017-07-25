using System;
using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.Results;
using DoctorWebServiciosWCF.Models.DAO;
using System.Collections.Generic;
using DoctorWebServiciosWCF.Controllers.Helpers;
using System.Linq;

namespace DoctorWebServiciosWCF.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ServicioObservacionDeAtencionClinica" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ServicioObservacionDeAtencionClinica.svc o ServicioObservacionDeAtencionClinica.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ServicioObservacionDeAtencionClinica : IServicioObservacionDeAtencionClinica
    {
        public IObservacionDeAtencionClinicaDAO Dao = new ObservacionDeAtencionClinicaDAO();
        public void DoWork()
        {
        }

        /// <summary>
        /// Metodo del servicio web para eliminar una observacion De Atencion Clinica
        /// </summary>
        /// <param name="observacionDeAtencionClinica">observacion a eliminar</param>
        /// <returns>Resultado del proceso</returns>
        public ResultadoProceso EliminarObservacionDeAtencionClinica(ObservacionDeAtencionClinica observacionDeAtencionClinica)
        {
            var resultado = new ResultadoProceso();
            try
            {
                Dao.EliminarObservacionDeAtencionClinica(observacionDeAtencionClinica);
                resultado.Inicializar("Todo bien");
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        /// <summary>
        /// Metodo del servicio web para guardar una observacion de Atencion Clinica
        /// </summary>
        /// <param name="observacionDeAtencionClinica">observacion a guardar</param>
        /// <returns>Resultado proceso</returns>
        public ResultadoProceso GuardarObservacionDeAtencionClinica(ObservacionDeAtencionClinica observacionDeAtencionClinica)
        {
            var resultado = new ResultadoProceso();
            try
            {
                Dao.GuardarObservacionDeAtencionClinica(observacionDeAtencionClinica);
                resultado.Inicializar("Todo bien");
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        /// <summary>
        /// Metodo del servicio web para obtener la lista de observaciones de atencion Clinica
        /// </summary>
        /// <returns>Resultado servicio lista de observaciones</returns>
        public ResultadoServicio<List<ObservacionDeAtencionClinica>> ObtenerSelectListObservacionDeAtencionClinica()
        {
            var resultado = new ResultadoServicio<List<ObservacionDeAtencionClinica>>();
            try
            {
                resultado.Inicializar(Dao.ObtenerSelectListObservacionDeAtencionClinica());
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
