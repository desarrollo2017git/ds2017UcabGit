using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.DAO;
using DoctorWebServiciosWCF.Models.Results;

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
               resultado.Contenido = Dao.ObtenerCalendario(calendarioId);
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
                resultado.Contenido = Dao.ObtenerCentroMedico(centroMedicoId);
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        public ResultadoServicio<Cita> ObtenerCita(int id)
        {
            throw new NotImplementedException();
        }

        public ResultadoServicio<List<Cita>> ObtenerCitasDoctor(string userId)
        {
            throw new NotImplementedException();
        }

        public ResultadoServicio<EspecialidadMedica> ObtenerEspecialidadMedica(int espMedica)
        {
            throw new NotImplementedException();
        }

        public ResultadoServicio<List<Cita>> ObtenerListaCitas(string userId)
        {
            throw new NotImplementedException();
        }

        public ResultadoServicio<Medico> ObtenerMedico(string userId)
        {
            throw new NotImplementedException();
        }

        public ResultadoServicio<Paciente> ObtenerPaciente(string userId)
        {
            throw new NotImplementedException();
        }

        public ResultadoServicio<CentroMedico> ObtenerSingleCentroMedicoInt(int centroMedicoId)
        {
            throw new NotImplementedException();
        }

        public ResultadoServicio<CentroMedico> ObtenerSingleCentroMedico(string centroMedico)
        {

            throw new NotImplementedException();
        }
    }
}
