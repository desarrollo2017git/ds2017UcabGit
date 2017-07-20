using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.DAO;
using DoctorWebServiciosWCF.Models.Service;

namespace DoctorWebServiciosWCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServicioCitas" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServicioCitas.svc or ServicioCitas.svc.cs at the Solution Explorer and start debugging.
    public class ServicioCitas : IServicioCitas
    {
        public ICitasConsultasDAO Dao = new CitasConsultasDAO();
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
            var resultado = new ResultadoServicio<Cita>();
            try
            {
                resultado.Contenido = Dao.ObtenerCita(id);
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        public ResultadoServicio<List<Cita>> ObtenerCitasDoctor(string userId)
        {
            throw new NotImplementedException();
        }

        public ResultadoServicio<List<EspecialidadMedica>> ObtenerEsMedicasPorMedicosEnCentroMedico(CentroMedico cMedico)
        {
            throw new NotImplementedException();
        }

        public ResultadoServicio<EspecialidadMedica> ObtenerEspecialidadMedica(int espMedica)
        {
            var resultado = new ResultadoServicio<EspecialidadMedica>();
            try
            {
                resultado.Contenido = Dao.ObtenerEspecialidadMedica(espMedica);
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        public ResultadoServicio<List<Cita>> ObtenerListaCitas(string userId)
        {
            throw new NotImplementedException();
        }

        public ResultadoServicio<Medico> ObtenerMedico(string userId)
        {
            var resultado = new ResultadoServicio<Medico>();
            try
            {
                resultado.Contenido = Dao.ObtenerMedico(userId);
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
                resultado.Contenido = Dao.ObtenerPaciente(userId);
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }


        public ResultadoServicio<List<CentroMedico>> ObtenerSelectListCentrosMedicos()
        {
            throw new NotImplementedException();
        }

        public ResultadoServicio<List<Medico>> ObtenerSelectListMedicosQueTrabajanEnCentroMedico(int centroMedicoId, int espMedica)
        {
            throw new NotImplementedException();
        }
    }
}
