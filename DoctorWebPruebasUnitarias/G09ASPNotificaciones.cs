﻿using DoctorWebASP.Controllers;
using DoctorWebASP.Models;
using DoctorWebASP.Models.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using static DoctorWebPruebasUnitarias.Extensiones;

namespace DoctorWebPruebasUnitarias
{
    [TestClass]
    public class G09ASPNotificaciones
    {
        private Mock<IServicioNotificaciones> servicio_mocked { get; set; }

        #region DoctorWebASP
        [TestInitialize]
        public void Inicializar() {
            this.servicio_mocked = new Mock<IServicioNotificaciones>();
        }

        [TestMethod]
        // El controlador no recibe notificaciones del servicio, lista vacia.
        public void CLIG09CtrlIndexCaso1()
        {
            //Inicializar
            int cantidadRegistros = 0;
            string nombreFiltro = null;
            int numeroPagina = 0;
            int numeroFilas = 5;
            var respuestaServicio = new List<Notificacion>() { };
            servicio_mocked
                .Setup(servicio => servicio.ObtenerTodos(out cantidadRegistros, nombreFiltro, numeroPagina, numeroFilas))
                .OutCallback(new OutAction<int, string, int, int>((out int cantidadPaginas, string name, int a, int b) => cantidadPaginas = 0))
                .Returns(() => { return respuestaServicio; });

            var controlador = new NotificacionesController(servicio_mocked.Object);

            //Ejecutar
            var resultado = controlador.Index(nombreFiltro, numeroPagina, numeroFilas);

            //Evaluar

            // El resultado del metodo es una Vista?
            Assert.IsInstanceOfType(resultado, typeof(ViewResult));

            ViewResult vista = (ViewResult)resultado;
            // La vista tiene errores?
            Assert.IsNull(vista.ViewData["error"]);
            // La vista contiene el mismo filtro?
            Assert.AreEqual(nombreFiltro, vista.ViewData["nombre"]);
            // La vista tiene el mismo numero de filas?
            Assert.AreEqual(numeroFilas, vista.ViewData["filas"]);
            // La vista permite paginar a la derecha?
            Assert.AreEqual(false, vista.ViewData["permitirSiguiente"]);
            // La vista permite paginar a la izquierda?
            Assert.AreEqual(false, vista.ViewData["permitirAnterior"]);
            // En caso de poder paginar cual es el indice de la siguiente pagina?
            Assert.AreEqual(1, vista.ViewData["siguienteIndice"]);
            // En caso de poder paginar cual es el indice de la anterior pagina?
            Assert.AreEqual(-1, vista.ViewData["anteriorIndice"]);
            // El modelo de la vista es la respuesta del servicio?
            Assert.AreSame(respuestaServicio, vista.Model);
        }

        [TestMethod]
        // El controlador recibe notificaciones del servicio, lista con 1 elemento de un total de 3 en base de datos.
        public void CLIG09CtrlIndexCaso2()
        {
            //Inicializar
            int cantidadRegistros = 3;
            string nombreFiltro = null;
            int numeroPagina = 1;
            int numeroFilas = 1;
            var respuestaServicio = new List<Notificacion>() { new Notificacion() };
            servicio_mocked
                .Setup(servicio => servicio.ObtenerTodos(out cantidadRegistros, nombreFiltro, numeroPagina, numeroFilas))
                .OutCallback(new OutAction<int, string, int, int>((out int cantidadPaginas, string name, int a, int b) => cantidadPaginas = cantidadRegistros))
                .Returns(() => { return respuestaServicio; });

            var controlador = new NotificacionesController(servicio_mocked.Object);

            //Ejecutar
            var resultado = controlador.Index(nombreFiltro, numeroPagina, numeroFilas);

            //Evaluar

            // El resultado del metodo es una Vista?
            Assert.IsInstanceOfType(resultado, typeof(ViewResult));

            ViewResult vista = (ViewResult)resultado;
            // La vista tiene errores?
            Assert.IsNull(vista.ViewData["error"]);
            // La vista contiene el mismo filtro?
            Assert.AreEqual(nombreFiltro, vista.ViewData["nombre"]);
            // La vista tiene el mismo numero de filas?
            Assert.AreEqual(numeroFilas, vista.ViewData["filas"]);
            // La vista permite paginar a la derecha?
            Assert.AreEqual(true, vista.ViewData["permitirSiguiente"]);
            // La vista permite paginar a la izquierda?
            Assert.AreEqual(true, vista.ViewData["permitirAnterior"]);
            // En caso de poder paginar cual es el indice de la siguiente pagina?
            Assert.AreEqual(2, vista.ViewData["siguienteIndice"]);
            // En caso de poder paginar cual es el indice de la anterior pagina?
            Assert.AreEqual(0, vista.ViewData["anteriorIndice"]);
            // El modelo de la vista es la respuesta del servicio?
            Assert.AreSame(respuestaServicio, vista.Model);
        }

