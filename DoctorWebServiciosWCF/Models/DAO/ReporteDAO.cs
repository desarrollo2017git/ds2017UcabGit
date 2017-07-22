using DoctorWebServiciosWCF.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.DAO
{
    public class ReporteDAO : DAO<Reporte>, IReporteDAO
    {
        private string lastTimeOnDay = "11:59:59 PM";
        private string firstTimeOnDay = "12:00:00 AM";

        public int getCantidadUsuariosRegistrados(string fechaInicioStr, string fechaFinStr)
        {
            try
            {
                DateTime fechaInicio = DateTime.Parse(fechaInicioStr + " " + firstTimeOnDay, CultureInfo.InvariantCulture);
                DateTime fechaFin = DateTime.Parse(fechaFinStr + " " + lastTimeOnDay, CultureInfo.InvariantCulture);

                var result = from p in db.Personas
                             where p.FechaCreacion >= fechaInicio & p.FechaCreacion <= fechaFin
                             select p;

                if (result == null)
                    throw Fabrica.CrearExcepcion("Hay un problema con la consulta en la base de datos.");

                return result.Count();
            }
            catch (FormatException e)
            {
                throw e;
            }
            catch (DoctorWebException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw Fabrica.CrearExcepcion(interna: e);
            }
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