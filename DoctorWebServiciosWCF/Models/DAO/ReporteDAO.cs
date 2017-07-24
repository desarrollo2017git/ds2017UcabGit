using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.DAO
{
    public class ReporteDAO : DAO<Reporte>, IReporteDAO
    {
        public int getCantidadUsuariosRegistrados(string fechaInicioStr, string fechaFinStr)
        {
            throw new NotImplementedException();
        }

        public double getPromedioCitasCanceladasPorMedico(string fechaInicioStr, string fechaFinStr)
        {
            throw new NotImplementedException();
        }

        public double getPromedioCitasPorMedico()
        {
            throw new NotImplementedException();
        }

        public double getPromedioEdadPaciente()
        {
            throw new NotImplementedException();
        }

        public double getPromedioRecursosDisponibles(string fechaInicioStr, string fechaFinStr)
        {
            throw new NotImplementedException();
        }

        public double getPromedioUsoApp()
        {
            throw new NotImplementedException();
        }
    }
}