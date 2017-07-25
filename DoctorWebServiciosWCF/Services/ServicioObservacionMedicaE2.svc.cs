﻿using System;
using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.Results;
using DoctorWebServiciosWCF.Models.DAO;
using System.Collections.Generic;
using DoctorWebServiciosWCF.Controllers.Helpers;
using System.Linq;

namespace DoctorWebServiciosWCF.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ServicioObservacionMedicaE2" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ServicioObservacionMedicaE2.svc o ServicioObservacionMedicaE2.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ServicioObservacionMedicaE2 : IServicioObservacionMedicaE2
    {
        public IObservacionMedicaE2DAO Dao = new ObservacionMedicaE2DAO();
        public void DoWork()
        {
        }

        /// <summary>
        /// Metodo del servicio web para eliminar una observacion medica
        /// </summary>
        /// <param name="observacionMedicaE2">observacion a eliminar</param>
        /// <returns>Resultado del proceso</returns>
        public ResultadoProceso EliminarObservacionMedicaE2(ObservacionMedicaE2 observacionMedicaE2)
        {
            var resultado = new ResultadoProceso();
            try
            {
                Dao.EliminarObservacionMedicaE2(observacionMedicaE2);
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
        /// <param name="observacionMedicaE2">observacion a guardar</param>
        /// <returns>Resultado proceso</returns>
        public ResultadoProceso GuardarObservacionMedicaE2(ObservacionMedicaE2 observacionMedicaE2)
        {
            var resultado = new ResultadoProceso();
            try
            {
                Dao.GuardarObservacionMedicaE2(observacionMedicaE2);
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
        public ResultadoServicio<List<ObservacionMedicaE2>> ObtenerSelectListObservacionMedicaE2()
        {
            var resultado = new ResultadoServicio<List<ObservacionMedicaE2>>();
            try
            {
                resultado.Inicializar(Dao.ObtenerSelectListObservacionMedicaE2());
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
