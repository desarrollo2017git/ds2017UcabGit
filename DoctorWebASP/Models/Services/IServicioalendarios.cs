using DoctorWebASP.Controllers;
using DoctorWebASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DoctorWebASP.Models.Services
{
    public interface IServicioCalendarios
    {
        // Metodo en el cliente para obtener un centro medico especifico
        CentroMedico ObtenerCentroMedico(int centroMedicoId);

        // Metodo en el cliente utilizado para obtener un paciente en especifico
        Paciente ObtenerPaciente(string userId);

        // Metodo del cliente que realiza el llamado al servicio
        // para obtener un Calendario especifico
        Calendario ObtenerCalendario(int calendarioId);

        // Metodo del cliente que realiza el llamado para Guardar una Cita
        void GuardarCita(Cita cita, Calendario calendario);

        // Obtener usuario logeado
        string ObtenerUsuarioLoggedIn(CitasController citasController);

        // Metodo en el cliente utilizado para obtener un medico especifico
        Medico ObtenerMedico(string userId);

        // Metodo en el cliente utilizado para obtener la lista de citas
        // que tiene un paciente
        List<Cita> ObtenerListaCitas(string userId);

        // Metodo del cliente para obtener la lista de citas de un doctor especifico
        List<Cita> ObtenerCitasDoctor(string userId);

        // Metodo en el cliente utilizado para obtener una lista de todos
        // los centros medicos
        SelectList ObtenerSelectListCentrosMedicos();

        // Metodo en el cliente utilizado para obtener las especialidades medicas
        // que existen en un centro medico especifico
        SelectList ObtenerEsMedicasPorMedicosEnCentroMedico(int cMedicoId);

        // Metodo del cliente utilizado para obtener
        // una especialidad medica en especifico
        EspecialidadMedica ObtenerEspecialidadMedica(int espMedica);

        // Metodo en el cliente para obtener la lista de los medicos
        // que trabajan en un centro medico
        SelectList ObtenerSelectListMedicosQueTrabajanEnCentroMedico(int centroMedicoId, int espMedica);

        // Metodo del cliente que realiza el llamado al servicio
        // para obtener una Cita especifica
        Cita ObtenerCita(int id);

        // Metodo del cliente que realiza el llamado para eliminar una Cita
        void EliminarCita(Cita cita, Calendario calendario);

        // Metodo en el cliente utilizado para obtener un centro medico
        // mediante su RIF
        CentroMedico ObtenerCentroMedicoRif(string centroMedico);

        // Metodo en el cliente para obtener una lista de los horarios
        // disponibles de un doctor
        List<Calendario> ObtenerListaDisponibilidad(string medicoId);

        // Metodo en el cliente utilizado para obtener
        // el medico encargado de una cita determinada
        Medico ObtenerMedicoAsignadoACita(int citaId);

        // Metodo del cliente utilizado para obtener la especialidad
        // medica de un doctor particular
        EspecialidadMedica ObtenerEspecialidadMedicaDelDoctor(int medicoId);
    }
}
