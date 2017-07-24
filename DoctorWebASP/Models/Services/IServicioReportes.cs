using DoctorWebASP.Models.Results;
using Newtonsoft.Json.Linq;
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
        ResultadoProceso getPromedioEdadPaciente();
        ResultadoProceso getPromedioCitasPorMedico();
        ResultadoProceso getPromedioRecursosDisponibles(string fechaInicioStr, string fechaFinStr);
        ResultadoProceso getPromedioUsoApp();
        ResultadoProceso getPromedioCitasCanceladasPorMedico(string fechaInicioStr, string fechaFinStr);

        ResultadoServicio<String> obtenerAtributos(List<string> entities);
    }
}
