using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DoctorWebASP.Controllers;
using System.Web.Mvc;
using DoctorWebASP.Models;
using System.Collections.Generic;
using DoctorWebASP.Controllers.Helpers;

namespace ReportesUnitTest
{
    [TestClass]
    public class G08ASPReportes
    {
        [TestMethod]
        public void ASPReporteCtrlIndex()
        {
            // Inicializar
            //var reportesIndexViewModel = new ReportesIndexViewModel();

            // Ejecutar
            ReportesController rc = new ReportesController();
            var resultado = rc.Index();

            // Evaluar
            Assert.IsNotNull(resultado);
            Assert.IsInstanceOfType(resultado, typeof(ViewResult));

        }

        [TestMethod]
        public void testgetPromedioCitasCanceladasPorMedico()
        {
            //ACT
            ReportesController rc = new ReportesController();
            var promedio = rc.getPromedioCitasCanceladasPorMedico("01-01-2017", "27-08-2017");

            //ASSERT
            Assert.IsNotNull(promedio);
            Assert.IsInstanceOfType(promedio, typeof(JsonResult));
        }

        [TestMethod]
        public void testGetCantidadUsuariosRegistrados()
        {

            //ACT
            ReportesController rc = new ReportesController();
            var cantidad = rc.getCantidadUsuariosRegistrados("01-01-2017", "27-08-2017");

            //ASSERT
            Assert.IsNotNull(cantidad);
            Assert.IsInstanceOfType(cantidad, typeof(JsonResult));
        }

        [TestMethod]
        public void getPromedioRecursosDisponibles_IsNotNull()
        {
            //ACT
            ReportesController rc = new ReportesController();
            var promedio = rc.getPromedioRecursosDisponibles("01-01-2017", "27-08-2017");

            //ASSERT
            Assert.IsNotNull(promedio);
            Assert.IsInstanceOfType(promedio, typeof(JsonResult));
        }

        [TestMethod]
        public void getReport()
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

            //ACT
            ReportesController rc = new ReportesController();
            var promedio = rc.getReport(datosConfigurados);

            //ASSERT
            Assert.IsNotNull(promedio);
            Assert.IsInstanceOfType(promedio, typeof(JsonResult));
        }

        [TestMethod]
        public void getAttributes()
        {
            List<string> entidades = new List<string>();
            entidades.Add("CentroMedico");
            entidades.Add("Medico");
            entidades.Add("Paciente");
            entidades.Add("RecursoHospitalario");

            //ACT
            ReportesController rc = new ReportesController();
            var promedio = rc.getAttributes(entidades);

            //ASSERT
            Assert.IsNotNull(promedio);
            Assert.IsInstanceOfType(promedio, typeof(JsonResult));
        }

        [TestMethod]
        public void getEntities()
        {
            //ACT
            ReportesController rc = new ReportesController();
            var promedio = rc.getEntities();

            //ASSERT
            Assert.IsNotNull(promedio);
            Assert.IsInstanceOfType(promedio, typeof(Dictionary<string,string>));
        }

        [TestMethod]
        public void ASPReporteConfiguradosIndex()
        {
            // Ejecutar
            ReportesController rc = new ReportesController();
            var resultado = rc.Configurados();

            // Evaluar
            Assert.IsNotNull(resultado);
            Assert.IsInstanceOfType(resultado, typeof(ViewResult));

        }

        [TestMethod]
        public void getReportException()
        {
            List<DatosConfigurados> datosConfigurados = new List<DatosConfigurados>();

            //ACT
            ReportesController rc = new ReportesController();
            var promedio = rc.getReport(throw Utilidades.Instancia.Fabrica.CrearExcepcion("Prueba unitaria."));

            //ASSERT
            Assert.IsNotNull(promedio);
            Assert.IsInstanceOfType(promedio, typeof(JsonResult));
        }
    }

}
