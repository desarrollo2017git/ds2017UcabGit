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
using System.Linq;

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

        /// <summary>
        /// Metodo del servicio web para eliminar una Cita
        /// </summary>
        /// <param name="cita">Cita a eliminar</param>
        /// <param name="calendario">Calendario para modificar su disponibilidad</param>
        /// <returns>Resultado del proceso</returns>
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

        /// <summary>
        /// Metodo del servicio web para guardar una cita
        /// </summary>
        /// <param name="cita">Cita a guardar</param>
        /// <param name="calendario">Calendario para modificar la disponibilidad</param>
        /// <returns>Resultado proceso</returns>
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

        /// <summary>
        /// Metodo del servicio web para obtener un calendario especifico
        /// </summary>
        /// <param name="calendarioId">Identificador del calendario</param>
        /// <returns>Resultado servicio de Calendario</returns>
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

        /// <summary>
        /// Metodo del servicio web para obtener un centro medico especifico
        /// </summary>
        /// <param name="centroMedicoId">Identificador del centro web </param>
        /// <returns>Resultado servicio centro medico</returns>
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

        /// <summary>
        /// Metodo del servicio web para obtener un centro medico por su RIF
        /// </summary>
        /// <param name="centroMedicoRif">RIF del centro medico</param>
        /// <returns>Resultado servicio centro medico</returns>
        public ResultadoServicio<CentroMedico> ObtenerCentroMedicoRif(string centroMedicoRif)
        {
            var resultado = new ResultadoServicio<CentroMedico>();
            try
            {
                //var dato = Dao.ObtenerCentroMedico(centroMedicoId);
                //resultado.Inicializar(Utilidades.Procesar(dato));
                resultado.Inicializar(Dao.ObtenerCentroMedicoRif(centroMedicoRif));
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        /// <summary>
        /// Metodo del servicio web para obtener una cita especifica
        /// </summary>
        /// <param name="id">Identificador de la cita</param>
        /// <returns>Resultado servicio cita</returns>
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

        /// <summary>
        /// Metodo del servicio web para obtener las citas de un doctor
        /// </summary>
        /// <param name="userId">Identificador de usuario del Doctor</param>
        /// <returns>Resultado servicio lista de citas</returns>
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

        /// <summary>
        /// Metodo del servicio web para obtener una lista de las especialidades medicas en un centro medico especifico
        /// </summary>
        /// <param name="cMedicoId">Identificador del centro medico</param>
        /// <returns>Resultado servicio lista de especialidades medicas</returns>
        public ResultadoServicio<List<EspecialidadMedica>> ObtenerEsMedicasPorMedicosEnCentroMedico(int cMedicoId)
        {
            var resultado = new ResultadoServicio<List<EspecialidadMedica>>();
            try
            {
                resultado.Inicializar(Dao.ObtenerEsMedicasPorMedicosEnCentroMedico(cMedicoId));
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        /// <summary>
        /// Metodo del servicio web para obtener una especialidad medica especifica
        /// </summary>
        /// <param name="espMedica">Identificador de la especialidad medica</param>
        /// <returns>Resultado Servicio</returns>
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

        /// <summary>
        /// Metodo del servicio web para obtener la lista de citas de un paciente
        /// </summary>
        /// <param name="userId">Identificador de usuario del paciente</param>
        /// <returns>Resultado servicio lista de citas</returns>
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

        /// <summary>
        /// Metodo del servicio web para obtener un medico especifico
        /// </summary>
        /// <param name="userId">Identificador de usuario del medico</param>
        /// <returns>Resultado servicio medico</returns>
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
        /// Metodo del servicio web para obtener la lista de los centros medicos
        /// </summary>
        /// <returns>Resultado servicio lista de centros medicos</returns>
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

        /// <summary>
        /// Metodo del servicio web para obtener una lista de los calendarios disponibles de un medico
        /// </summary>
        /// <param name="medicoId">Identificador del medico</param>
        /// <returns>Resultado servicio lista de calendario</returns>
        public ResultadoServicio<List<Calendario>> ObtenerListaDisponibilidad(int medicoId)
        {
            var resultado = new ResultadoServicio<List<Calendario>>();
            try
            {
                resultado.Inicializar(Dao.ObtenerListaDisponibilidad(medicoId));
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        /// <summary>
        /// Metodo del servicio web para un medico asignado a una cita especifica
        /// </summary>
        /// <param name="citaId">Identificador de la cita</param>
        /// <returns>Resultado servicio medico</returns>
        public ResultadoServicio<Medico> ObtenerMedicoAsignadoACita(int citaId)
        {
            var resultado = new ResultadoServicio<Medico>();
            try
            {
                resultado.Inicializar(Dao.ObtenerMedicoAsignadoACita(citaId));
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        /// <summary>
        /// Metodo del servicio web para obtener la especialidad medica de un doctor especifico
        /// </summary>
        /// <param name="medicoId">Identificador del medico</param>
        /// <returns>Resultado servicio especialidad medica</returns>
        public ResultadoServicio<EspecialidadMedica> ObtenerEspecialidadMedicaDelDoctor(int medicoId)
        {
            var resultado = new ResultadoServicio<EspecialidadMedica>();
            try
            {
                resultado.Inicializar(Dao.ObtenerEspecialidadMedicaDelDoctor(medicoId));
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }
            return resultado;
        }

        /// <summary>
        /// Metodo del servicio web para obtener la lista de medicos que trabajan en un centro medico especifico
        /// </summary>
        /// <param name="centroMedicoId">Identificador del centro medico</param>
        /// <param name="espMedica">Identificador de la especialidad medica</param>
        /// <returns>Resultado servicio lista medicos</returns>
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
