using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DoctorWebASP.Controllers;
using System.Web.Mvc;

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

            //int actual = rc.pruebaunitaria();

            // Evaluar
            Assert.IsNotNull(resultado);
            Assert.IsInstanceOfType(resultado, typeof(ViewResult));

        }

        [TestMethod]
        public void testgetPromedioCitasCanceladasPorMedico()
        {
            //ACT
            ReportesController rc = new ReportesController();
            //var promedio = rc.getPromedioCitasCanceladasPorMedico("01-01-2017", "27-08-2017");

            //ASSERT
            //Assert.IsNotNull(promedio);
            //Assert.IsInstanceOfType(promedio, typeof(JsonResult));
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
    }

}
