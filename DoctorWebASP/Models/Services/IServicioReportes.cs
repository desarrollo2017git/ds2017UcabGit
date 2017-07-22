﻿using DoctorWebASP.Models.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorWebASP.Models.Services
{
    public interface IServicioReportes
    {
        ResultadoProceso getCantidadUsuariosRegistrados(string fechaInicioStr, string fechaFinStr);
        double getPromedioEdadPaciente();
        double getPromedioCitasPorMedico();
        double getPromedioRecursosDisponibles(string fechaInicioStr, string fechaFinStr);
        double getPromedioUsoApp();
        double getPromedioCitasCanceladasPorMedico(string fechaInicioStr, string fechaFinStr);
    }
}