using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using DoctorWebServiciosWCF.Services;
using DoctorWebServiciosWCF.Models.DAO;
using DoctorWebServiciosWCF.Models;
using static DoctorWebPruebasUnitarias.Extensiones;
using DoctorWebServiciosWCF.Helpers;
using System.Collections.Specialized;
using DoctorWebServiciosWCF.Controllers.Helpers;

namespace DoctorWebPruebasUnitarias
{
    [TestClass]
    public class G09WCFNotificaciones
    {
        #region DoctorWebServiciosWCF
        private Mock<INotificacionDAO> mockDao { get; set; }
        private Mock<IUtilidades> mockUtils { get; set; }

        private Mock<IFabrica> mockFabric { get; set; }

        [TestInitialize]
        public void Inicializar()
        {
            this.mockDao = new Mock<INotificacionDAO>();
            this.mockUtils = new Mock<IUtilidades>();
            this.mockFabric = new Mock<IFabrica>();
        }

        [TestMethod]
        public void CLIG0SrEnviarCaso1()
        {
            //Inicializar
            string nombreNotificacion = String.Empty;
            string correo = "prueba@unitaria.com";
            NameValueCollection coleccion = new NameValueCollection();
            coleccion.Add("Prueba", "Unitaria");

            Notificacion notificacion = new Notificacion { Nombre = nombreNotificacion };
            this.mockDao
                .Setup(dao => dao.Obtener(nombreNotificacion))
                .Returns(() => { return notificacion; });

            this.mockUtils
                .Setup(dao => dao.ObtenerCabeceraActual())
                .Returns(() => { return coleccion; });

            this.mockFabric
                .Setup(fabric => fabric.CrearResultadoProceso())
                .Returns(() => { return Utilidades.Instancia.Fabrica.CrearResultadoProceso(); });

            this.mockFabric
                .Setup(fabric => fabric.CrearDiccionario<string, object>())
                .Returns(() => { return Utilidades.Instancia.Fabrica.CrearDiccionario<string, object>(); });

            this.mockUtils
                .Setup(dao => dao.Fabrica)
                .Returns(() => { return this.mockFabric.Object; });

            IServicioNotificaciones servicio = new ServicioNotificaciones(this.mockDao.Object, this.mockUtils.Object);

            //Ejecutar
            var resultado = servicio.Enviar(nombreNotificacion, correo);

            //Evaluar
            Assert.IsTrue(resultado.SinProblemas);
        }

        [TestMethod]
        public void CLIG0SrEnviarCaso2()
        {
            //Inicializar
            string nombreNotificacion = String.Empty;
            string correo = "prueba@unitaria.com";
            NameValueCollection coleccion = new NameValueCollection();
            coleccion.Add("Prueba", "Unitaria");

            Notificacion notificacion = new Notificacion { Nombre = nombreNotificacion };
            this.mockDao
                .Setup(dao => dao.Obtener(nombreNotificacion))
                .Returns(() => { throw Utilidades.Instancia.Fabrica.CrearExcepcion("Prueba Unitaria"); });

            this.mockUtils
                .Setup(dao => dao.ObtenerCabeceraActual())
                .Returns(() => { return coleccion; });


            IServicioNotificaciones servicio = new ServicioNotificaciones(this.mockDao.Object, Utilidades.Instancia);

            //Ejecutar
            var resultado = servicio.Enviar(nombreNotificacion, correo);

            //Evaluar
            Assert.IsFalse(resultado.SinProblemas);
        }

        [TestMethod]
        public void CLIG0SrvObtenerCaso1()
        {
            //Inicializar
            int codigo = 1;
            Notificacion notificacion = new Notificacion { NotificacionId = codigo };
            this.mockDao
                .Setup(dao => dao.Obtener(codigo))
                .Returns(() => { return notificacion; });

            IServicioNotificaciones servicio = new ServicioNotificaciones(this.mockDao.Object, Utilidades.Instancia);

            //Ejecutar
            var resultado = servicio.Obtener(codigo.ToString());

            //Evaluar
            Assert.IsTrue(resultado.SinProblemas);
        }

        [TestMethod]
        public void CLIG0SrvObtenerCaso2()
        {
            //Inicializar
            int codigo = 0;
            Notificacion notificacion = new Notificacion { NotificacionId = codigo };
            this.mockDao
                .Setup(dao => dao.Obtener(codigo))
                .Returns(() => { return notificacion; });

            IServicioNotificaciones servicio = new ServicioNotificaciones(this.mockDao.Object, Utilidades.Instancia);

            //Ejecutar
            var resultado = servicio.Obtener($"Err{codigo}");

            //Evaluar
            Assert.IsFalse(resultado.SinProblemas);
        }

