using System;
using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.Results;
using DoctorWebServiciosWCF.Models.DAO;
using System.Collections.Generic;
using DoctorWebServiciosWCF.Controllers.Helpers;
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
    }
}
