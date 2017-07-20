using DoctorWebServiciosWCF.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoctorWebServiciosWCF.Models.ORM
{
    public class ContextoBD : DbContext
    {
        public ContextoBD() :
            base("DoctorWebBD")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Calendario> Calendarios { get; set; }
        public DbSet<EspecialidadMedica> EspecialidadesMedicas { get; set; }
        public DbSet<CentroMedico> CentrosMedicos { get; set; }
        public DbSet<RecursoHospitalario> RecursosHospitalarios { get; set; }
        public DbSet<Almacen> Almacenes { get; set; }
        public DbSet<UsoRecurso> UsoRecursos { get; set; }
        public DbSet<Bitacora> Bitacoras { get; set; }
        public DbSet<ObservacionMedica> ObservacionMedicas { get; set; }
        public DbSet<ResultadoExamenMedico> ResultadoExamenMedicos { get; set; }
        public DbSet<ObservacionDeAtencionClinica> ObservacionDeAtencionClincas { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Calendario>()
                .HasOptional(c => c.Cita)
                .WithRequired(c => c.Calendario);
        }
    }
}