        [TestMethod]
        public void CLIG0SrvBorrarCaso1()
        {
            //Inicializar
            int codigo = 1;
            string mensaje = String.Empty;
            Notificacion notificacion = new Notificacion { NotificacionId = codigo };
            this.mockDao
                .Setup(dao => dao.Borrar(out mensaje, codigo))
                .OutCallback(new OutAction<string, int>((out string nota, int id) => nota = mensaje ))
                .Returns(() => { return true; });

            this.mockUtils
                .Setup(dao => dao.ObtenerClave(It.IsAny<string>()))
                .Returns(() => { return "true"; });

            IServicioNotificaciones servicio = new ServicioNotificaciones(this.mockDao.Object, Utilidades.Instancia);

            //Ejecutar
            var resultado = servicio.Borrar(codigo.ToString());

            //Evaluar
            Assert.IsTrue(resultado.SinProblemas);
        }

        [TestMethod]
        public void CLIG0SrvBorrarCaso2()
        {
            //Inicializar
            int codigo = 1;
            string mensaje = String.Empty;
            Notificacion notificacion = new Notificacion { NotificacionId = codigo };
            this.mockDao
                .Setup(dao => dao.Borrar(out mensaje, codigo));
                //.OutCallback(new OutAction<string, int>((out string nota, int id) => nota = mensaje))
                //.Returns(() => { return true; });

            IServicioNotificaciones servicio = new ServicioNotificaciones(this.mockDao.Object, Utilidades.Instancia);

            //Ejecutar
            var resultado = servicio.Borrar($"Err{codigo}");

            //Evaluar
            Assert.IsFalse(resultado.SinProblemas);
        }

        [TestMethod]
        public void CLIG0SrvGuardarCaso1()
        {
            //Inicializar
            int codigo = 0;
            Notificacion notificacion = new Notificacion { NotificacionId = codigo };
            this.mockDao
                .Setup(dao => dao.Guardar(notificacion))
                .Returns(() => { return true; });

            IServicioNotificaciones servicio = new ServicioNotificaciones(this.mockDao.Object, Utilidades.Instancia);

            //Ejecutar
            var resultado = servicio.Guardar(notificacion);

            //Evaluar
            Assert.IsTrue(resultado.SinProblemas);
        }

        [TestMethod]
        public void CLIG0SrvGuardarCaso2()
        {
            //Inicializar
            int codigo = 0;
            Notificacion notificacion = new Notificacion { NotificacionId = codigo };
            this.mockDao
                .Setup(dao => dao.Guardar(notificacion))
                .Returns(() => { throw Utilidades.Instancia.Fabrica.CrearExcepcion("Prueba unitaria."); });

            IServicioNotificaciones servicio = new ServicioNotificaciones(this.mockDao.Object, Utilidades.Instancia);

            //Ejecutar
            var resultado = servicio.Guardar(notificacion);

            //Evaluar
            Assert.IsFalse(resultado.SinProblemas);
        }

        [TestMethod]
        public void CLIG0SrvObtenerTodosCaso1()
        {
            //Inicializar
            int codigo = 1;
            int cantidadPaginas = 1;
            string nombre = null;
            int filas = 3;
            int pagina = 0;

            List<Notificacion> notificaciones = new List<Notificacion>(
                new[] {
                    new Notificacion { NotificacionId = codigo }
                }
                );
            this.mockDao
                .Setup(dao => dao.ObtenerTodos(out cantidadPaginas, nombre, pagina, filas))
                .OutCallback(new OutAction<int, string, int, int>((out int paginas, string name, int a, int b) => paginas = cantidadPaginas))
                .Returns(() => { return notificaciones; });

            IServicioNotificaciones servicio = new ServicioNotificaciones(this.mockDao.Object, Utilidades.Instancia);

            //Ejecutar
            var resultado = servicio.ObtenerTodos(nombre, pagina, filas);

            //Evaluar
            Assert.IsTrue(resultado.SinProblemas);
        }

        [TestMethod]
        public void CLIG0SrvObtenerTodosCaso2()
        {
            //Inicializar
            int codigo = 1;
            int cantidadPaginas = 1;
            string nombre = null;
            int filas = 3;
            int pagina = 0;

            List<Notificacion> notificaciones = new List<Notificacion>(
                new[] {
                    new Notificacion { NotificacionId = codigo }
                }
                );
            this.mockDao
                .Setup(dao => dao.ObtenerTodos(out cantidadPaginas, nombre, pagina, filas))
                .OutCallback(new OutAction<int, string, int, int>((out int paginas, string name, int a, int b) => paginas = cantidadPaginas))
                .Returns(() => { throw Utilidades.Instancia.Fabrica.CrearExcepcion("Prueba unitaria."); });

            IServicioNotificaciones servicio = new ServicioNotificaciones(this.mockDao.Object, Utilidades.Instancia);

            //Ejecutar
            var resultado = servicio.ObtenerTodos(nombre, pagina, filas);

            //Evaluar
            Assert.IsFalse(resultado.SinProblemas);
        }

        [TestMethod]
        public void CLIG0SrvConstructorBase()
        {
            //Inicializar
            IServicioNotificaciones servicio = null;

            //Ejecutar
            servicio = new ServicioNotificaciones();

            //Evaluar
            Assert.IsNotNull(servicio);
        }

        #endregion
    }
}
