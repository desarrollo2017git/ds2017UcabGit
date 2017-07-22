using System;
using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.Results;
using DoctorWebServiciosWCF.Models.DAO;
using System.Collections.Generic;
using DoctorWebServiciosWCF.Controllers.Helpers;
using System.Linq;

namespace DoctorWebServiciosWCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServicioCitas" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServicioCitas.svc or ServicioCitas.svc.cs at the Solution Explorer and start debugging.
    public class ServicioObservacionMedica : IServicioObservacionMedica
    {
        public IObservacionMedicaDAO Dao = new ObservacionMedicaDAO();
        public void DoWork()
        {
        }


        /// <summary>
        /// Metodo del servicio web para eliminar una Observacion medica
        /// </summary>
        /// <param name="observacionMedica">Observacion Medica a eliminar</param>
        /// <returns>Resultado del proceso</returns>
        public ResultadoProceso EliminarObservacionMedica(ObservacionMedica observacionMedica)
        {
            var resultado = new ResultadoProceso();
            try
            {
                Dao.EliminarObservacionMedica(observacionMedica);
                resultado.Inicializar("Ok");
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        /// <summary>
        /// Metodo del servicio web para guardar una observacion medica
        /// </summary>
        /// <param name="observacionMedica">observacion medica a guardar</param>
        /// <returns>Resultado proceso</returns>
        public ResultadoProceso GuardarObservacionMedica(ObservacionMedica observacionMedica)
        {
            var resultado = new ResultadoProceso();
            try
            {
                Dao.GuardarObservacionMedica(observacionMedica);
                resultado.Inicializar("Ok");
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

       
        /// <summary>
        /// Metodo del servicio web para obtener una observacion Medica
        /// </summary>
        /// <param name="observacionMedicaId">Identificador de la observacion medica </param>
        /// <returns>Resultado servicio observacion medica</returns>
        public ResultadoServicio<ObservacionMedica> ObtenerObservacionMedica(int observacionMedicaId)
        {
            var resultado = new ResultadoServicio<ObservacionMedica>();
            try
            {
               
                resultado.Inicializar(Dao.ObtenerObservacion(observacionMedicaId));
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        /// <summary>
        /// Metodo del servicio web para obtener un paciente especifico
        /// </summary>
        /// <param name="userId">Identificador de usuario del paciente</param>
        /// <returns>Resultado servicio paciente</returns>
        public ResultadoServicio<Paciente> ObtenerPaciente(string userId)
        {
            var resultado = new ResultadoServicio<Paciente>();
            try
            {
                resultado.Inicializar(Dao.ObtenerPaciente(userId));
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        /// <summary>
        /// Metodo del servicio web para obtener la lista de observacion medica
        /// </summary>
        /// <returns>Resultado servicio lista de observaciones</returns>
        public ResultadoServicio<List<ObservacionMedica>> ObtenerListaObservacionMedica()
        {
            var resultado = new ResultadoServicio<List<ObservacionMedica>>();
            try
            {
                resultado.Inicializar(Dao.ObtenerListaObservacionMedica());
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }


    }
}
