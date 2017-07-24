namespace DoctorWebASP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class thefix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ObservacionMedicas", "Paciente", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ObservacionMedicas", "Paciente");
        }
    }
}
