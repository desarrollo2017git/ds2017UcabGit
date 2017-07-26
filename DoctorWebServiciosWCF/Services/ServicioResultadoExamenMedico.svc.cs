using System;
using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.Results;
using DoctorWebServiciosWCF.Models.DAO;
using System.Collections.Generic;
using DoctorWebServiciosWCF.Controllers.Helpers;
using System.Linq;

namespace DoctorWebServiciosWCF.Services
{
   
    public class ServicioResultadoExamenMedico : IServicioResultadoExamenMedico
    {
        public IResultadoExamenMedicoDAO Dao = new ResultadoExamenMedicoDAO();
        public void DoWork()
        {
        }

        /// <summary>
        /// Metodo del servicio web para eliminar un resultado de examen medico
        /// </summary>
        /// <param name="resultadoExamenMedico">resultado a eliminar</param>
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
        /// Metodo del servicio web para guardar un resultado medico
        /// </summary>
        /// <param name="resultadoExamenMedico">resultado a guardar</param>
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
        /// Metodo del servicio web para obtener la lista de resultados de examen medico
        /// </summary>
        /// <returns>Resultado servicio lista de resultados</returns>
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