        [TestMethod]
        // El controlador captura una excepcion tras invocar el servicio.
        public void CLIG09CtrlIndexCaso3()
        {
            //Inicializar
            int cantidadRegistros = 0;
            string nombreFiltro = null;
            int numeroPagina = 1;
            int numeroFilas = 1;
            var respuestaServicio = new List<Notificacion>() { new Notificacion() };
            servicio_mocked
                .Setup(servicio => servicio.ObtenerTodos(out cantidadRegistros, nombreFiltro, numeroPagina, numeroFilas))
                .OutCallback(new OutAction<int, string, int, int>((out int cantidadPaginas, string name, int a, int b) => cantidadPaginas = cantidadRegistros))
                .Returns(() => { throw new Exception(); });

            var controlador = new NotificacionesController(servicio_mocked.Object);

            //Ejecutar
            var resultado = controlador.Index(nombreFiltro, numeroPagina, numeroFilas);

            //Evaluar

            // El resultado del metodo es una Vista?
            Assert.IsInstanceOfType(resultado, typeof(ViewResult));
            /*
            ViewResult vista = (ViewResult)resultado;
            // La vista tiene errores?
            Assert.IsNotNull(vista.ViewData["error"]);
            // La vista contiene el mismo filtro?
            Assert.AreEqual(nombreFiltro, vista.ViewData["nombre"]);
            // La vista tiene el mismo numero de filas?
            Assert.AreEqual(numeroFilas, vista.ViewData["filas"]);
            // La vista permite paginar a la derecha?
            Assert.AreEqual(false, vista.ViewData["permitirSiguiente"]);
            // La vista permite paginar a la izquierda?
            Assert.AreEqual(true, vista.ViewData["permitirAnterior"]);
            // En caso de poder paginar cual es el indice de la siguiente pagina?
            Assert.AreEqual(2, vista.ViewData["siguienteIndice"]);
            // En caso de poder paginar cual es el indice de la anterior pagina?
            Assert.AreEqual(0, vista.ViewData["anteriorIndice"]);*/

        }

        [TestMethod]
        // El controlador recibe codigo de notificacion igual a 0.
        public void CLIG09CtrlDetailCaso1()
        {
            //Inicializar
            int codigo = 0;
            var respuestaServicio = new Notificacion() { NotificacionId = codigo };
            servicio_mocked
                .Setup(servicio => servicio.Obtener(codigo))
                .Returns(() => { return respuestaServicio; });

            var controlador = new NotificacionesController(servicio_mocked.Object);

            //Ejecutar
            var resultado = controlador.Detail(codigo);

            //Evaluar

            // El resultado del metodo es una Vista?
            Assert.IsInstanceOfType(resultado, typeof(ViewResult));

            ViewResult vista = (ViewResult)resultado;
            // La vista tiene errores?
            Assert.IsNull(vista.ViewData["error"]);            
        }

        [TestMethod]
        // El controlador recibe un codigo de notificacion y el servicio le regresa el registro.
        public void CLIG09CtrlDetailCaso2()
        {
            //Inicializar
            int codigo = 1;
            var respuestaServicio = new Notificacion() { NotificacionId = codigo };
            servicio_mocked
                .Setup(servicio => servicio.Obtener(codigo))
                .Returns(() => { return respuestaServicio; });

            var controlador = new NotificacionesController(servicio_mocked.Object);

            //Ejecutar
            var resultado = controlador.Detail(codigo);

            //Evaluar

            // El resultado del metodo es una Vista?
            Assert.IsInstanceOfType(resultado, typeof(ViewResult));

            ViewResult vista = (ViewResult)resultado;
            // La vista tiene errores?
            Assert.IsNull(vista.ViewData["error"]);
            // El modelo de la vista no es la respuesta del servicio?
            Assert.AreSame(respuestaServicio, vista.Model);
        }

