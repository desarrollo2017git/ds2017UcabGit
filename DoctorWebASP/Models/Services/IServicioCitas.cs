﻿using DoctorWebASP.Controllers;
using DoctorWebASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DoctorWebASP.Models.Services
{
    public interface IServicioCitas
    {
        CentroMedico ObtenerCentroMedico(int centroMedicoId);
        Paciente ObtenerPaciente(string userId);
        Calendario ObtenerCalendario(int calendarioId);
        void GuardarCita(Cita cita, Calendario calendario);
        string ObtenerUsuarioLoggedIn(CitasController citasController);
        Medico ObtenerMedico(string userId);
        List<Cita> ObtenerListaCitas(string userId);
        List<Cita> ObtenerCitasDoctor(string userId);
        SelectList ObtenerSelectListCentrosMedicos();
        SelectList ObtenerEsMedicasPorMedicosEnCentroMedico(int cMedicoId);
        EspecialidadMedica ObtenerEspecialidadMedica(int espMedica);
        SelectList ObtenerSelectListMedicosQueTrabajanEnCentroMedico(int centroMedicoId, int espMedica);
        Cita ObtenerCita(int id);
        void EliminarCita(Cita cita, Calendario calendario);
        CentroMedico ObtenerCentroMedicoRif(string centroMedico);
        List<Calendario> ObtenerListaDisponibilidad(string medicoId);
        Medico ObtenerMedicoAsignadoACita(int citaId);
        EspecialidadMedica ObtenerEspecialidadMedicaDelDoctor(int medicoId);
    }
}
