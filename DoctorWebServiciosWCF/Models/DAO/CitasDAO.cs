using DoctorWebServiciosWCF.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DoctorWebServiciosWCF.Models.DAO
{

    using Modelo = Cita;

    public class CitasDAO : DAO<Modelo>, ICitasDAO
    {
        /// <summary>
        /// Metodo del Data Access Object utilizado para eliminar Citas. 
        /// </summary>
        /// <param name="cita">Cita que se desea eliminar</param>
        /// <param name="calendario">Calendario para devolver la disponibilidad</param>
        public void EliminarCita(Modelo cita, Calendario calendario)
        {
            var notificacionDAO = Utilidades.Instancia.Fabrica.CrearNotificacionDAO();
            try
            {
                var notificacion = notificacionDAO.Obtener("cancelarCita");

                if (notificacion != null)
                {
                    notificacion.Enviar(calendario.Medico.Email, new { nombre = calendario.Medico.ConcatUserName });
                    notificacion.Enviar(cita.Paciente.Email, new { nombre = cita.Paciente.ConcatUserName });
                }
            }
            catch (DoctorWebException e)
            {
                throw e;
            }
            catch (Exception e)
            {
<<<<<<< HEAD
                throw Fabrica.CrearExcepcion(interna: e);
=======
                throw Utilidades.Instancia.Fabrica.CrearExcepcion(interna: e);
>>>>>>> master
            }
            // Actializamos el estado del calendario a disponible
            // luego, utilizamos el comando Actualizar
            calendario.Disponible = 1;
<<<<<<< HEAD
            var daoCalendario = Fabrica.CrearDAO<Calendario>();
=======
            var daoCalendario = Utilidades.Instancia.Fabrica.CrearDAO<Calendario>();
>>>>>>> master
            daoCalendario.Actualizar(calendario, registro => registro.CalendarioId == calendario.CalendarioId);

            // Obtenemos la cita a eliminar de la BD usando el comando ObtenerPrimeroQue
            // luego eliminamos dicha cita con el comando Borrar
            var citaToMod = ObtenerPrimeroQue(c => c.CitaId == cita.CitaId);
            Borrar(citaToMod);

        }

        /// <summary>
        /// Metodo del DAO para guardar Citas en la Base de datos
        /// </summary>
        /// <param name="cita">Cita que se desea guardar</param>
        /// <param name="calendario">Calendario para setear la disponibilidad</param>
        public void GuardarCita(Modelo cita, Calendario calendario)
        {            
            cita.CitaId = calendario.CalendarioId;

            // Creamos la cita utilizando comando Crear
            Crear(cita);

            // Procedemos a actualizar la disponibilidad del calendario
            // Para esto, debemos recuperar el objeto en la base de datos
            // luego cambiamos su contenido por el del calendario que recibimos cuya disponibilidad es 0  
            // usando el comando Actualizar  
            calendario.Disponible = 0;
<<<<<<< HEAD
            var daoCalendario = Fabrica.CrearDAO<Calendario>();
=======
            var daoCalendario = Utilidades.Instancia.Fabrica.CrearDAO<Calendario>();
>>>>>>> master
            daoCalendario.Actualizar(calendario, registro => registro.CalendarioId == calendario.CalendarioId);

            // NOTA: Igual aca
            var notificacionDAO = Utilidades.Instancia.Fabrica.CrearNotificacionDAO();
            try
            {
                var notificacion = notificacionDAO.Obtener("generarCita");

                if (notificacion != null)
                {
                    notificacion.Enviar(calendario.Medico.Email, new { nombre = calendario.Medico.ConcatUserName });
                    notificacion.Enviar(cita.Paciente.Email, new { nombre = cita.Paciente.ConcatUserName });
                }
            }
            catch (DoctorWebException e)
            {
                throw e;
            }
            catch (Exception e)
            {
<<<<<<< HEAD
                throw Fabrica.CrearExcepcion(interna: e);
=======
                throw Utilidades.Instancia.Fabrica.CrearExcepcion(interna: e);
>>>>>>> master
            }
        }

        /// <summary>
        /// Metodo del DAO para obtener un calendario especifico
        /// </summary>
        /// <param name="calendarioId">Identificador del calendario</param>
        /// <returns>Calendario</returns>
        public Calendario ObtenerCalendario(int calendarioId)
        {
<<<<<<< HEAD
            var daoCalendario = Fabrica.CrearDAO<Calendario>();
=======
            var daoCalendario = Utilidades.Instancia.Fabrica.CrearDAO<Calendario>();
>>>>>>> master
            //var calendario = daoCalendario.ObtenerPrimeroQue(c => c.CalendarioId == calendarioId);
            var calendario = db.Calendarios.Include(m => m.Medico).Single(c => c.CalendarioId == calendarioId);
            return calendario;

        }
        /// <summary>
        /// Metodo del DAO para obtener una cita especifica
        /// </summary>
        /// <param name="id">Identificar de la cita</param>
        /// <returns>Cita</returns>
        public Modelo ObtenerCita(int id)
        {
            return ObtenerTodos().Include(c => c.Calendario).Include(c => c.CentroMedico).Include(c => c.Paciente).Where(c => c.CitaId == id).Single();
        }

        /// <summary>
        /// Metodo del DAO para obtener una especialidad medica especifica
        /// </summary>
        /// <param name="espMedica">Identificador de la especialidad medica</param>
        /// <returns>Especialidad medica</returns>
        public EspecialidadMedica ObtenerEspecialidadMedica(int espMedica)
        {
<<<<<<< HEAD
            var daoEspecialidades = Fabrica.CrearDAO<EspecialidadMedica>();
=======
            var daoEspecialidades = Utilidades.Instancia.Fabrica.CrearDAO<EspecialidadMedica>();
>>>>>>> master
            return daoEspecialidades.ObtenerPrimeroQue(e => e.EspecialidadMedicaId == espMedica);
        }
        
        /// <summary>
        /// Metodo del DAO para obtener un centro medico especifico
        /// </summary>
        /// <param name="centroMedicoId">Identificador del centro medico</param>
        /// <returns>Centro medico</returns>
        public CentroMedico ObtenerCentroMedico(int centroMedicoId)
        {
<<<<<<< HEAD
            var daoCentrosMedicos = Fabrica.CrearDAO<CentroMedico>();
=======
            var daoCentrosMedicos = Utilidades.Instancia.Fabrica.CrearDAO<CentroMedico>();
>>>>>>> master
            return daoCentrosMedicos.ObtenerPrimeroQue(m => m.CentroMedicoId == centroMedicoId);

        }

        /// <summary>
        /// Metodo del DAO para obtener la especialidad medica de un medico especifico
        /// </summary>
        /// <param name="medicoId">Identificador del medico</param>
        /// <returns>Especialidad medica</returns>
        public EspecialidadMedica ObtenerEspecialidadMedicaDelDoctor(int medicoId)
        {
<<<<<<< HEAD
            var daoPersonas = Fabrica.CrearDAO<Persona>();
=======
            var daoPersonas = Utilidades.Instancia.Fabrica.CrearDAO<Persona>();
>>>>>>> master
            return daoPersonas.ObtenerTodos().OfType<Medico>().Where(m => m.PersonaId == medicoId).Select(p => p.EspecialidadMedica).Single();
        }

        /// <summary>
        /// Metodo del DAO para obtener el medico asignado a una cita especifica
        /// </summary>
        /// <param name="citaId">Identificador de la cita</param>
        /// <returns>Medico</returns>
        public Medico ObtenerMedicoAsignadoACita(int citaId)
        {
<<<<<<< HEAD
            var daoCalendario = Fabrica.CrearDAO<Calendario>();
=======
            var daoCalendario = Utilidades.Instancia.Fabrica.CrearDAO<Calendario>();
>>>>>>> master
            return daoCalendario.ObtenerTodos().Where(m => m.Cita.CitaId == citaId).Select(p => p.Medico).Single();
        }

        /// <summary>
        /// Metodo del DAO para obtener pacientes a partir de su identificador de usuario
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <returns>Paciente</returns>
        public Paciente ObtenerPaciente(string userId)
        {
<<<<<<< HEAD
            var daoPersonas = Fabrica.CrearDAO<Persona>();
=======
            var daoPersonas = Utilidades.Instancia.Fabrica.CrearDAO<Persona>();
>>>>>>> master
            return daoPersonas.ObtenerTodos().OfType<Paciente>().Single(p => p.ApplicationUserId == userId);
        }

        /// <summary>
        /// Metodo del DAO para obtener medicos a partir de su identificador de usuario
        /// </summary>
        /// <param name="userId">Identificador del usuario</param>
        /// <returns>Medico</returns>
        public Medico ObtenerMedico(string userId)
        {
<<<<<<< HEAD
            var daoPersonas = Fabrica.CrearDAO<Persona>();
=======
            var daoPersonas = Utilidades.Instancia.Fabrica.CrearDAO<Persona>();
>>>>>>> master
            return daoPersonas.ObtenerTodos().OfType<Medico>().Include(m => m.EspecialidadMedica).Single(p => p.ApplicationUserId == userId);
        }
        
        /// <summary>
        /// Metodo del DAO para obtener la lista de las citas de un paciente
        /// </summary>
        /// <param name="userId">Identificador de usuario del paciente</param>
        /// <returns>Lista de citas</returns>
        public List<Modelo> ObtenerListaCitas(string userId)
        {
            return ObtenerTodos().Include(c => c.CentroMedico).Include(c => c.Paciente).Include(c => c.Calendario).Where(c => c.Paciente.ApplicationUserId == userId).ToList();
        }

        /// <summary>
        /// Metodo del DAO para obtener la lista de las citas de un medico
        /// </summary>
        /// <param name="userId">Identificador de usuario del medico</param>
        /// <returns>Lista de citas</returns>
        public List<Modelo> ObtenerCitasDoctor(string userId)
        {
            return ObtenerTodos().Include(c => c.CentroMedico).Include(c => c.Paciente).Include(c => c.Calendario).Where(c => c.Calendario.Medico.ApplicationUserId == userId).ToList();
        }
        
        /// <summary>
        /// Metodo del DAO para obtener una lista de los centros medicos
        /// </summary>
        /// <returns>Lista de centros medicos</returns>
        public List<CentroMedico> ObtenerSelectListCentrosMedicos()
        {
            var dao = Utilidades.Instancia.Fabrica.CrearDAO<CentroMedico>();
            return dao.ObtenerTodos().ToList();
        }
        
        /// <summary>
        /// Metodo del DAO para obtener la lista de especialidades medicas en un centro medico
        /// </summary>
        /// <param name="cMedicoId">Identificador del centro medico</param>
        /// <returns>Lista de especialidades</returns>
        public List<EspecialidadMedica> ObtenerEsMedicasPorMedicosEnCentroMedico(int cMedicoId)
        {
            //.Include(e => e.EspecialidadMedica).Include(e => e.CentroMedico)
<<<<<<< HEAD
            var daoPersonas = Fabrica.CrearDAO<Persona>();
=======
            var daoPersonas = Utilidades.Instancia.Fabrica.CrearDAO<Persona>();
>>>>>>> master
            return daoPersonas.ObtenerTodos().OfType<Medico>().Where(m => m.CentroMedico.CentroMedicoId == cMedicoId).Select(c => c.EspecialidadMedica).Distinct().ToList();
        }

        /// <summary>
        /// Metodo del DAO para obtener la lista de los doctores que trabajen en un centro medico
        /// </summary>
        /// <param name="centroMedicoId">Identificador del centro medico</param>
        /// <param name="espMedica">Identificador de la especialidad medica</param>
        /// <returns>Lista de medicos</returns>
        public List<Medico> ObtenerSelectListMedicosQueTrabajanEnCentroMedico(int centroMedicoId, int espMedica)
        {
<<<<<<< HEAD
            var daoPersonas = Fabrica.CrearDAO<Persona>();
=======
            var daoPersonas = Utilidades.Instancia.Fabrica.CrearDAO<Persona>();
>>>>>>> master
            return daoPersonas.ObtenerTodos().OfType<Medico>().Where(p => p.CentroMedico.CentroMedicoId == centroMedicoId && p.EspecialidadMedica.EspecialidadMedicaId == espMedica).ToList();
        }

        /// <summary>
        /// Metodo del DAO para obtener centros medicos a partir de su RIF
        /// </summary>
        /// <param name="centroMedicoRif">RIF del centro medico</param>
        /// <returns>Centro medico</returns>
        public CentroMedico ObtenerCentroMedicoRif(string centroMedicoRif)
        {
<<<<<<< HEAD
            var daoCentrosMedicos = Fabrica.CrearDAO<CentroMedico>();
=======
            var daoCentrosMedicos = Utilidades.Instancia.Fabrica.CrearDAO<CentroMedico>();
>>>>>>> master
            return daoCentrosMedicos.ObtenerPrimeroQue(m => m.Rif == centroMedicoRif);
        }

        /// <summary>
        /// Metodo del DAO para obtener la lista de horarios/calendario de un medico
        /// </summary>
        /// <param name="medicoId">Identificador del medico</param>
        /// <returns>Lista de calendarios</returns>
        public List<Calendario> ObtenerListaDisponibilidad(int medicoId)
        {
            //Where(m => m.Medico.PersonaId == mdId && m.Disponible == 1).OrderBy(m => m.HoraInicio)
<<<<<<< HEAD
            var daoCalendarios = Fabrica.CrearDAO<Calendario>();
=======
            var daoCalendarios = Utilidades.Instancia.Fabrica.CrearDAO<Calendario>();
>>>>>>> master
            return daoCalendarios.ObtenerTodos().Where(m => m.Medico.PersonaId == medicoId && m.Disponible == 1).OrderBy(m => m.HoraInicio).ToList();
        }
    }
}