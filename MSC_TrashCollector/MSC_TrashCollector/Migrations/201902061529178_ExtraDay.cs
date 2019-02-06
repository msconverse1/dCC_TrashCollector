namespace MSC_TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtraDay : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExtraPickupDates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ExtraDay = c.String(),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExtraPickupDates", "CustomerId", "dbo.Customers");
            DropIndex("dbo.ExtraPickupDates", new[] { "CustomerId" });
            DropTable("dbo.ExtraPickupDates");
        }
    }
}
