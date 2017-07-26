using DoctorWebServiciosWCF.Helpers;
using DoctorWebServiciosWCF.Models.ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.DAO
{
    using Modelo = ObservacionDeAtencionClinica;
    public class ObservacionDeAtencionClinicaDAO : DAO<Modelo>, IObservacionDeAtencionClinicaDAO
    {
        /// <summary>
        /// Metodo del Data Access Object utilizado para eliminar observaciones. 
        /// </summary>
        /// <param name="observacionDeAtencionClinica">observacion que se desea eliminar</param>
        public void EliminarObservacionDeAtencionClinica(Modelo observacionDeAtencionClinica)
        {
            var notificacionDAO = Utilidades.Instancia.Fabrica.CrearNotificacionDAO();
            try
            {
                // Obtenemos la cita a eliminar de la BD usando el comando ObtenerPrimeroQue
                // luego eliminamos dicha cita con el comando Borrar
                var observacionAEliminar = ObtenerPrimeroQue(c => c.ObservacionDeAtencionMedicaId == observacionDeAtencionClinica.ObservacionDeAtencionMedicaId);
                Borrar(observacionAEliminar);
            }
            catch (DoctorWebException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw Utilidades.Instancia.Fabrica.CrearExcepcion(interna: e);
            }



        }

        /// <summary>
        /// Metodo del DAO para guardar observaciones en la Base de datos
        /// </summary>
        /// <param name="observacionDeAtencionClinica">observacion que se desea guardar</param>
        public void GuardarObservacionDeAtencionClinica(Modelo observacionDeAtencionClinica)
        {

            try
            {
                // Creamos la observaion utilizando comando Crear
                Crear(observacionDeAtencionClinica);
            }
            catch (DoctorWebException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw Utilidades.Instancia.Fabrica.CrearExcepcion(interna: e);
            }
        }

        /// <summary>
        /// Metodo del DAO para obtener una lista de las observaciones DeAtencionClinicas
        /// </summary>
        /// <returns>Lista de observaciones DeAtencionClinicas</returns>
        public List<ObservacionDeAtencionClinica> ObtenerSelectListObservacionDeAtencionClinica()
        {
            var dao = Utilidades.Instancia.Fabrica.CrearDAO<ObservacionDeAtencionClinica>();
            return dao.ObtenerTodos().ToList();
        }
    }
}