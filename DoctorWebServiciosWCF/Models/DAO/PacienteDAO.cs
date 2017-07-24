using System;
using System.Collections.Generic;
using System.Linq;
using DoctorWebServiciosWCF.Helpers;
using System.Web;

namespace DoctorWebServiciosWCF.Models.DAO
{
    using Modelo = Paciente;

    public class PacienteDAO : DAO<Modelo>, IPacienteDAO
    {
        /// <summary>
        /// Este metodo permite guardar los cambios de la notificacion que se indica.
        /// </summary>
        /// <param name="notificacion">Notificacion a guardar</param>
        /// <returns>Indica si finalizo correctamente o no.</returns>
        public bool GuardarPaciente(Modelo paciente)
        {
            try
            {
                var daoPaciente = Utilidades.Instancia.Fabrica.CrearDAO<Paciente>();
                daoPaciente.Crear(paciente);
                return true;
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

        /*public Paciente AddSeguros(Paciente paciente)
        {
            if (Request.Form["ListSeguros"] == null)
                return paciente;

            string list_seguros = Request.Form["ListSeguros"].ToString();
            string[] seguros = list_seguros.Split(',');
            foreach (string seguro_in in seguros)
            {
                Seguro seguro_found;
                seguro_found = db.Seguros.Find(Convert.ToInt32(seguro_in));
                if (seguro_found != null)
                    paciente.Seguros.Add(seguro_found);
            }
            return paciente;
        }*/


        public List<Seguro> ObtenerSeguros()
        {
            var daoPacientes = Utilidades.Instancia.Fabrica.CrearDAO<Seguro>();
            return daoPacientes.ObtenerTodos().ToList();
        }

        /// <summary>
        /// Metodo del DAO para obtener pacientes a partir de su identificador de usuario
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <returns>Paciente</returns>
        public Paciente ObtenerPaciente(string userId)
        {
            var daoPersonas = Utilidades.Instancia.Fabrica.CrearDAO<Persona>();
            return daoPersonas.ObtenerTodos().OfType<Paciente>().Single(p => p.ApplicationUserId == userId);
        }


        /// <summary>
        /// Metodo para borrar un paciente
        /// </summary>
        /// <param name="paciente"></param>
        public void EliminarPaciente(Modelo paciente)
        {
            var pacienteDAO = Utilidades.Instancia.Fabrica.CrearPacienteDAO();

            // Obtenemos la cita a eliminar de la BD usando el comando ObtenerPrimeroQue
            // luego eliminamos dicha cita con el comando Borrar
            var pacienteBuscado = ObtenerPrimeroQue(p => p.PersonaId == paciente.PersonaId);
            Borrar(pacienteBuscado);

        }

        /// <summary>
        /// Metodo del DAO para obtener una lista de los seguros
        /// </summary>
        /// <returns>Lista de seguros</returns>
        public List<Seguro> ObtenerSelectListSeguros()
        {
            var dao = Utilidades.Instancia.Fabrica.CrearDAO<Seguro>();
            return dao.ObtenerTodos().ToList();
        }









    }
}
 