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
        /*
        public void EliminarObservacionMedica(Modelo observacionMedica)
        {
            throw new NotImplementedException();
        }

        
        public void GuardarObservacionMedica(Modelo observacionMedica)
        {
            throw new NotImplementedException();
        }

     

        
        public Modelo ObtenerObservacion(int observacionId)
        {
            throw new NotImplementedException();
        } 

        public Paciente ObtenerPaciente(string userId)
        {
            throw new NotImplementedException();
        } */

        /// <summary>
        /// Metodo del DAO para obtener una Observacion Medica especifica
        /// </summary>
        /// <param name="observacionMedicaId">Identificador de la observacion medica</param>
        /// <returns>Observacion</returns>
        public ObservacionMedica ObtenerObservacion(int observacionMedicaId)
        {
            return db.ObservacionMedicas.Single(m => m.ObservacionMedicaId == observacionMedicaId);

        }

        /// <summary>
        /// Metodo del DAO para obtener pacientes a partir de su identificador de usuario
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <returns>Paciente</returns>
        public Paciente ObtenerPaciente(string userId)
        {
            return db.Personas.OfType<Paciente>().Single(p => p.ApplicationUserId == userId);
        }

        /// <summary>
        /// Metodo del DAO para guardar una observacion una observacion medica en la Base de datos
        /// </summary>
        /// <param name="observacionMedica">Cita que se desea guardar</param>
        public void GuardarObservacionMedica(Modelo observacionMedica)
        {          

            // Creamos la cita utilizando COMANDO
            Crear(observacionMedica);

        }

       

             /// <summary>
        /// Metodo del Data Access Object utilizado para eliminar Citas. 
        /// </summary>
        /// <param name="cita">Cita que se desea eliminar</param>
        /// <param name="calendario">Calendario para devolver la disponibilidad</param>
        public void EliminarObservacionMedica(Modelo observacionMedica)
        {
            var obstoDelete = ObtenerPrimeroQue(c => c.ObservacionMedicaId == observacionMedica.ObservacionMedicaId);
            Borrar(obstoDelete);
            db.SaveChanges();
        }

        /// <summary>
        /// Metodo del DAO para obtener una lista de las observaciones medicas
        /// </summary>
        /// <returns>Lista de observaciones medicas</returns>
        public List<ObservacionMedica> ObtenerListaObservacionMedica()
        {
            var dao = Fabrica.CrearDAO<ObservacionMedica>();
            return dao.ObtenerTodos().ToList();
        }


    }
}