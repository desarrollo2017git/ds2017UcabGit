namespace DoctorWebASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class agregandonuevasclases : DbMigration
    {
        public override void Up()
        {
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
                        Paciente_PersonaId = c.Int(),
                    })
                .PrimaryKey(t => t.ResultadoExamenMedicoID)
                .ForeignKey("dbo.Personas", t => t.Paciente_PersonaId)
                .Index(t => t.Paciente_PersonaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResultadoExamenMedicoes", "Paciente_PersonaId", "dbo.Personas");
            DropForeignKey("dbo.ObservacionMedicas", "Paciente_PersonaId", "dbo.Personas");
            DropForeignKey("dbo.ObservacionDeAtencionClinicas", "Paciente_PersonaId", "dbo.Personas");
            DropIndex("dbo.ResultadoExamenMedicoes", new[] { "Paciente_PersonaId" });
            DropIndex("dbo.ObservacionMedicas", new[] { "Paciente_PersonaId" });
            DropIndex("dbo.ObservacionDeAtencionClinicas", new[] { "Paciente_PersonaId" });
            DropTable("dbo.ResultadoExamenMedicoes");
            DropTable("dbo.ObservacionMedicas");
            DropTable("dbo.ObservacionDeAtencionClinicas");
        }
    }
}
