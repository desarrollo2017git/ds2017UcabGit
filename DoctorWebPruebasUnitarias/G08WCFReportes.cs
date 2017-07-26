using DoctorWebServiciosWCF.Helpers;
using DoctorWebServiciosWCF.Models;
using DoctorWebServiciosWCF.Models.DAO;
using DoctorWebServiciosWCF.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWebPruebasUnitarias
{
    [TestClass]
    public class G08WCFReportes
    {
        #region DoctorWebServiciosWCF
        private Mock<IReporteDAO> mockDao { get; set; }
        private Mock<IUtilidades> mockUtils { get; set; }
        private Mock<IFabrica> mockFabric { get; set; }

        [TestInitialize]
        public void Inicializar()
        {
            this.mockDao = new Mock<IReporteDAO>();
            this.mockUtils = new Mock<IUtilidades>();
            this.mockFabric = new Mock<IFabrica>();
        }
        #endregion

        [TestMethod]
        public void ReportePreestablecido1Caso1()
        {
            string fechaInicioStr = "01-01-2017";
            string fechaFinStr = "08-01-2017";
            string fechaInicio = "01-01-2017";
            string fechaFin = "08-01-2017";
            string codigo = "1";
            int numero = 0;
            //Inicializar
            this.mockDao
                .Setup(dao => dao.getCantidadUsuariosRegistrados(fechaInicioStr,fechaFinStr))
                .Returns(() => { return numero; });

            this.mockFabric
                .Setup(fabric => fabric.CrearResultadoProceso())
                .Returns(() => { return Utilidades.Instancia.Fabrica.CrearResultadoProceso(); });

            this.mockUtils
                .Setup(dao => dao.Fabrica)
                .Returns(() => { return this.mockFabric.Object; });

            IServicioReportes servicio = new ServicioReportes(this.mockDao.Object, this.mockUtils.Object);

            //Ejecutar
            var resultado = servicio.ReportesPreestablecidos(codigo,fechaInicio,fechaFin);

            //Evaluar
            Assert.IsTrue(resultado.SinProblemas);
        }

        [TestMethod]
        public void ReportePreestablecido1Caso2()
        {
            string fechaInicioStr = String.Empty;
            string fechaFinStr = String.Empty;
            string fechaInicio = String.Empty;
            string fechaFin = String.Empty;
            string codigo = "1";
            int numero = 0;
            //Inicializar
            this.mockDao
                .Setup(dao => dao.getCantidadUsuariosRegistrados(fechaInicioStr, fechaFinStr))
                .Returns(() => { return numero; });

            this.mockFabric
                .Setup(fabric => fabric.CrearResultadoProceso())
                .Returns(() => { return Utilidades.Instancia.Fabrica.CrearResultadoProceso(); });

            this.mockUtils
                .Setup(dao => dao.Fabrica)
                .Returns(() => { return this.mockFabric.Object; });

            IServicioReportes servicio = new ServicioReportes(this.mockDao.Object, this.mockUtils.Object);

            //Ejecutar
            var resultado = servicio.ReportesPreestablecidos(codigo, fechaInicio, fechaFin);

            //Evaluar
            Assert.IsFalse(resultado.SinProblemas);
        }

        [TestMethod]
        public void ReportePreestablecido2Caso1()
        {
            string codigo = "2";
            double numero = 0;
            //Inicializar
            this.mockDao
                .Setup(dao => dao.getPromedioEdadPaciente())
                .Returns(() => { return numero; });

            this.mockFabric
                .Setup(fabric => fabric.CrearResultadoProceso())
                .Returns(() => { return Utilidades.Instancia.Fabrica.CrearResultadoProceso(); });

            this.mockUtils
                .Setup(dao => dao.Fabrica)
                .Returns(() => { return this.mockFabric.Object; });

            IServicioReportes servicio = new ServicioReportes(this.mockDao.Object, this.mockUtils.Object);

            //Ejecutar
            var resultado = servicio.ReportesPreestablecidos(codigo, null, null);

            //Evaluar
            Assert.IsTrue(resultado.SinProblemas);
        }

        [TestMethod]
        public void ReportePreestablecido2Caso2()
        {
            string codigo = "Dos";
            double numero = 14;
            //Inicializar
            this.mockDao
                .Setup(dao => dao.getPromedioEdadPaciente())
                .Returns(() => { return numero; });

            this.mockFabric
                .Setup(fabric => fabric.CrearResultadoProceso())
                .Returns(() => { return Utilidades.Instancia.Fabrica.CrearResultadoProceso(); });

            this.mockUtils
                .Setup(dao => dao.Fabrica)
                .Returns(() => { return this.mockFabric.Object; });

            IServicioReportes servicio = new ServicioReportes(this.mockDao.Object, this.mockUtils.Object);

            //Ejecutar
            var resultado = servicio.ReportesPreestablecidos(codigo, null, null);

            //Evaluar
            Assert.IsFalse(resultado.SinProblemas);
        }

        [TestMethod]
        public void ReportePreestablecido3Caso1()
        {
            string codigo = "3";
            int numero = 0;
            //Inicializar
            this.mockDao
                .Setup(dao => dao.getPromedioCitasPorMedico())
                .Returns(() => { return numero; });

            this.mockFabric
                .Setup(fabric => fabric.CrearResultadoProceso())
                .Returns(() => { return Utilidades.Instancia.Fabrica.CrearResultadoProceso(); });

            this.mockUtils
                .Setup(dao => dao.Fabrica)
                .Returns(() => { return this.mockFabric.Object; });

            IServicioReportes servicio = new ServicioReportes(this.mockDao.Object, this.mockUtils.Object);

            //Ejecutar
            var resultado = servicio.ReportesPreestablecidos(codigo, null, null);

            //Evaluar
            Assert.IsTrue(resultado.SinProblemas);
        }

        [TestMethod]
        public void ReportePreestablecido3Caso2()
        {
            string codigo = "3";
            double numero = 14;
            //Inicializar
            this.mockDao
                .Setup(dao => dao.getPromedioCitasPorMedico())
                .Returns(() => { return numero; });

            this.mockFabric
                .Setup(fabric => fabric.CrearResultadoProceso())
                .Returns(() => { return Utilidades.Instancia.Fabrica.CrearResultadoProceso(); });

            this.mockUtils
                .Setup(dao => dao.Fabrica)
                .Returns(() => { return this.mockFabric.Object; });

            IServicioReportes servicio = new ServicioReportes(this.mockDao.Object, this.mockUtils.Object);

            //Ejecutar
            var resultado = servicio.ReportesPreestablecidos(codigo, null, null);

            //Evaluar
            Assert.AreNotEqual(resultado.Mensaje, 13.ToString());
        }

        [TestMethod]
        public void ReportePreestablecido4Caso1()
        {
            string fechaInicioStr = "01-01-2017";
            string fechaFinStr = "08-01-2017";
            string fechaInicio = "01-01-2017";
            string fechaFin = "08-01-2017";
            string codigo = "4";
            int numero = 0;
            //Inicializar
            this.mockDao
                .Setup(dao => dao.getPromedioRecursosDisponibles(fechaInicioStr, fechaFinStr))
                .Returns(() => { return numero; });

            this.mockFabric
                .Setup(fabric => fabric.CrearResultadoProceso())
                .Returns(() => { return Utilidades.Instancia.Fabrica.CrearResultadoProceso(); });

            this.mockUtils
                .Setup(dao => dao.Fabrica)
                .Returns(() => { return this.mockFabric.Object; });

            IServicioReportes servicio = new ServicioReportes(this.mockDao.Object, this.mockUtils.Object);

            //Ejecutar
            var resultado = servicio.ReportesPreestablecidos(codigo, fechaInicio, fechaFin);

            //Evaluar
            Assert.IsTrue(resultado.SinProblemas);
        }

        [TestMethod]
        public void ReportePreestablecido4Caso2()
        {
            string fechaInicioStr = "01-01-2017";
            string fechaFinStr = "08-01-2017";
            string fechaInicio = "01-01-2017";
            string fechaFin = "08-01-2017";
            string codigo = "8";
            int numero = 0;
            //Inicializar
            this.mockDao
                .Setup(dao => dao.getPromedioRecursosDisponibles(fechaInicioStr, fechaFinStr))
                .Returns(() => { return numero; });

            this.mockFabric
                .Setup(fabric => fabric.CrearResultadoProceso())
                .Returns(() => { return Utilidades.Instancia.Fabrica.CrearResultadoProceso(); });

            this.mockUtils
                .Setup(dao => dao.Fabrica)
                .Returns(() => { return this.mockFabric.Object; });

            IServicioReportes servicio = new ServicioReportes(this.mockDao.Object, this.mockUtils.Object);

            //Ejecutar
            var resultado = servicio.ReportesPreestablecidos(codigo, fechaInicio, fechaFin);

            //Evaluar
            Assert.IsFalse(resultado.SinProblemas);
        }

        [TestMethod]
        public void ReportePreestablecido5Caso1()
        {
            string fechaInicioStr = String.Empty;
            string fechaFinStr = String.Empty;
            string fechaInicio = String.Empty;
            string fechaFin = String.Empty;
            string codigo = "5";
            int numero = 1;
            //Inicializar
            this.mockDao
                .Setup(dao => dao.getPromedioUsoApp())
                .Returns(() => { return numero; });

            this.mockFabric
                .Setup(fabric => fabric.CrearResultadoProceso())
                .Returns(() => { return Utilidades.Instancia.Fabrica.CrearResultadoProceso(); });

            this.mockUtils
                .Setup(dao => dao.Fabrica)
                .Returns(() => { return this.mockFabric.Object; });

            IServicioReportes servicio = new ServicioReportes(this.mockDao.Object, this.mockUtils.Object);

            //Ejecutar
            var resultado = servicio.ReportesPreestablecidos(codigo, fechaInicio, fechaFin);

            //Evaluar
            Assert.IsTrue(resultado.SinProblemas);
        }

        [TestMethod]
        public void ReportePreestablecido5Caso2()
        {
            string fechaInicioStr = String.Empty;
            string fechaFinStr = String.Empty;
            string fechaInicio = String.Empty;
            string fechaFin = String.Empty;
            string codigo = "5";
            double numero = 2;
            //Inicializar
            this.mockDao
                .Setup(dao => dao.getPromedioUsoApp())
                .Returns(() => { return numero; });

            this.mockFabric
                .Setup(fabric => fabric.CrearResultadoProceso())
                .Returns(() => { return Utilidades.Instancia.Fabrica.CrearResultadoProceso(); });

            this.mockUtils
                .Setup(dao => dao.Fabrica)
                .Returns(() => { return this.mockFabric.Object; });

            IServicioReportes servicio = new ServicioReportes(this.mockDao.Object, this.mockUtils.Object);

            //Ejecutar
            var resultado = servicio.ReportesPreestablecidos(codigo, fechaInicio, fechaFin);

            //Evaluar
            Assert.AreNotEqual(resultado.Mensaje, 1.ToString());
        }

        [TestMethod]
        public void ReportePreestablecido6Caso1()
        {
            string fechaInicioStr = "01-01-2017";
            string fechaFinStr = "08-01-2017";
            string fechaInicio = "01-01-2017";
            string fechaFin = "08-01-2017";
            string codigo = "6";
            int numero = 1;
            //Inicializar
            this.mockDao
                .Setup(dao => dao.getPromedioCitasCanceladasPorMedico(fechaInicioStr, fechaFinStr))
                .Returns(() => { return numero; });

            this.mockFabric
                .Setup(fabric => fabric.CrearResultadoProceso())
                .Returns(() => { return Utilidades.Instancia.Fabrica.CrearResultadoProceso(); });

            this.mockUtils
                .Setup(dao => dao.Fabrica)
                .Returns(() => { return this.mockFabric.Object; });

            IServicioReportes servicio = new ServicioReportes(this.mockDao.Object, this.mockUtils.Object);

            //Ejecutar
            var resultado = servicio.ReportesPreestablecidos(codigo, fechaInicio, fechaFin);

            //Evaluar
            Assert.AreEqual(resultado.Mensaje, numero.ToString());
        }

        [TestMethod]
        public void ReportePreestablecido6Caso2()
        {
            string fechaInicioStr = String.Empty;
            string fechaFinStr = String.Empty;
            string fechaInicio = String.Empty;
            string fechaFin = String.Empty;
            string codigo = "-1";
            double numero = 2;
            //Inicializar
            this.mockDao
                .Setup(dao => dao.getPromedioCitasCanceladasPorMedico(fechaInicioStr, fechaFinStr))
                .Returns(() => { return numero; });

            this.mockFabric
                .Setup(fabric => fabric.CrearResultadoProceso())
                .Returns(() => { return Utilidades.Instancia.Fabrica.CrearResultadoProceso(); });

            this.mockUtils
                .Setup(dao => dao.Fabrica)
                .Returns(() => { return this.mockFabric.Object; });

            IServicioReportes servicio = new ServicioReportes(this.mockDao.Object, this.mockUtils.Object);

            //Ejecutar
            var resultado = servicio.ReportesPreestablecidos(codigo, fechaInicio, fechaFin);

            //Evaluar
            Assert.IsFalse(resultado.SinProblemas);
        }

        [TestMethod]
        public void ReporteConfiguradoAtributosCaso1()
        {
            List<string> entidades = new List<string>();
            entidades.Add("CentroMedico");
            entidades.Add("Medico");
            entidades.Add("Paciente");
            entidades.Add("RecursoHospitalario");

            Dictionary<string, object> atributos = new Dictionary<string, object>();

            //Inicializar
            this.mockDao
                .Setup(dao => dao.obtenerAtributos(entidades))
                .Returns(() => { return atributos; });

            this.mockFabric
                .Setup(fabric => fabric.CrearResultadoProceso())
                .Returns(() => { return Utilidades.Instancia.Fabrica.CrearResultadoProceso(); });

            this.mockUtils
                .Setup(dao => dao.Fabrica)
                .Returns(() => { return this.mockFabric.Object; });

            IServicioReportes servicio = new ServicioReportes(this.mockDao.Object, this.mockUtils.Object);

            //Ejecutar
            var resultado = servicio.ObtenerAtributos(entidades);

            //Evaluar
            Assert.IsTrue(resultado.SinProblemas);
        }

        [TestMethod]
        public void ReporteConfiguradoAtributosCaso2()
        {
            List<string> entidades = new List<string>();

            Dictionary<string, object> atributos = new Dictionary<string, object>();

            //Inicializar
            this.mockDao
                .Setup(dao => dao.obtenerAtributos(entidades))
                .Returns(() => { throw Utilidades.Instancia.Fabrica.CrearExcepcion("Prueba unitaria."); });

            this.mockFabric
                .Setup(fabric => fabric.CrearResultadoDe<Dictionary<string,object>>())
                .Returns(() => { return Utilidades.Instancia.Fabrica.CrearResultadoDe<Dictionary<string,object>>();});

            this.mockUtils
                .Setup(dao => dao.Fabrica)
                .Returns(() => { return this.mockFabric.Object; });

            IServicioReportes servicio = new ServicioReportes(this.mockDao.Object, this.mockUtils.Object);

            //Ejecutar
            var resultado = servicio.ObtenerAtributos(entidades);

            //Evaluar
            Assert.IsFalse(resultado.SinProblemas);
        }

        [TestMethod]
        public void ReporteConfiguradoGenerarReporteCaso1()
        {
            List<DatosConfigurados> datosConfigurados = new List<DatosConfigurados>();
            DatosConfigurados dcUno = new DatosConfigurados();
            DatosConfigurados dcDos = new DatosConfigurados();
            dcUno.Instancia = "Paciente";
            dcUno.Atributo = "Nombre";
            dcDos.Instancia = "Medico";
            dcDos.Atributo = "Sueldo";
            dcDos.Condicional = "<";
            dcDos.Valor = "1";
            datosConfigurados.Add(dcUno);
            datosConfigurados.Add(dcDos);

            string respuesta = "Respuesta";

            //Inicializar
            this.mockDao
                .Setup(dao => dao.generarReporteConfigurado(datosConfigurados))
                .Returns(() => { return respuesta; });

            this.mockFabric
                .Setup(fabric => fabric.CrearResultadoDe<string>())
                .Returns(() => { return Utilidades.Instancia.Fabrica.CrearResultadoDe<string>(); });

            this.mockUtils
                .Setup(dao => dao.Fabrica)
                .Returns(() => { return this.mockFabric.Object; });

            IServicioReportes servicio = new ServicioReportes(this.mockDao.Object, this.mockUtils.Object);

            //Ejecutar
            var resultado = servicio.ReportesConfigurados(datosConfigurados);

            //Evaluar
            Assert.IsTrue(resultado.SinProblemas);
        }

        [TestMethod]
        public void ReporteConfiguradoGenerarReporteCaso2()
        {
            List<DatosConfigurados> datosConfigurados = new List<DatosConfigurados>();
            DatosConfigurados dcUno = new DatosConfigurados();
            DatosConfigurados dcDos = new DatosConfigurados();
            dcUno.Instancia = "Paciente";
            dcUno.Atributo = "Nombre";
            dcDos.Instancia = "Medico";
            dcDos.Atributo = "Sueldo";
            dcDos.Condicional = "<";
            dcDos.Valor = "1";
            datosConfigurados.Add(dcUno);
            datosConfigurados.Add(dcDos);

            string respuesta = "Respuesta";

            //Inicializar
            this.mockDao
                .Setup(dao => dao.generarReporteConfigurado(datosConfigurados))
                .Returns(() => { throw Utilidades.Instancia.Fabrica.CrearExcepcion("Esperando corregir el proyecto."); });

            this.mockFabric
                .Setup(fabric => fabric.CrearResultadoDe<string>())
                .Returns(() => { return Utilidades.Instancia.Fabrica.CrearResultadoDe<string>(); });

            this.mockUtils
                .Setup(dao => dao.Fabrica)
                .Returns(() => { return this.mockFabric.Object; });

            IServicioReportes servicio = new ServicioReportes(this.mockDao.Object, this.mockUtils.Object);

            //Ejecutar
            var resultado = servicio.ReportesConfigurados(datosConfigurados);

            //Evaluar
            Assert.IsFalse(resultado.SinProblemas);
        }
    }
}