using DoctorWebServiciosWCF.Controllers.Helpers;
using DoctorWebServiciosWCF.Helpers;
using System;
using System.Globalization;
using System.Linq;

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
            try
            {
                DateTime dtFechaInicio = DateTime.Parse(fechaInicioStr, CultureInfo.InvariantCulture);
                DateTime dtFechaFin = DateTime.Parse(fechaFinStr, CultureInfo.InvariantCulture);

                double? cantidadCitasCanceladas = (from c in db.Calendarios
                                                   where c.Cancelada & c.Disponible == 1 & c.HoraInicio >= dtFechaInicio & c.HoraFin <= dtFechaFin
                                                   select c).Count();
                double? cantidadMedicos = (from p in db.Personas
                                           where p is Medico
                                           select p).Count();

                if (cantidadCitasCanceladas == null || cantidadMedicos == null)
                    throw Fabrica.CrearExcepcion("Hay un problema con la consulta en la base de datos.");

                if (cantidadMedicos == 0)
                    throw new DivideByZeroException("Hay un error de división entre cero.");

                double promedio = (double)cantidadCitasCanceladas / (double)cantidadMedicos;

                if (Double.IsInfinity(promedio) || Double.IsNaN(promedio))
                    throw new NotFiniteNumberException("La operación retornó un número no válido.");

                return promedio;
            }
            catch (DivideByZeroException e)
            {
                throw e;
            }
            catch (NotFiniteNumberException e)
            {
                throw e;
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

        public double getPromedioCitasPorMedico()
        {
            try
            {
                double? cantidadCitas = (from c in db.Calendarios
                                         where !c.Cancelada & c.Disponible == 0
                                         select c).Count();
                double? cantidadMedicos = (from p in db.Personas
                                           where p is Medico
                                           select p).Count();
                if (cantidadMedicos == null || cantidadCitas == null)
                    throw Fabrica.CrearExcepcion("Hay un problema con la consulta en la base de datos.");

                if (cantidadMedicos == 0)
                    throw new DivideByZeroException("Hay un error de división entre cero.");

                double promedio = (double)cantidadCitas / (double)cantidadMedicos;

                if (Double.IsInfinity(promedio) || Double.IsNaN(promedio))
                    throw new NotFiniteNumberException("La operación retornó un número no válido.");

                return promedio;
            }
            catch (DivideByZeroException e)
            {
                throw e;
            }
            catch (NotFiniteNumberException e)
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

        public double getPromedioEdadPaciente()
        {
            try
            {
                var result = from p in db.Personas
                             where (p is Paciente)
                             select p.FechaNacimiento;

                if (result == null)
                    throw Fabrica.CrearExcepcion("Hay un problema con la consulta en la base de datos.");

                double total = 0;

                double cantidadPacientes = result.Count();

                if (cantidadPacientes == 0)
                    throw new DivideByZeroException("Hay un error de división entre cero.");

                foreach (var r in result.ToList())
                {
                    Age age = new Age(r, DateTime.Today.AddDays(1).AddTicks(-1));
                    total = total + age.Years;
                }

                double promedio = total / cantidadPacientes;

                if (Double.IsInfinity(promedio) || Double.IsNaN(promedio))
                    throw new NotFiniteNumberException("La operación retorna un tipo de dato no válido.");

                return promedio;
            }
            catch (DivideByZeroException e)
            {
                throw e;
            }
            catch (NotFiniteNumberException e)
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

        public double getPromedioRecursosDisponibles(string fechaInicioStr, string fechaFinStr)
        {
            try
            {
                DateTime dtFechaInicio = DateTime.Parse(fechaInicioStr + " " + firstTimeOnDay, CultureInfo.InvariantCulture);
                DateTime dtFechaFin = DateTime.Parse(fechaFinStr + " " + lastTimeOnDay, CultureInfo.InvariantCulture);

                var result = from ur in db.UsoRecursos
                             join ci in db.Citas on ur.Cita equals ci
                             join ca in db.Calendarios on ci.Calendario equals ca
                             where ca.HoraInicio >= dtFechaInicio & ca.HoraInicio <= dtFechaFin & !ca.Cancelada
                             select ur;

                var almacen = (from a in db.Almacenes
                               select a);

                double? cantidadRecursos = (from rh in db.RecursosHospitalarios
                                            select rh).Count();

                if (result == null || almacen == null || cantidadRecursos == null)
                    throw Fabrica.CrearExcepcion("Hay un problema con la consulta en la base de datos.");

                if (cantidadRecursos == 0)
                    throw new DivideByZeroException("Hay un error de división entre cero.");

                double? totalCantidadRecursos = 0;

                foreach (var a in almacen.ToList())
                {
                    foreach (var ur in result.ToList())
                    {
                        if (a.RecursoHospitalario == ur.RecursoHospitalario)
                        {
                            if (a.Disponible - ur.Cantidad >= 0)
                            {
                                totalCantidadRecursos = totalCantidadRecursos + (a.Disponible - ur.Cantidad);
                            }
                        }
                    }
                }

                double promedio = (double)totalCantidadRecursos / (double)cantidadRecursos;

                if (Double.IsInfinity(promedio) || Double.IsNaN(promedio))
                    throw new NotFiniteNumberException("La operación retornó un número no válido.");

                return result.Count();
            }
            catch(DivideByZeroException e)
            {
                throw e;
            }
            catch(NotFiniteNumberException e)
            {
                throw e;
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

        public double getPromedioUsoApp()
        {
            try
            {
                double? bitacora = (from b in db.Bitacoras
                                    select b).Count();

                double? usuarios = (from u in db.Personas
                                    select u).Count();

                if (bitacora == null || usuarios == null)
                    throw Fabrica.CrearExcepcion("Hay un problema con la consulta en la base de datos.");

                if (usuarios == 0)
                    throw new DivideByZeroException("Hay un error de división entre cero.");

                double promedio = (double)bitacora / (double)usuarios;

                if (Double.IsInfinity(promedio) || Double.IsNaN(promedio))
                    throw new NotFiniteNumberException("La operación retorna un tipo de dato no válido.");

                return promedio;
            }
            catch (DivideByZeroException e)
            {
                throw e;
            }
            catch (NotFiniteNumberException e)
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
    }
}