using System;
using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.Results;
using DoctorWebServiciosWCF.Models.DAO;
using System.Collections.Generic;
using DoctorWebServiciosWCF.Controllers.Helpers;
using System.Linq;

namespace DoctorWebServiciosWCF.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ServicioResultadoExamenMedico" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ServicioResultadoExamenMedico.svc o ServicioResultadoExamenMedico.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ServicioResultadoExamenMedico : IServicioResultadoExamenMedico
    {
        public IResultadoExamenMedicoDAO Dao = new ResultadoExamenMedicoDAO();
        public void DoWork()
        {
        }

        /// <summary>
        /// Metodo del servicio web para eliminar una observacion medica
        /// </summary>
        /// <param name="observacionMedica">observacion a eliminar</param>
        /// <returns>Resultado del proceso</returns>
        public ResultadoProceso EliminarResultadoExamenMedico(ResultadoExamenMedico resultadoExamenMedico)
        {
            var resultado = new ResultadoProceso();
            try
            {
                Dao.EliminarResultadoExamenMedico(resultadoExamenMedico);
                resultado.Inicializar("Todo bien");
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        /// <summary>
        /// Metodo del servicio web para guardar una observacion
        /// </summary>
        /// <param name="observacionMedica">observacion a guardar</param>
        /// <returns>Resultado proceso</returns>
        public ResultadoProceso GuardarResultadoExamenMedico(ResultadoExamenMedico resultadoExamenMedico)
        {
            var resultado = new ResultadoProceso();
            try
            {
                Dao.GuardarResultadoExamenMedico(resultadoExamenMedico);
                resultado.Inicializar("Todo bien");
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        /// <summary>
        /// Metodo del servicio web para obtener la lista de observaciones
        /// </summary>
        /// <returns>Resultado servicio lista de observaciones</returns>
        public ResultadoServicio<List<ResultadoExamenMedico>> ObtenerSelectListResultadoExamenMedico()
        {
            var resultado = new ResultadoServicio<List<ResultadoExamenMedico>>();
            try
            {
                resultado.Inicializar(Dao.ObtenerSelectListResultadoExamenMedico());
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
