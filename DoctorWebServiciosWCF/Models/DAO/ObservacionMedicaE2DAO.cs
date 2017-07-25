using DoctorWebServiciosWCF.Helpers;
using DoctorWebServiciosWCF.Models.ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.DAO
{
    using Modelo = ObservacionMedicaE2;
    public class ObservacionMedicaE2DAO : DAO<Modelo>, IObservacionMedicaE2DAO
    {
        /// <summary>
        /// Metodo del Data Access Object utilizado para eliminar observaciones. 
        /// </summary>
        /// <param name="observacionMedicaE2">observacion que se desea eliminar</param>
        public void EliminarObservacionMedicaE2(Modelo observacionMedicaE2)
        {
            var notificacionDAO = Utilidades.Instancia.Fabrica.CrearNotificacionDAO();
            try
            {
                // Obtenemos la cita a eliminar de la BD usando el comando ObtenerPrimeroQue
                // luego eliminamos la observacion con el comando borrar
                var observacionAEliminar = ObtenerPrimeroQue(c => c.ObservacionMedicaId == observacionMedicaE2.ObservacionMedicaId);
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
        /// <param name="observacionMedicaE2">observacion que se desea guardar</param>
        public void GuardarObservacionMedicaE2(Modelo observacionMedicaE2)
        {

            try
            {
                // Creamos la observacion utilizando comando Crear
                Crear(observacionMedicaE2);
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
        /// Metodo del DAO para obtener una lista de las observaciones medicas
        /// </summary>
        /// <returns>Lista de observaciones medicas</returns>
        public List<ObservacionMedicaE2> ObtenerSelectListObservacionMedicaE2()
        {
            var dao = Utilidades.Instancia.Fabrica.CrearDAO<ObservacionMedicaE2>();
            return dao.ObtenerTodos().ToList();
        }
    }

}