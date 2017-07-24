using DoctorWebServiciosWCF.Helpers;
using DoctorWebServiciosWCF.Models.ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.DAO
{
    using Modelo = ObservacionMedica;
    public class ObservacionMedicaDAO : DAO<Modelo>, IObservacionMedicaDAO
    {
        /// <summary>
        /// Metodo del Data Access Object utilizado para eliminar observaciones. 
        /// </summary>
        /// <param name="observacionMedica">observacion que se desea eliminar</param>
        public void EliminarObservacionMedica(Modelo observacionMedica)
        {
            var notificacionDAO = Fabrica.CrearNotificacionDAO();
            try
            {
                // Obtenemos la cita a eliminar de la BD usando el comando ObtenerPrimeroQue
                // luego eliminamos dicha observacion con el comando Borrar
                var observacionAEliminar = ObtenerPrimeroQue(c => c.ObservacionMedicaId == observacionMedica.ObservacionMedicaId);
                Borrar(observacionAEliminar);
            }
            catch (DoctorWebException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw Fabrica.CrearExcepcion(interna: e);
            }



        }

        /// <summary>
        /// Metodo del DAO para guardar observaciones en la Base de datos
        /// </summary>
        /// <param name="observacionMedica">observacion que se desea guardar</param>
        public void GuardarObservacionMedica(Modelo observacionMedica)
        {
           
            try
            {
                // Creamos la observaion utilizando comando Crear
                Crear(observacionMedica);
            }
            catch (DoctorWebException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw Fabrica.CrearExcepcion(interna: e);
            }
        }

        /// <summary>
        /// Metodo del DAO para obtener una lista de las observaciones medicas
        /// </summary>
        /// <returns>Lista de observaciones medicas</returns>
        public List<ObservacionMedica> ObtenerSelectListObservacionMedica()
        {
            var dao = Fabrica.CrearDAO<ObservacionMedica>();
            return dao.ObtenerTodos().ToList();
        }
    }

}   