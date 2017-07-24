using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.DAO;
using DoctorWebServiciosWCF.Models.Results;
using System;
using System.Collections.Generic;

namespace DoctorWebServiciosWCF.Services
{
    public class ServicioPacientes : IServicioPacientes
    {
        public IPacienteDAO dao = new PacienteDAO();

        public void DoWork()
        {
        }

        /// <summary>
        /// Metodo del servicio web para eliminar un paciente
        /// </summary>
        /// <param name="paciente">paciente a eliminar</param>
        /// <returns>Resultado del proceso</returns>
        public ResultadoProceso EliminarPaciente(Paciente paciente)
        {
            var resultado = new ResultadoProceso();
            try
            {
                dao.EliminarPaciente(paciente);
                resultado.Inicializar("Todo bien");
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        /// <summary>
        /// Metodo del servicio web para guardar un paciente
        /// </summary>
        /// <param name="paciente">paciente a guardar</param>
        /// <returns>Resultado proceso</returns>
        public ResultadoProceso GuardarPaciente(Paciente paciente)
        {
            var resultado = new ResultadoProceso();
            try
            {
                dao.GuardarPaciente(paciente);
                resultado.Inicializar("Todo bien");
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
                resultado.Inicializar(dao.ObtenerPaciente(userId));
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        /// <summary>
        /// Metodo del servicio web para obtener la lista de los seguros
        /// </summary>
        /// <returns>Resultado servicio lista de centros medicos</returns>
        public ResultadoServicio<List<Seguro>> ObtenerSelectListSeguros()
        {
            var resultado = new ResultadoServicio<List<Seguro>>();
            try
            {
                resultado.Inicializar(dao.ObtenerSelectListSeguros());
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }


    }
}
