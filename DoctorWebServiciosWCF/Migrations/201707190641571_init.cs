namespace DoctorWebServiciosWCF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Almacens",
                c => new
                    {
                        AlmacenId = c.Int(nullable: false, identity: true),
                        Disponible = c.Int(nullable: false),
                        CentroMedico_CentroMedicoId = c.Int(),
                        RecursoHospitalario_RecursoHospitalarioId = c.Int(),
                    })
                .PrimaryKey(t => t.AlmacenId)
                .ForeignKey("dbo.CentroMedicoes", t => t.CentroMedico_CentroMedicoId)
                .ForeignKey("dbo.RecursoHospitalarios", t => t.RecursoHospitalario_RecursoHospitalarioId)
                .Index(t => t.CentroMedico_CentroMedicoId)
                .Index(t => t.RecursoHospitalario_RecursoHospitalarioId);
            
            CreateTable(
                "dbo.CentroMedicoes",
                c => new
                    {
                        CentroMedicoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Rif = c.String(),
                        Direccion = c.String(),
                        Telefono = c.String(),
                    })
                .PrimaryKey(t => t.CentroMedicoId);
            
            CreateTable(
                "dbo.Citas",
                c => new
                    {
                        CitaId = c.Int(nullable: false),
                        CentroMedico_CentroMedicoId = c.Int(),
                        Paciente_PersonaId = c.Int(),
                    })
                .PrimaryKey(t => t.CitaId)
                .ForeignKey("dbo.Calendarios", t => t.CitaId)
                .ForeignKey("dbo.CentroMedicoes", t => t.CentroMedico_CentroMedicoId)
                .ForeignKey("dbo.Personas", t => t.Paciente_PersonaId)
                .Index(t => t.CitaId)
                .Index(t => t.CentroMedico_CentroMedicoId)
                .Index(t => t.Paciente_PersonaId);
            
            CreateTable(
                "dbo.Calendarios",
                c => new
                    {
                        CalendarioId = c.Int(nullable: false, identity: true),
                        HoraInicio = c.DateTime(nullable: false),
                        HoraFin = c.DateTime(nullable: false),
                        Cancelada = c.Boolean(nullable: false),
                        Disponible = c.Byte(nullable: false),
                        Medico_PersonaId = c.Int(),
                    })
                .PrimaryKey(t => t.CalendarioId)
                .ForeignKey("dbo.Personas", t => t.Medico_PersonaId)
                .Index(t => t.Medico_PersonaId);
            
            CreateTable(
                "dbo.Personas",
                c => new
                    {
                        PersonaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Apellido = c.String(nullable: false),
                        Cedula = c.String(nullable: false),
                        Genero = c.String(nullable: false),
                        Telefono = c.String(nullable: false),
                        FechaNacimiento = c.DateTime(nullable: false),
                        FechaCreacion = c.DateTime(nullable: false),
                        Email = c.String(),
                        Direccion = c.String(),
                        ApplicationUserId = c.String(),
                        Sueldo = c.Decimal(precision: 18, scale: 2),
                        TipoSangre = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        CentroMedico_CentroMedicoId = c.Int(),
                        EspecialidadMedica_EspecialidadMedicaId = c.Int(),
                    })
                .PrimaryKey(t => t.PersonaId)
                .ForeignKey("dbo.CentroMedicoes", t => t.CentroMedico_CentroMedicoId)
                .ForeignKey("dbo.EspecialidadMedicas", t => t.EspecialidadMedica_EspecialidadMedicaId)
                .Index(t => t.CentroMedico_CentroMedicoId)
                .Index(t => t.EspecialidadMedica_EspecialidadMedicaId);
            
            CreateTable(
                "dbo.EspecialidadMedicas",
                c => new
                    {
                        EspecialidadMedicaId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.EspecialidadMedicaId);
            
            CreateTable(
                "dbo.HistoriaMedicas",
                c => new
                    {
                        HistoriaMedicaId = c.Int(nullable: false, identity: true),
                        Paciente_PersonaId = c.Int(),
                    })
                .PrimaryKey(t => t.HistoriaMedicaId)
                .ForeignKey("dbo.Personas", t => t.Paciente_PersonaId)
                .Index(t => t.Paciente_PersonaId);
            
            CreateTable(
                "dbo.ObservacionDeAtencionClinicas",
                c => new
                    {
                        ObservacionDeAtencionMedicaId = c.Int(nullable: false, identity: true),
                        Observacion = c.String(nullable: false),
                        Comentario = c.String(nullable: false),
                        tipo = c.Int(nullable: false),
                        Paciente_PersonaId = c.Int(),
                    })
                .PrimaryKey(t => t.ObservacionDeAtencionMedicaId)
                .ForeignKey("dbo.Personas", t => t.Paciente_PersonaId)
                .Index(t => t.Paciente_PersonaId);
            
            CreateTable(
                "dbo.ObservacionMedicas",
                c => new
                    {
                        ObservacionMedicaId = c.Int(nullable: false, identity: true),
                        Diagnostico = c.String(nullable: false),
                        Indicacion = c.String(nullable: false),
                        Paciente_PersonaId = c.Int(),
                    })
                .PrimaryKey(t => t.ObservacionMedicaId)
                .ForeignKey("dbo.Personas", t => t.Paciente_PersonaId)
                .Index(t => t.Paciente_PersonaId);
            
            CreateTable(
                "dbo.ResultadoExamenMedicoes",
                c => new
                    {
                        ResultadoExamenMedicoID = c.Int(nullable: false, identity: true),
                        Comentario = c.String(nullable: false),
                        Paciente_PersonaId = c.Int(),
                    })
                .PrimaryKey(t => t.ResultadoExamenMedicoID)
                .ForeignKey("dbo.Personas", t => t.Paciente_PersonaId)
                .Index(t => t.Paciente_PersonaId);

            CreateTable(
               "dbo.ObservacionDeAtencionClinicas",
               c => new
               {
                   ObservacionDeAtencionMedicaId = c.Int(nullable: false, identity: true),
                   Observacion = c.String(nullable: false),
                   Comentario = c.String(nullable: false),
                   tipo = c.Int(nullable: false),
                   Paciente_PersonaId = c.Int(),
               })
               .PrimaryKey(t => t.ObservacionDeAtencionMedicaId)
               .ForeignKey("dbo.Personas", t => t.Paciente_PersonaId)
               .Index(t => t.Paciente_PersonaId);

            CreateTable(
                "dbo.ObservacionClinicaE2",
                c => new
                {
                    ObservacionDeAtencionMedicaId = c.Int(nullable: false, identity: true),
                    Observacion = c.String(nullable: false),
                    Comentario = c.String(nullable: false),
                    Tipo = c.String(nullable: false),
                    Paciente = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ObservacionDeAtencionMedicaId);

            CreateTable(
                "dbo.ObservacionMedicaE2",
                c => new
                {
                    ObservacionMedicaId = c.Int(nullable: false, identity: true),
                    Diagnostico = c.String(nullable: false),
                    Indicacion = c.String(nullable: false),
                    Paciente = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ObservacionMedicaId);

            CreateTable(
                "dbo.ResultadoE2",
                c => new
                {
                    ResultadoExamenMedicoID = c.Int(nullable: false, identity: true),
                    Comentario = c.String(nullable: false),
                    Paciente = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ResultadoExamenMedicoID);

            CreateTable(
                "dbo.Tratamientoes",
                c => new
                    {
                        TratamientoId = c.Int(nullable: false, identity: true),
                        Cita_CitaId = c.Int(),
                    })
                .PrimaryKey(t => t.TratamientoId)
                .ForeignKey("dbo.Citas", t => t.Cita_CitaId)
                .Index(t => t.Cita_CitaId);
            
            CreateTable(
                "dbo.RecursoHospitalarios",
                c => new
                    {
                        RecursoHospitalarioId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Descripcion = c.String(nullable: false),
                        Tipo = c.String(),
                        Componentes = c.String(),
                        Posologia = c.String(),
                        Recomendaciones = c.String(),
                    })
                .PrimaryKey(t => t.RecursoHospitalarioId);
            
            CreateTable(
                "dbo.Bitacoras",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Usuario = c.String(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Accion = c.String(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notificacions",
                c => new
                    {
                        NotificacionId = c.Int(nullable: false, identity: true),
                        Estado = c.Byte(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 60),
                        Descripcion = c.String(nullable: false, maxLength: 255),
                        Asunto = c.String(nullable: false, maxLength: 128),
                        Contenido = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.NotificacionId);
            
            CreateTable(
                "dbo.UsoRecursoes",
                c => new
                    {
                        UsoRecursoId = c.Int(nullable: false, identity: true),
                        Cantidad = c.Int(nullable: false),
                        Cita_CitaId = c.Int(),
                        RecursoHospitalario_RecursoHospitalarioId = c.Int(),
                    })
                .PrimaryKey(t => t.UsoRecursoId)
                .ForeignKey("dbo.Citas", t => t.Cita_CitaId)
                .ForeignKey("dbo.RecursoHospitalarios", t => t.RecursoHospitalario_RecursoHospitalarioId)
                .Index(t => t.Cita_CitaId)
                .Index(t => t.RecursoHospitalario_RecursoHospitalarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsoRecursoes", "RecursoHospitalario_RecursoHospitalarioId", "dbo.RecursoHospitalarios");
            DropForeignKey("dbo.UsoRecursoes", "Cita_CitaId", "dbo.Citas");
            DropForeignKey("dbo.Almacens", "RecursoHospitalario_RecursoHospitalarioId", "dbo.RecursoHospitalarios");
            DropForeignKey("dbo.Almacens", "CentroMedico_CentroMedicoId", "dbo.CentroMedicoes");
            DropForeignKey("dbo.Tratamientoes", "Cita_CitaId", "dbo.Citas");
            DropForeignKey("dbo.ResultadoExamenMedicoes", "Paciente_PersonaId", "dbo.Personas");
            DropForeignKey("dbo.ObservacionMedicas", "Paciente_PersonaId", "dbo.Personas");
            DropForeignKey("dbo.ObservacionDeAtencionClinicas", "Paciente_PersonaId", "dbo.Personas");
            DropForeignKey("dbo.HistoriaMedicas", "Paciente_PersonaId", "dbo.Personas");
            DropForeignKey("dbo.Citas", "Paciente_PersonaId", "dbo.Personas");
            DropForeignKey("dbo.Citas", "CentroMedico_CentroMedicoId", "dbo.CentroMedicoes");
            DropForeignKey("dbo.Calendarios", "Medico_PersonaId", "dbo.Personas");
            DropForeignKey("dbo.Personas", "EspecialidadMedica_EspecialidadMedicaId", "dbo.EspecialidadMedicas");
            DropForeignKey("dbo.Personas", "CentroMedico_CentroMedicoId", "dbo.CentroMedicoes");
            DropForeignKey("dbo.Citas", "CitaId", "dbo.Calendarios");
            DropIndex("dbo.UsoRecursoes", new[] { "RecursoHospitalario_RecursoHospitalarioId" });
            DropIndex("dbo.UsoRecursoes", new[] { "Cita_CitaId" });
            DropIndex("dbo.Tratamientoes", new[] { "Cita_CitaId" });
            DropIndex("dbo.ResultadoExamenMedicoes", new[] { "Paciente_PersonaId" });
            DropIndex("dbo.ObservacionMedicas", new[] { "Paciente_PersonaId" });
            DropIndex("dbo.ObservacionDeAtencionClinicas", new[] { "Paciente_PersonaId" });
            DropIndex("dbo.HistoriaMedicas", new[] { "Paciente_PersonaId" });
            DropIndex("dbo.Personas", new[] { "EspecialidadMedica_EspecialidadMedicaId" });
            DropIndex("dbo.Personas", new[] { "CentroMedico_CentroMedicoId" });
            DropIndex("dbo.Calendarios", new[] { "Medico_PersonaId" });
            DropIndex("dbo.Citas", new[] { "Paciente_PersonaId" });
            DropIndex("dbo.Citas", new[] { "CentroMedico_CentroMedicoId" });
            DropIndex("dbo.Citas", new[] { "CitaId" });
            DropIndex("dbo.Almacens", new[] { "RecursoHospitalario_RecursoHospitalarioId" });
            DropIndex("dbo.Almacens", new[] { "CentroMedico_CentroMedicoId" });
            DropTable("dbo.UsoRecursoes");
            DropTable("dbo.Notificacions");
            DropTable("dbo.Bitacoras");
            DropTable("dbo.RecursoHospitalarios");
            DropTable("dbo.Tratamientoes");
            DropTable("dbo.ResultadoExamenMedicoes");
            DropTable("dbo.ObservacionMedicas");
            DropTable("dbo.ObservacionDeAtencionClinicas");
            DropTable("dbo.HistoriaMedicas");
            DropTable("dbo.EspecialidadMedicas");
            DropTable("dbo.Personas");
            DropTable("dbo.Calendarios");
            DropTable("dbo.Citas");
            DropTable("dbo.CentroMedicoes");
            DropTable("dbo.Almacens");
            DropTable("dbo.ResultadoE2");
            DropTable("dbo.ObservacionMedicaE2");
            DropTable("dbo.ObservacionClinicaE2");
        }
    }
}
