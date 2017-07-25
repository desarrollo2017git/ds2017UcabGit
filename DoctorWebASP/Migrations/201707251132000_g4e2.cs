namespace DoctorWebASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class g4e2 : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ResultadoE2");
            DropTable("dbo.ObservacionMedicaE2");
            DropTable("dbo.ObservacionClinicaE2");
        }
    }
}
