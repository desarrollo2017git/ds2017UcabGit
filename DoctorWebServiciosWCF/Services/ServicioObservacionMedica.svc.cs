using System;
using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.Results;
using DoctorWebServiciosWCF.Models.DAO;
using System.Collections.Generic;
using DoctorWebServiciosWCF.Controllers.Helpers;
using System.Linq;

namespace DoctorWebServiciosWCF.Services
{
    
    public class ServicioObservacionMedica : IServicioObservacionMedica
    {
        public IObservacionMedicaDAO Dao = new ObservacionMedicaDAO();

        /// <summary>
        /// Metodo para probar que el servicio web funciona
        /// </summary>
        /// <returns>Resultado del proceso</returns>
        public void DoWork()
        {
        }

        /// <summary>
        /// Metodo del servicio web para eliminar una observacion medica
        /// </summary>
        /// <param name="observacionMedica">observacion a eliminar</param>
        /// <returns>Resultado del proceso</returns>
        public ResultadoProceso EliminarObservacionMedica(ObservacionMedica observacionMedica)
        {
            var resultado = new ResultadoProceso();
            try
            {
                Dao.EliminarObservacionMedica(observacionMedica);
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
        public ResultadoProceso GuardarObservacionMedica(ObservacionMedica observacionMedica)
        {
            var resultado = new ResultadoProceso();
            try
            {
                Dao.GuardarObservacionMedica(observacionMedica);
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
        public ResultadoServicio<List<ObservacionMedica>> ObtenerSelectListObservacionMedica()
        {
            var resultado = new ResultadoServicio<List<ObservacionMedica>>();
            try
            {
                resultado.Inicializar(Dao.ObtenerSelectListObservacionMedica());
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
