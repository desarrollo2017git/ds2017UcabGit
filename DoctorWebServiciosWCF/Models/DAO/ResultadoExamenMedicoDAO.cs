using DoctorWebServiciosWCF.Helpers;
using DoctorWebServiciosWCF.Models.ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.DAO
{
    using Modelo = ResultadoExamenMedico;
    public class ResultadoExamenMedicoDAO : DAO<Modelo>, IResultadoExamenMedicoDAO
    {
        /// <summary>
        /// Metodo del Data Access Object utilizado para eliminar observaciones. 
        /// </summary>
        /// <param name="observacionMedica">observacion que se desea eliminar</param>
        public void EliminarResultadoExamenMedico(Modelo resultadoExamenMedico)
        {
            var notificacionDAO = Utilidades.Instancia.Fabrica.CrearNotificacionDAO();
            try
            {
                // Obtenemos la  a eliminar de la BD usando el comando ObtenerPrimeroQue
                // luego eliminamos dicha  con el comando Borrar
                var observacionAEliminar = ObtenerPrimeroQue(c => c.ResultadoExamenMedicoID == resultadoExamenMedico.ResultadoExamenMedicoID);
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
        /// <param name="observacionMedica">observacion que se desea guardar</param>
        public void GuardarResultadoExamenMedico(Modelo resultadoExamenMedico)
        {

            try
            {
                // Creamos la observaion utilizando comando Crear
                Crear(resultadoExamenMedico);
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
        public List<ResultadoExamenMedico> ObtenerSelectListResultadoExamenMedico()
        {
            var dao = Utilidades.Instancia.Fabrica.CrearDAO<ResultadoExamenMedico>();
            return dao.ObtenerTodos().ToList();
        }
    }

}