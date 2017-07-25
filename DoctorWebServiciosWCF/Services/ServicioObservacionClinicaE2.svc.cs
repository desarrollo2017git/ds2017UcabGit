using System;
using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.Results;
using DoctorWebServiciosWCF.Models.DAO;
using System.Collections.Generic;
using DoctorWebServiciosWCF.Controllers.Helpers;
using System.Linq;

namespace DoctorWebServiciosWCF.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ServicioObservacionClinicaE2" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ServicioObservacionClinicaE2.svc o ServicioObservacionClinicaE2.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ServicioObservacionClinicaE2 : IServicioObservacionClinicaE2
    {
        public IObservacionClinicaE2DAO Dao = new ObservacionClinicaE2DAO();
        public void DoWork()
        {
        }

        /// <summary>
        /// Metodo del servicio web para eliminar una observacion clinica
        /// </summary>
        /// <param name="observacionClinicaE2">observacion a eliminar</param>
        /// <returns>Resultado del proceso</returns>
        public ResultadoProceso EliminarObservacionClinicaE2(ObservacionClinicaE2 observacionClinicaE2)
        {
            var resultado = new ResultadoProceso();
            try
            {
                Dao.EliminarObservacionClinicaE2(observacionClinicaE2);
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
        /// <param name="observacionClinicaE2">observacion a guardar</param>
        /// <returns>Resultado proceso</returns>
        public ResultadoProceso GuardarObservacionClinicaE2(ObservacionClinicaE2 observacionClinicaE2)
        {
            var resultado = new ResultadoProceso();
            try
            {
                Dao.GuardarObservacionClinicaE2(observacionClinicaE2);
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
        public ResultadoServicio<List<ObservacionClinicaE2>> ObtenerSelectListObservacionClinicaE2()
        {
            var resultado = new ResultadoServicio<List<ObservacionClinicaE2>>();
            try
            {
                resultado.Inicializar(Dao.ObtenerSelectListObservacionClinicaE2());
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
