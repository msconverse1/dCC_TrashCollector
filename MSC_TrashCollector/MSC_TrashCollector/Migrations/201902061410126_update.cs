namespace MSC_TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "SuspendedDayId", "dbo.SuspendedDays");
            DropIndex("dbo.Customers", new[] { "SuspendedDayId" });
            AlterColumn("dbo.Customers", "SuspendedDayId", c => c.Int());
            CreateIndex("dbo.Customers", "SuspendedDayId");
            AddForeignKey("dbo.Customers", "SuspendedDayId", "dbo.SuspendedDays", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "SuspendedDayId", "dbo.SuspendedDays");
            DropIndex("dbo.Customers", new[] { "SuspendedDayId" });
            AlterColumn("dbo.Customers", "SuspendedDayId", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "SuspendedDayId");
            AddForeignKey("dbo.Customers", "SuspendedDayId", "dbo.SuspendedDays", "ID", cascadeDelete: true);
        }
    }
}
