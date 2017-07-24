namespace DoctorWebServiciosWCF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class milagro : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ObservacionMedicas", "Paciente", c => c.Int(nullable: false));
            AlterColumn("dbo.ObservacionMedicas", "Diagnostico", c => c.String());
            AlterColumn("dbo.ObservacionMedicas", "Indicacion", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ObservacionMedicas", "Indicacion", c => c.String(nullable: false));
            AlterColumn("dbo.ObservacionMedicas", "Diagnostico", c => c.String(nullable: false));
            DropColumn("dbo.ObservacionMedicas", "Paciente");
        }
    }
}
