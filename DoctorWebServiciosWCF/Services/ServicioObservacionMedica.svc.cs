using System;
using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.Results;
using DoctorWebServiciosWCF.Models.DAO;
using System.Collections.Generic;
using DoctorWebServiciosWCF.Controllers.Helpers;
using System.Linq;

namespace DoctorWebServiciosWCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServicioObservacionMedica" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServicioObservacionMedica.svc or ServicioObservacionMedica.svc.cs at the Solution Explorer and start debugging.
    public class ServicioObservacionMedica : IServicioObservacionMedica
    {
        public IObservacionMedicaDAO Dao = new ObservacionMedicaDAO();
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
