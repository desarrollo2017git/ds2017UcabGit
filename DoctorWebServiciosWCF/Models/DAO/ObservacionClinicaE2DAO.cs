using DoctorWebServiciosWCF.Helpers;
using DoctorWebServiciosWCF.Models.ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.DAO
{
    using Modelo = ObservacionClinicaE2;
    public class ObservacionClinicaE2DAO : DAO<Modelo>, IObservacionClinicaE2DAO
    {
        /// <summary>
        /// Metodo del Data Access Object utilizado para eliminar observaciones. 
        /// </summary>
        /// <param name="observacionClinicaE2">observacion que se desea eliminar</param>
        public void EliminarObservacionClinicaE2(Modelo observacionClinicaE2)
        {
            var notificacionDAO = Utilidades.Instancia.Fabrica.CrearNotificacionDAO();
            try
            {
                // Obtenemos la cita a eliminar de la BD usando el comando ObtenerPrimeroQue
                // luego eliminamos la observacion con el comando borrar
                var observacionAEliminar = ObtenerPrimeroQue(c => c.ObservacionDeAtencionMedicaId == observacionClinicaE2.ObservacionDeAtencionMedicaId);
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
        /// <param name="observacionClinicaE2">observacion que se desea guardar</param>
        public void GuardarObservacionClinicaE2(Modelo observacionClinicaE2)
        {

            try
            {
                // Creamos la observacion utilizando comando Crear
                Crear(observacionClinicaE2);
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
        /// Metodo del DAO para obtener una lista de las observaciones clinicas
        /// </summary>
        /// <returns>Lista de observaciones clinicas</returns>
        public List<ObservacionClinicaE2> ObtenerSelectListObservacionClinicaE2()
        {
            var dao = Utilidades.Instancia.Fabrica.CrearDAO<ObservacionClinicaE2>();
            return dao.ObtenerTodos().ToList();
        }
    }
}