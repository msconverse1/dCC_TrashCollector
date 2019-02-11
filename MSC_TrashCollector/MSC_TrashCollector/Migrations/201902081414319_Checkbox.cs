namespace MSC_TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Checkbox : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "TrashPickup", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "TrashPickup");
        }
    }
}
