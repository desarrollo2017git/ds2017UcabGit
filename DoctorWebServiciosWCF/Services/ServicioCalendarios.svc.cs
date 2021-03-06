﻿using System;
using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.Results;
using DoctorWebServiciosWCF.Models.DAO;
using System.Collections.Generic;
using DoctorWebServiciosWCF.Helpers;
using System.Linq;

namespace DoctorWebServiciosWCF.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ServicioCalendarios" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ServicioCalendarios.svc o ServicioCalendarios.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ServicioCalendarios : IServicioCalendarios
    {


        public ICalendariosDAO Dao = new CalendariosDAO();
        public void DoWork()
        {
        }

        /// <summary>
        /// Metodo del servicio web que se encarga de registrar un calendario
        /// </summary>
        /// <param name="calendario"> Objeto Calendario </param>
        /// <returns> Resultado servicio de Calendario </returns>
        public ResultadoServicio<Calendario> GuardarCalendario(Calendario calendario)
        {
            var resultado = new ResultadoServicio<Calendario>();
            try
            {
                resultado.Inicializar(Dao.GuardarCalendario(calendario));
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        /// <summary>
        /// Metodo del servicio web que se encarga de eliminar un calendario
        /// </summary>
        /// <param name="calendario"> Objeto Calendario </param>
        /// <returns> Resultado servicio de Calendario </returns>
        public ResultadoProceso EliminarCalendario(Calendario calendario)
        {
            var resultado = new ResultadoProceso();
            try
            {
                Dao.EliminarCalendario(calendario);
                resultado.Inicializar("Todo bien");
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        /// <summary>
        /// Metodo del servicio web que se encarga de obtener el medico asociado al id dado
        /// </summary>
        /// <param name="userId"> Codigo identificador del medico </param>
        /// <returns> Resultado servicio de Medico </returns>
        public ResultadoServicio<List<Medico>> ObtenerMedico(string userId)
        {
            var resultado = new ResultadoServicio<List<Medico>>();
            try
            {
                resultado.Inicializar(Dao.ObtenerMedico(userId));
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        /// <summary>
        /// Metodo del servicio web que se encarga de obtener el paciente asociado al id dado
        /// </summary>
        /// <param name="userId"> Codigo identificador del paciente </param>
        /// <returns> Resultado servicio de Paciente </returns>
        public ResultadoServicio<List<Paciente>> ObtenerPaciente(string userId)
        {
            var resultado = new ResultadoServicio<List<Paciente>>();
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
        /// Metodo del servicio web que se encarga de obtener el tiempo libre de un medico especifico
        /// </summary>
        /// <param name="medicoId"> Codigo identificador del medico </param>
        /// <returns> Resultado servicio de Calendario </returns>
        public ResultadoServicio<List<Calendario>> ObtenerTiempoDoctor(int medicoId)
        {
            var resultado = new ResultadoServicio<List<Calendario>>();
            try
            {
                resultado.Inicializar(Dao.ObtenerTiempoDoctor(medicoId));
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        /// <summary>
        /// Metodo del servicio web que se encarga de obtener las citas asociadas a un medico específico
        /// </summary>
        /// <param name="medicoId"> Codigo identificador del medico </param>
        /// <returns> Resultado servicio de Calendario </returns>
        public ResultadoServicio<List<Calendario>> ObtenerCitasDoctor(int medicoId)
        {
            var resultado = new ResultadoServicio<List<Calendario>>();
            try
            {
                resultado.Inicializar(Dao.ObtenerCitasDoctor(medicoId));
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        /// <summary>
        /// Metodo del servicio web que se encarga de obtener el paciente asociado a un calendario específico
        /// </summary>
        /// <param name="calendarioId"> Codigo identificador del calendario </param>
        /// <returns> Resultado servicio de Paciente </returns>
        public ResultadoServicio<Paciente> ObtenerPacienteCalendario(int calendarioId)
        {
            var resultado = new ResultadoServicio<Paciente>();
            try
            {
                resultado.Inicializar(Dao.ObtenerPacienteCalendario(calendarioId));
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        /// <summary>
        /// Metodo del servicio web que se encarga de obtener las citas asociadas a un paciente específico
        /// </summary>
        /// <param name="pacienteId"> Codigo identificador del paciente </param>
        /// <returns> Resultado servicio de Calendario </returns>
        public ResultadoServicio<List<Calendario>> ObtenerCitasPaciente(int pacienteId)
        {
            var resultado = new ResultadoServicio<List<Calendario>>();
            try
            {
                resultado.Inicializar(Dao.ObtenerCitasPaciente(pacienteId));
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        /// <summary>
        /// Metodo del servicio web que se encarga de obtener el medico asociado a un calendario específico
        /// </summary>
        /// <param name="calendarioId"> Codigo identificador del calendario </param>
        /// <returns> Resultado servicio de Medico </returns>
        public ResultadoServicio<Medico> ObtenerMedicoCalendario(int calendarioId)
        {
            var resultado = new ResultadoServicio<Medico>();
            try
            {
                resultado.Inicializar(Dao.ObtenerMedicoCalendario(calendarioId));
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }
    }

}
