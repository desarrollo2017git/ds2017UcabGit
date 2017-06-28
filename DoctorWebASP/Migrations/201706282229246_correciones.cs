namespace DoctorWebASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correciones : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ObservacionMedicas", "Indicacion", c => c.String(nullable: false));
            AddColumn("dbo.ResultadoExamenMedicoes", "Comentario", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ResultadoExamenMedicoes", "Comentario");
            DropColumn("dbo.ObservacionMedicas", "Indicacion");
        }
    }
}
