using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWebServiciosWCF.Models.DAO
{
    public interface ICalendariosDAO
    {
        // Metodo del DAO para obtener un centro medico especifico
        CentroMedico ObtenerCentroMedico(int centroMedicoId);

        // Metodo del DAO para obtener pacientes a partir de su identificador de usuario
        Paciente ObtenerPaciente(string userId);

        // Metodo del DAO para obtener un calendario especifico
        Calendario ObtenerCalendario(int calendarioId);

        // Metodo del DAO para guardar Citas en la Base de datos
        void GuardarCita(Cita cita, Calendario calendario);

        // Metodo del DAO para obtener medicos a partir de su identificador de usuario
        Medico ObtenerMedico(string userId);

        // Metodo del DAO para obtener la lista de las citas de un paciente
        List<Cita> ObtenerListaCitas(string userId);

        // Metodo del DAO para obtener la lista de las citas de un medico
        List<Cita> ObtenerCitasDoctor(string userId);

        // Metodo del DAO para obtener una lista de los centros medicos
        List<CentroMedico> ObtenerSelectListCentrosMedicos();

        // Metodo del DAO para obtener la lista de especialidades medicas en un centro medico
        List<EspecialidadMedica> ObtenerEsMedicasPorMedicosEnCentroMedico(int cMedicoId);

        // Metodo del DAO para obtener una especialidad medica especifica
        EspecialidadMedica ObtenerEspecialidadMedica(int espMedica);

        // Metodo del DAO para obtener la lista de los doctores que trabajen en un centro medico
        List<Medico> ObtenerSelectListMedicosQueTrabajanEnCentroMedico(int centroMedicoId, int espMedica);

        // Metodo del DAO para obtener una cita especifica
        Cita ObtenerCita(int id);

        // Metodo del Data Access Object utilizado para eliminar Citas. 
        void EliminarCita(Cita cita, Calendario calendario);

        // Metodo del DAO para obtener centros medicos a partir de su RIF
        CentroMedico ObtenerCentroMedicoRif(string centroMedicoRif);

        // Metodo del DAO para obtener la lista de horarios/calendario de un medico
        List<Calendario> ObtenerListaDisponibilidad(int medicoId);

        // Metodo del DAO para obtener el medico asignado a una cita especifica
        Medico ObtenerMedicoAsignadoACita(int citaId);

        // Metodo del DAO para obtener la especialidad medica de un medico especifico
        EspecialidadMedica ObtenerEspecialidadMedicaDelDoctor(int medicoId);
    }
}
