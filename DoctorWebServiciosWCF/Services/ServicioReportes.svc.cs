using DoctorWebServiciosWCF.Controllers;
using DoctorWebServiciosWCF.Helpers;
using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.DAO;
using DoctorWebServiciosWCF.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DoctorWebServiciosWCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ReporteService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ReporteService.svc or ReporteService.svc.cs at the Solution Explorer and start debugging.
    public class ServicioReportes : IServicioReportes
    {
        /// <summary>
        /// Instancia dao para interactuar con la Base de datos.
        /// </summary>
        private readonly IReporteDAO dao = Fabrica.CrearReporteDAO();

        public string DoWork(string codigo)
        {
            return "Hola mundo";
        }

        public ResultadoProceso Reportes(string tipo, string codigo, string fechaInicio, string fechaFin)
        {
            var resultado = Fabrica.CrearResultadoProceso();

            try
            {
                int id = 0;
                if (!int.TryParse(codigo, out id))
                    throw new FormatException("El código debe ser un número.");

                if (!tipo.Equals(ReporteTipo.preestablecido.ToString()) && !tipo.Equals(ReporteTipo.configurado.ToString()))
                    throw Fabrica.CrearExcepcion("No se puede realizar ninguna operación para el tipo " + tipo);

                if (tipo.Equals(ReporteTipo.preestablecido.ToString()) && !(id >= 1 && id <= 6))
                    throw Fabrica.CrearExcepcion("No se puede realizar ninguna operación para el código " + codigo);

                if (tipo.Equals(ReporteTipo.preestablecido.ToString()))
                {
                    switch (id)
                    {
                        case 1:
                            comprobarFecha(fechaInicio, fechaFin);
                            resultado.Inicializar(dao.getCantidadUsuariosRegistrados(fechaInicio, fechaFin).ToString());
                            break;
                        case 2:
                            resultado.Inicializar(dao.getPromedioEdadPaciente().ToString());
                            break;
                        case 3:
                            resultado.Inicializar(dao.getPromedioCitasPorMedico().ToString());
                            break;
                        case 4:
                            comprobarFecha(fechaInicio, fechaFin);
                            resultado.Inicializar(dao.getPromedioRecursosDisponibles(fechaInicio, fechaFin).ToString());
                            break;
                        case 5:
                            resultado.Inicializar(dao.getPromedioUsoApp().ToString());
                            break;
                        case 6:
                            comprobarFecha(fechaInicio, fechaFin);
                            resultado.Inicializar(dao.getPromedioCitasCanceladasPorMedico(fechaInicio, fechaFin).ToString());
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }

            return resultado;
        }

        public void comprobarFecha(string fechaInicio, string fechaFin)
        {
            if (String.IsNullOrEmpty(fechaInicio) || String.IsNullOrEmpty(fechaFin))
                throw Fabrica.CrearExcepcion("La fecha de inicio o fecha fin están vacías o son nulas");
        }
    }
}
