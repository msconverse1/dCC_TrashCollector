namespace MSC_TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmployeedayChoice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "DayChosen", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "DayChosen");
        }
    }
}
