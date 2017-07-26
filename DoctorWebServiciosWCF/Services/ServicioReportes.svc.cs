using DoctorWebServiciosWCF.Controllers;
using DoctorWebServiciosWCF.Helpers;
using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.DAO;
using DoctorWebServiciosWCF.Models.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
        private readonly IReporteDAO dao = Utilidades.Instancia.Fabrica.CrearReporteDAO();

        public string DoWork(string codigo)
        {
            return "Hola mundo";
        }

        /// <summary>
        /// Método utilizado para indicar que operación se debe realizar según los parámetros indicados.
        /// </summary>
        /// <param name="codigo">Código que indica el id de la operación a realizar.</param>
        /// <param name="fechaInicio">Fecha de inicio del periodo seleccionado.</param>
        /// <param name="fechaFin">Fecha de fin del periodo seleccionado.</param>
        /// <returns>Resultado obtenido en la operación realizada.</returns>
        public ResultadoProceso ReportesPreestablecidos(string codigo, string fechaInicio, string fechaFin)
        {
            var resultado = Utilidades.Instancia.Fabrica.CrearResultadoProceso();

            try
            {
                int id = 0;
                if (!int.TryParse(codigo, out id))
                    throw new FormatException("El código debe ser un número.");

                if (!(id >= 1 && id <= 6))
                    throw Fabrica.Instancia.CrearExcepcion("No se puede realizar ninguna operación para el código " + codigo);

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
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }

            return resultado;
        }

        /// <summary>
        /// Método utilizado para llenar una lista de atributos, según el parámetro recibido. 
        /// </summary>
        /// <param name="selectedEntities">Parámetro que indica las entidades seleccionadas.</param>
        /// <returns>Objeto que contiene los atributos de las entidades seleccionadas.</returns>
        public ResultadoServicio<object> ObtenerAtributos(List<string> entidades)
        {
            var resultado = Fabrica.Instancia.CrearResultadoDe<object>();
            try
            {
                var obj = JsonConvert.SerializeObject(dao.obtenerAtributos(entidades));
                resultado.Inicializar(obj);
            }
            catch (Exception ex)
            {
                resultado.Mensaje = ex.Message;
            }

            return resultado;
        }

        public ResultadoServicio<string> ReportesConfigurados(List<DatosConfigurados> datosConfigurados)
        {
            var resultado = Fabrica.Instancia.CrearResultadoDe<string> ();
            resultado.Inicializar(dao.generarReporteConfigurado(datosConfigurados));

            return resultado;
        }

        /// <summary>
        /// Método utilizado para comprobar que las fechas ingresadas como parametros de los reportes 1, 4 y 6, sea válida.
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio del periodo seleccionado.</param>
        /// <param name="fechaFin">Fecha de fin del periodo seleccionado.</param>
        public void comprobarFecha(string fechaInicio, string fechaFin)
        {
            if (String.IsNullOrEmpty(fechaInicio) || String.IsNullOrEmpty(fechaFin))
                throw Utilidades.Instancia.Fabrica.CrearExcepcion("La fecha de inicio o fecha fin están vacías o son nulas");
        }
    }
}