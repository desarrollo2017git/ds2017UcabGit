using DoctorWebServiciosWCF.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace DoctorWebServiciosWCF.Models.DAO
{
    public class ReporteDAO : DAO<Reporte>, IReporteDAO
    {
        private string lastTimeOnDay = "11:59:59 PM";
        private string firstTimeOnDay = "12:00:00 AM";

        #region REPORTES PREESTABLECIDOS
        #region REPORTE #1 - Cantidad de usuarios registrados en un tiempo determinado
        /// <summary>
        /// Método utilizado para obtener la cantidad de usuarios registrados durante el periodo de tiempo seleccionado por el usuario.
        /// </summary>
        /// <param name="fechaInicioStr">Fecha incicial para el periodo de conteo de registro de usuarios.</param>
        /// <param name="fechaFinStr">Fecha incicial para el periodo de conteo de registro de usuarios.</param>
        /// <returns>Cantidad de usuarios registrados en un periodo de tiempo determinado.</returns>
        /// <exception cref="System.FormatException">Excepción lanzada en caso de que el formato de los parámetros este incorrecto.</exception>
        /// <exception cref="DoctorWebException">Excepción lanzada en caso de ocurrir un problema en la consulta de la base de datos.</exception>
        /// <exception cref="System.Exception">Excepción lanzada en caso de existir algún otro tipo de error y que no pueda ser capturado por las excepciones anteriores.</exception>
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
                    throw Utilidades.Instancia.Fabrica.CrearExcepcion("Hay un problema con la consulta en la base de datos.");

                Utilidades.Instancia.Debug($"Obteniendo Reporte#1 | App: Servicio | Tipo: Preestablecido | Descripción: getCantidadUsuariosRegistrados | Resultado: {result.Count()} usuarios.");
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
                throw Utilidades.Instancia.Fabrica.CrearExcepcion(interna: e);
            }
        }
        #endregion

        #region REPORTE #2 - Promedio de edad de los pacientes.
        /// <summary>
        /// Método utilizado para obtener la edad promedio de los pacientes.
        /// </summary>
        /// <returns>Promedio de edad de los pacientes.</returns>
        /// <exception cref="System.DivideByZeroException">Excepción lanzada en caso de ocurrir un división entre cero en las operaciones realizadas por el método.</exception>
        /// <exception cref="System.NotFiniteNumberException">Excepción lanzada en caso de que las operaciones realizadas por el método arrojen un número no válido.</exception>
        /// <exception cref="DoctorWebException">Excepción lanzada en caso de ocurrir un problema en la consulta de la base de datos.</exception>
        /// <exception cref="System.Exception">Excepción lanzada en caso de existir algún otro tipo de error y que no pueda ser capturado por las excepciones anteriores.</exception>
        public double getPromedioEdadPaciente()
        {
            try
            {
                var result = from p in db.Personas
                             where (p is Paciente)
                             select p.FechaNacimiento;

                if (result == null)
                    throw Utilidades.Instancia.Fabrica.CrearExcepcion("Hay un problema con la consulta en la base de datos.");

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

                Utilidades.Instancia.Debug($"Obteniendo Reporte#2 | App: Servicio | Tipo: Preestablecido | Descripción: getPromedioEdadPaciente | Resultado: {promedio} años.");
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
                throw Utilidades.Instancia.Fabrica.CrearExcepcion(interna: e);
            }
        }
        #endregion

        #region REPORTE #3 - Promedio de citas por médico.
        /// <summary>
        /// Método utilizado para obtener el promedio de citas atendidas por médico.
        /// </summary>
        /// <returns>Promedio de citas atendidas por médico</returns>
        /// <exception cref="System.DivideByZeroException">Excepción lanzada en caso de ocurrir un división entre cero en las operaciones realizadas por el método.</exception>
        /// <exception cref="System.NotFiniteNumberException">Excepción lanzada en caso de que las operaciones realizadas por el método arrojen un número no válido.</exception>
        /// <exception cref="DoctorWebException">Excepción lanzada en caso de ocurrir un problema en la consulta de la base de datos.</exception>
        /// <exception cref="System.Exception">Excepción lanzada en caso de existir algún otro tipo de error y que no pueda ser capturado por las excepciones anteriores.</exception>
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
                    throw Utilidades.Instancia.Fabrica.CrearExcepcion("Hay un problema con la consulta en la base de datos.");

                if (cantidadMedicos == 0)
                    throw new DivideByZeroException("Hay un error de división entre cero.");

                double promedio = (double)cantidadCitas / (double)cantidadMedicos;

                if (Double.IsInfinity(promedio) || Double.IsNaN(promedio))
                    throw new NotFiniteNumberException("La operación retornó un número no válido.");

                Utilidades.Instancia.Debug($"Obteniendo Reporte#3 | App: Servicio | Tipo: Preestablecido | Descripción: getPromedioCitasPorMedico | Resultado: {promedio} citas.");
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
                throw Utilidades.Instancia.Fabrica.CrearExcepcion(interna: e);
            }
        }
        #endregion

        #region REPORTE #4 - Promedio de recursos disponibles en un tiempo determinado.
        /// <summary>
        /// Método utilizado para obtener el promedio de recursos disponibles en un periodo de tiempo seleccionado por el usuario.
        /// </summary>
        /// <param name="fechaInicioStr">Fecha incicial para el periodo de conteo de registro de usuarios.</param>
        /// <param name="fechaFinStr">Fecha incicial para el periodo de conteo de registro de usuarios.</param>
        /// <returns>Promedio de recursos disponibles en un periodo de tiempo determinado.</returns>
        /// /// <exception cref="System.DivideByZeroException">Excepción lanzada en caso de ocurrir un división entre cero en las operaciones realizadas por el método.</exception>
        /// <exception cref="System.NotFiniteNumberException">Excepción lanzada en caso de que las operaciones realizadas por el método arrojen un número no válido.</exception>
        /// <exception cref="System.FormatException">Excepción lanzada en caso de que el formato de los parámetros este incorrecto.</exception>
        /// <exception cref="DoctorWebException">Excepción lanzada en caso de ocurrir un problema en la consulta de la base de datos.</exception>
        /// <exception cref="System.Exception">Excepción lanzada en caso de existir algún otro tipo de error y que no pueda ser capturado por las excepciones anteriores.</exception>
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
                    throw Utilidades.Instancia.Fabrica.CrearExcepcion("Hay un problema con la consulta en la base de datos.");

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

                Utilidades.Instancia.Debug($"Obteniendo Reporte#4 | App: Servicio | Tipo: Preestablecido | Descripción: getPromedioRecursosDisponibles | Resultado: {promedio} recursos.");
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
                throw Utilidades.Instancia.Fabrica.CrearExcepcion(interna: e);
            }
        }
        #endregion

        #region REPORTE #5 - Promedio de uso de la aplicación
        /// <summary>
        /// Método utilizado para obtener el promedio de uso de la aplicación.
        /// </summary>
        /// <returns>Promedio de uso de la aplicación.</returns>
        /// <exception cref="System.DivideByZeroException">Excepción lanzada en caso de ocurrir un división entre cero en las operaciones realizadas por el método.</exception>
        /// <exception cref="System.NotFiniteNumberException">Excepción lanzada en caso de que las operaciones realizadas por el método arrojen un número no válido.</exception>
        /// <exception cref="DoctorWebException">Excepción lanzada en caso de ocurrir un problema en la consulta de la base de datos.</exception>
        /// <exception cref="System.Exception">Excepción lanzada en caso de existir algún otro tipo de error y que no pueda ser capturado por las excepciones anteriores.</exception>
        public double getPromedioUsoApp()
        {
            try
            {
                double? bitacora = (from b in db.Bitacoras
                                    select b).Count();

                double? usuarios = (from u in db.Personas
                                    select u).Count();

                if (bitacora == null || usuarios == null)
                    throw Utilidades.Instancia.Fabrica.CrearExcepcion("Hay un problema con la consulta en la base de datos.");

                if (usuarios == 0)
                    throw new DivideByZeroException("Hay un error de división entre cero.");

                double promedio = (double)bitacora / (double)usuarios;

                if (Double.IsInfinity(promedio) || Double.IsNaN(promedio))
                    throw new NotFiniteNumberException("La operación retorna un tipo de dato no válido.");

                Utilidades.Instancia.Debug($"Obteniendo Reporte#5 | App: Servicio | Tipo: Preestablecido | Descripción: getPromedioUsoApp | Resultado: {promedio}.");
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
                throw Utilidades.Instancia.Fabrica.CrearExcepcion(interna: e);
            }
        }
        #endregion

        #region REPORTE #6 - Promedio de citas canceladas por médico en un tiempo determinado
        /// <summary>
        /// Método utilizado para obtener el promedio de citas canceladas por médico en un periodo de tiempo seleccionado por el usuario.
        /// </summary>
        /// <param name="fechaInicioStr">Fecha incicial para el periodo de conteo de registro de usuarios.</param>
        /// <param name="fechaFinStr">Fecha incicial para el periodo de conteo de registro de usuarios.</param>
        /// <returns>Promedio de citas canceladas en un periodo de tiempo determinado.</returns>
        /// <exception cref="System.DivideByZeroException">Excepción lanzada en caso de ocurrir un división entre cero en las operaciones realizadas por el método.</exception>
        /// <exception cref="System.NotFiniteNumberException">Excepción lanzada en caso de que las operaciones realizadas por el método arrojen un número no válido.</exception>
        /// <exception cref="System.FormatException">Excepción lanzada en caso de que el formato de los parámetros este incorrecto.</exception>
        /// <exception cref="DoctorWebException">Excepción lanzada en caso de ocurrir un problema en la consulta de la base de datos.</exception>
        /// <exception cref="System.Exception">Excepción lanzada en caso de existir algún otro tipo de error y que no pueda ser capturado por las excepciones anteriores.</exception>
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
                    throw Utilidades.Instancia.Fabrica.CrearExcepcion("Hay un problema con la consulta en la base de datos.");

                if (cantidadMedicos == 0)
                    throw new DivideByZeroException("Hay un error de división entre cero.");

                double promedio = (double)cantidadCitasCanceladas / (double)cantidadMedicos;

                if (Double.IsInfinity(promedio) || Double.IsNaN(promedio))
                    throw new NotFiniteNumberException("La operación retornó un número no válido.");

                Utilidades.Instancia.Debug($"Obteniendo Reporte#6 | App: Servicio | Tipo: Preestablecido | Descripción: getPromedioCitasCanceladasPorMedico | Resultado: {promedio} citas.");
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
                throw Utilidades.Instancia.Fabrica.CrearExcepcion(interna: e);
            }
        }
        #endregion
        #endregion

        /// <summary>
        /// Método utilizado para llenar una lista de atributos, según el parámetro recibido. 
        /// </summary>
        /// <param name="selectedEntities">Parámetro que indica las entidades seleccionadas.</param>
        /// <exception cref="DoctorWebException">Esta excepción es lanzada en caso de existir algun error en al ejecución.</exception>
        /// <exception cref="System.Exception">Esta es la excepción general, es lanzada en caso de existir un error que no fue atrapado por excepciones especificas.</exception>
        /// <returns>Retorna un objeto "resultado" que indica si fue fue exitosa o fallida la operación.</returns>
        public Dictionary<string, object> obtenerAtributos(List<string> entidades)
        {
            try
            {
                String PROJECT_MODEL_STR = "DoctorWebServiciosWCF.Models.";

                object item;
                var filtros = new Dictionary<string, object>();

                foreach (var entidad in entidades)
                {
                    // create the object from the specification string
                    var tipo = Type.GetType(PROJECT_MODEL_STR + entidad);
                    item = Activator.CreateInstance(Type.GetType(PROJECT_MODEL_STR + entidad));

                    var attributos = new Dictionary<string, string>();
                    foreach (var property in item.GetType().GetProperties())
                    {
                        if (!property.PropertyType.ToString().Contains("Models") && !property.Name.Contains("Concat") && !property.Name.Contains("Application") && !property.Name.Contains("NombreCompleto"))
                            attributos.Add(key: property.Name, value: "");
                    }
                    filtros.Add(tipo.Name, attributos);
                }

                Utilidades.Instancia.Debug($"Obteniendo Reporte | App: Servicio | Tipo: Configurado | Descripción: obtenerAtributos | Resultado: {filtros.ToString()}.");
                return filtros;
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

        public string generarReporteConfigurado(List<DatosConfigurados> datosConfigurados)
        {
            DataTable dt = new DataTable();
            var context = db;
            var conn = context.Database.Connection;
            var connectionState = conn.State;
            try
            {
                using (context)
                {
                    if (connectionState != ConnectionState.Open)
                        conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM Personas";
                        //cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add(new SqlParameter("jobCardId", 100525));
                        using (var reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                            Utilidades.Instancia.Debug($"Obteniendo Reporte | App: Servicio | Tipo: Configurado | Descripción: generarReporteConfigurado | Resultado: {dt.Columns.ToString()}.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connectionState != ConnectionState.Open)
                    conn.Close();
            }
            //return dt;

            //var query = db.Database.SqlQuery<dynamic>("SELECT * FROM Personas").ToList();

            return (Newtonsoft.Json.JsonConvert.SerializeObject(dt));
        }
    }
}