using System;
using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.Results;
using DoctorWebServiciosWCF.Models.DAO;
using System.ServiceModel.Web;
using System.Net;
using System.Collections.Generic;
using Newtonsoft;
using Newtonsoft.Json;
using DoctorWebServiciosWCF.Controllers.Helpers;

namespace DoctorWebServiciosWCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServicioCitas" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServicioCitas.svc or ServicioCitas.svc.cs at the Solution Explorer and start debugging.
    public class ServicioCitas : IServicioCitas
    {
        public ICitasConsultasDAO Dao = new CitasDAO();
        public void DoWork()
        {
        }

        public ResultadoProceso EliminarCita(Cita cita, Calendario calendario)
        {
            var resultado = new ResultadoProceso();
            try
            {
                Dao.EliminarCita(cita, calendario);
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        public ResultadoProceso GuardarCita(Cita cita, Calendario calendario)
        {
            var resultado = new ResultadoProceso();
            try
            {
                Dao.GuardarCita(cita, calendario);
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        public ResultadoServicio<Calendario> ObtenerCalendario(int calendarioId)
        {
            var resultado = new ResultadoServicio<Calendario>();
            try
            {
                resultado.Inicializar(Dao.ObtenerCalendario(calendarioId));
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        public ResultadoServicio<CentroMedico> ObtenerCentroMedico(int centroMedicoId)
        {
            var resultado = new ResultadoServicio<CentroMedico>();
            try
            {
                //var dato = Dao.ObtenerCentroMedico(centroMedicoId);
                //resultado.Inicializar(Utilidades.Procesar(dato));
                resultado.Inicializar(Dao.ObtenerCentroMedico(centroMedicoId));
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        public ResultadoServicio<Cita> ObtenerCita(int id)
        {
            var resultado = new ResultadoServicio<Cita>();
            try
            {
                resultado.Inicializar(Dao.ObtenerCita(id));
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        public ResultadoServicio<List<Cita>> ObtenerCitasDoctor(string userId)
        {
            var resultado = new ResultadoServicio<List<Cita>>();
            try
            {
                //var dato = Dao.ObtenerCitasDoctor(userId);
                //resultado.Inicializar(Utilidades.Procesar(dato));
                resultado.Inicializar(Dao.ObtenerCitasDoctor(userId));
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        public ResultadoServicio<List<EspecialidadMedica>> ObtenerEsMedicasPorMedicosEnCentroMedico(CentroMedico cMedico)
        {
            var resultado = new ResultadoServicio<List<EspecialidadMedica>>();
            try
            {
                resultado.Inicializar(Dao.ObtenerEsMedicasPorMedicosEnCentroMedico(cMedico));
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        public ResultadoServicio<EspecialidadMedica> ObtenerEspecialidadMedica(int espMedica)
        {
            var resultado = new ResultadoServicio<EspecialidadMedica>();
            try
            {
                resultado.Inicializar(Dao.ObtenerEspecialidadMedica(espMedica));
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        public ResultadoServicio<List<Cita>> ObtenerListaCitas(string userId)
        {
            var resultado = new ResultadoServicio<List<Cita>>();
            try
            {
                resultado.Inicializar(Dao.ObtenerListaCitas(userId));
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        public ResultadoServicio<Medico> ObtenerMedico(string userId)
        {
            var resultado = new ResultadoServicio<Medico>();
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

        public ResultadoServicio<List<CentroMedico>> ObtenerSelectListCentrosMedicos()
        {
            var resultado = new ResultadoServicio<List<CentroMedico>>();
            try
            {
                resultado.Inicializar(Dao.ObtenerSelectListCentrosMedicos());
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        public ResultadoServicio<List<Medico>> ObtenerSelectListMedicosQueTrabajanEnCentroMedico(int centroMedicoId, int espMedica)
        {
            var resultado = new ResultadoServicio<List<Medico>>();
            try
            {
                resultado.Inicializar(Dao.ObtenerSelectListMedicosQueTrabajanEnCentroMedico(centroMedicoId, espMedica));
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }
    }
}
