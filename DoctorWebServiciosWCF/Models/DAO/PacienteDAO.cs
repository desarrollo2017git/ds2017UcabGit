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
        /// Este metodo permite guardar los cambios de un paciente.
        /// </summary>
        /// <param name="paciente">paciente a guardar</param>
        /// <returns>Indica si finalizo correctamente o no.</returns>
        public bool GuardarPaciente(Modelo paciente)
        {
            try
            {
                var daoPaciente = Utilidades.Instancia.Fabrica.CrearDAO<Paciente>();

                if (paciente.PersonaId > 0)
                    daoPaciente.Actualizar(paciente, registro => registro.PersonaId == paciente.PersonaId);
                else
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


        /// <summary>
        /// Metodo para obtener la lista de seguros
        /// </summary>
        /// <returns>Lista de Seguros</returns>
        public List<Seguro> ObtenerSeguros()
        {
            var daoPacientes = Utilidades.Instancia.Fabrica.CrearDAO<Seguro>();
            return daoPacientes.ObtenerTodos().ToList();
        }

        /// <summary>
        /// Metodo para obtener la lista de pacientes
        /// </summary>
        /// <returns>Lista de Pacientes</returns>
        public List<Paciente> ObtenerPacientesList(String id)
        {
            var daoPacientes = Utilidades.Instancia.Fabrica.CrearDAO<Paciente>();
            return daoPacientes.ObtenerTodosLosQue(registro => registro.Cedula == id).ToList();
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




    }
}
 