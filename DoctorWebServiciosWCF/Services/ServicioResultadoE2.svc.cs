using System;
using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.Results;
using DoctorWebServiciosWCF.Models.DAO;
using System.Collections.Generic;
using DoctorWebServiciosWCF.Controllers.Helpers;
using System.Linq;

namespace DoctorWebServiciosWCF.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ServicioResultadoE2" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ServicioResultadoE2.svc o ServicioResultadoE2.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ServicioResultadoE2 : IServicioResultadoE2
    {
        public IResultadoE2DAO Dao = new ResultadoE2DAO();
        public void DoWork()
        {
        }

        /// <summary>
        /// Metodo del servicio web para eliminar un resultado de examen medico
        /// </summary>
        /// <param name="resultadoE2">resultado a eliminar</param>
        /// <returns>Resultado del proceso</returns>
        public ResultadoProceso EliminarResultadoE2(ResultadoE2 resultadoE2)
        {
            var resultado = new ResultadoProceso();
            try
            {
                Dao.EliminarResultadoE2(resultadoE2);
                resultado.Inicializar("Todo bien");
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        /// <summary>
        /// Metodo del servicio web para guardar un resultado
        /// </summary>
        /// <param name="resultadoE2">resultado a guardar</param>
        /// <returns>Resultado proceso</returns>
        public ResultadoProceso GuardarResultadoE2(ResultadoE2 resultadoE2)
        {
            var resultado = new ResultadoProceso();
            try
            {
                Dao.GuardarResultadoE2(resultadoE2);
                resultado.Inicializar("Todo bien");
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        /// <summary>
        /// Metodo del servicio web para obtener la lista de resultados
        /// </summary>
        /// <returns>Resultado servicio lista de resultados</returns>
        public ResultadoServicio<List<ResultadoE2>> ObtenerSelectListResultadoE2()
        {
            var resultado = new ResultadoServicio<List<ResultadoE2>>();
            try
            {
                resultado.Inicializar(Dao.ObtenerSelectListResultadoE2());
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