        [TestMethod]
        // El controlador recibe un codigo de notificacion y el servicio encuentra el registro.
        public void CLIG09CtrlDetailCaso3()
        {
            //Inicializar
            int codigo = 1;
            Notificacion respuestaServicio = null;
            servicio_mocked
                .Setup(servicio => servicio.Obtener(codigo))
                .Returns(() => { return respuestaServicio; });

            var controlador = new NotificacionesController(servicio_mocked.Object);

            //Ejecutar
            var resultado = controlador.Detail(codigo);

            //Evaluar
            // El resultado es redireccionar al usuario?
            Assert.IsInstanceOfType(resultado, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        // El controlador recibe un codigo de notificacion y falla el invocar el servicio.
        public void CLIG09CtrlDetailCaso4()
        {
            //Inicializar
            int codigo = 1;            
            servicio_mocked
                .Setup(servicio => servicio.Obtener(codigo))
                .Returns(() => { throw new Exception("Falla del servicio"); });

            var controlador = new NotificacionesController(servicio_mocked.Object);

            //Ejecutar
            var resultado = controlador.Detail(codigo);

            //Evaluar
            // El resultado es redireccionar al usuario?
            Assert.IsInstanceOfType(resultado, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        // El controlador recibe los datos del formulario y guarda la notificacion sin problemas.
        public void CLIG09CtrlCreateCaso1()
        {
            //Inicializar
            FormCollection collection = new FormCollection();
            collection.Add("NotificacionId", "0");
            collection.Add("Estado", "1");
            collection.Add("Nombre", "Prueba");
            collection.Add("Descripcion", "Prueba Descripcion");
            collection.Add("Contenido", "Prueba Contenido");
            collection.Add("Asunto", "Prueba Asunto");

            Notificacion registro = new Notificacion();
            string mensajeError = String.Empty;
            servicio_mocked
                .Setup(servicio => servicio.Guardar(out mensajeError, registro))
                .OutCallback(new OutAction<string, Notificacion>((out string mensaje, Notificacion notificacion) => mensaje = String.Empty))
                .Returns(() => { return true; });

            var controlador = new NotificacionesController(servicio_mocked.Object);

            //Ejecutar
            var resultado = controlador.Create(collection);

            //Evaluar
            // El resultado es redireccionar al usuario?
            Assert.IsInstanceOfType(resultado, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        // El controlador recibe los datos del formulario y falla al guardar la notificacion.
        public void CLIG09CtrlCreateCaso2()
        {
            //Inicializar
            FormCollection collection = new FormCollection();
            collection.Add("NotificacionId", "0");
            collection.Add("Estado", "1");
            collection.Add("Nombre", "Prueba");
            collection.Add("Descripcion", "Prueba Descripcion");
            collection.Add("Contenido", "Prueba Contenido");
            collection.Add("Asunto", "Prueba Asunto");

            Notificacion registro = new Notificacion();
            string mensajeError = It.IsAny<string>();
            servicio_mocked
                .Setup(servicio => servicio.Guardar(out mensajeError, It.IsAny<Notificacion>()))
                .OutCallback(new OutAction<string, Notificacion>((out string mensaje, Notificacion notificacion) => mensaje = String.Empty))
                .Returns(() => { throw new Exception(); });

            var controlador = new NotificacionesController(servicio_mocked.Object);

            //Ejecutar
            var resultado = controlador.Create(collection);

            //Evaluar
            // El resultado es redireccionar al usuario?
            Assert.IsInstanceOfType(resultado, typeof(ViewResult));
        }

        [TestMethod]
        // El controlador recibe los datos del formulario y guarda la notificacion.
        public void CLIG09CtrlEditareCaso1()
        {
            //Inicializar
            FormCollection collection = new FormCollection();
            collection.Add("NotificacionId", "1");
            collection.Add("Estado", "1");
            collection.Add("Nombre", "Prueba");
            collection.Add("Descripcion", "Prueba Descripcion");
            collection.Add("Contenido", "Prueba Contenido");
            collection.Add("Asunto", "Prueba Asunto");

            Notificacion registro = new Notificacion();
            string mensajeError = It.IsAny<string>();
            servicio_mocked
                .Setup(servicio => servicio.Guardar(out mensajeError, It.IsAny<Notificacion>()))
                .OutCallback(new OutAction<string, Notificacion>((out string mensaje, Notificacion notificacion) => mensaje = String.Empty))
                .Returns(() => { return true; });

            var controlador = new NotificacionesController(servicio_mocked.Object);

            //Ejecutar
            var resultado = controlador.Edit(collection);

            //Evaluar
            // El resultado es redireccionar al usuario?
            Assert.IsInstanceOfType(resultado, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        // El controlador recibe los datos del formulario y falla al guardar la notificacion.
        public void CLIG09CtrlEditareCaso2()
        {
            //Inicializar
            FormCollection collection = new FormCollection();
            collection.Add("NotificacionId", "1");
            collection.Add("Estado", "1");
            collection.Add("Nombre", "Prueba");
            collection.Add("Descripcion", "Prueba Descripcion");
            collection.Add("Contenido", "Prueba Contenido");
            collection.Add("Asunto", "Prueba Asunto");

            Notificacion registro = new Notificacion();
            string mensajeError = It.IsAny<string>();
            servicio_mocked
                .Setup(servicio => servicio.Guardar(out mensajeError, It.IsAny<Notificacion>()))
                .OutCallback(new OutAction<string, Notificacion>((out string mensaje, Notificacion notificacion) => mensaje = String.Empty))
                .Returns(() => { throw new Exception(); });

            var controlador = new NotificacionesController(servicio_mocked.Object);

            //Ejecutar
            var resultado = controlador.Edit(collection);

            //Evaluar
            // El resultado es redireccionar al usuario?
            Assert.IsInstanceOfType(resultado, typeof(ViewResult));
        }

        [TestMethod]
        // El controlador recibe la notificacion y el servicio la borra correctamente.
        public void CLIG09CtrlDeleteCaso1()
        {
            //Inicializar
            FormCollection collection = new FormCollection();
            collection.Add("NotificacionId", "1");

            Notificacion registro = new Notificacion();
            string mensajeError = It.IsAny<string>();
            servicio_mocked
                .Setup(servicio => servicio.Borrar(out mensajeError, It.IsAny<int>()))
                .OutCallback(new OutAction<string, int>((out string mensaje, int notificacionId) => mensaje = String.Empty))
                .Returns(() => { return true; });

            var controlador = new NotificacionesController(servicio_mocked.Object);

            //Ejecutar
            var resultado = controlador.Delete(collection);

            //Evaluar
            // El resultado es redireccionar al usuario?
            Assert.IsInstanceOfType(resultado, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        // El controlador recibe el codigo de notificacion y mientra la vista.
        public void CLIG09CtrlDeleteCaso3()
        {
            //Inicializar
            int codigo = 1;

            var respuestaServicio = new Notificacion() { NotificacionId = codigo };
            servicio_mocked
                .Setup(servicio => servicio.Obtener(codigo))
                .Returns(() => { return respuestaServicio; });

            var controlador = new NotificacionesController(servicio_mocked.Object);

            //Ejecutar
            var resultado = controlador.Delete(codigo);

            //Evaluar
            // El resultado es redireccionar al usuario?
            Assert.IsInstanceOfType(resultado, typeof(ViewResult));
        }

        [TestMethod]
        // El controlador recibe el codigo de notificacion pero no existe.
        public void CLIG09CtrlDeleteCaso4()
        {
            //Inicializar
            int codigo = 1;

            Notificacion respuestaServicio = null;
            servicio_mocked
                .Setup(servicio => servicio.Obtener(codigo))
                .Returns(() => { return respuestaServicio; });

            var controlador = new NotificacionesController(servicio_mocked.Object);

            //Ejecutar
            var resultado = controlador.Delete(codigo);

            //Evaluar
            // El resultado es redireccionar al usuario?
            Assert.IsInstanceOfType(resultado, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        // El controlador recibe el codigo de notificacion pero no existe.
        public void CLIG09CtrlDeleteCaso5()
        {
            //Inicializar
            int codigo = 0;
            
            servicio_mocked
                .Setup(servicio => servicio.Obtener(codigo))
                .Returns(() => { throw new Exception(); });

            var controlador = new NotificacionesController(servicio_mocked.Object);

            //Ejecutar
            var resultado = controlador.Delete(codigo);

            //Evaluar
            // El resultado es redireccionar al usuario?
            Assert.IsInstanceOfType(resultado, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        // El controlador recibe la notificacion y falla al borrarla con el servicio.
        public void CLIG09CtrlDeleteCaso2()
        {
            //Inicializar
            FormCollection collection = new FormCollection();
            collection.Add("NotificacionId", "1");

            Notificacion registro = new Notificacion();
            string mensajeError = It.IsAny<string>();
            servicio_mocked
                .Setup(servicio => servicio.Borrar(out mensajeError, It.IsAny<int>()))
                .OutCallback(new OutAction<string, int>((out string mensaje, int notificacionId) => mensaje = String.Empty))
                .Returns(() => { throw new Exception(); });

            var controlador = new NotificacionesController(servicio_mocked.Object);

            //Ejecutar
            var resultado = controlador.Delete(collection);

            //Evaluar
            // El resultado es redireccionar al usuario?
            Assert.IsInstanceOfType(resultado, typeof(RedirectToRouteResult));
        }

        #endregion

    }
}
