using DoctorWebServiciosWCF.Helpers;
using DoctorWebServiciosWCF.Models.ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.DAO
{
    using Modelo = ResultadoE2;
    public class ResultadoE2DAO : DAO<Modelo>, IResultadoE2DAO
    {
        /// <summary>
        /// Metodo del Data Access Object utilizado para eliminar resultados. 
        /// </summary>
        /// <param name="resultadoE2">resultado que se desea eliminar</param>
        public void EliminarResultadoE2(Modelo resultadoE2)
        {
            var notificacionDAO = Utilidades.Instancia.Fabrica.CrearNotificacionDAO();
            try
            {
                // Obtenemos el resultado a eliminar de la BD usando el comando ObtenerPrimeroQue
                // luego eliminamos el resultado con el comando borrar
                var observacionAEliminar = ObtenerPrimeroQue(c => c.ResultadoExamenMedicoID == resultadoE2.ResultadoExamenMedicoID);
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
        /// Metodo del DAO para guardar resultados en la Base de datos
        /// </summary>
        /// <param name="resultadoE2">resultado que se desea guardar</param>
        public void GuardarResultadoE2(Modelo resultadoE2)
        {

            try
            {
                // Creamos el resultado utilizando comando Crear
                Crear(resultadoE2);
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
        /// Metodo del DAO para obtener una lista de los resultados
        /// </summary>
        /// <returns>Lista de resultados de examenes medicos</returns>
        public List<ResultadoE2> ObtenerSelectListResultadoE2()
        {
            var dao = Utilidades.Instancia.Fabrica.CrearDAO<ResultadoE2>();
            return dao.ObtenerTodos().ToList();
        }
    }
}