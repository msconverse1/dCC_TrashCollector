namespace MSC_TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Createlogicforpickupcost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "PickupCost", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "PickupCost");
        }
    }
}
