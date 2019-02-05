namespace MSC_TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rebased : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "ZipCodeID", "dbo.ZipCodes");
            DropIndex("dbo.Addresses", new[] { "ZipCodeID" });
            AddColumn("dbo.Addresses", "ZipCode", c => c.Int(nullable: false));
            DropColumn("dbo.Addresses", "ZipCodeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Addresses", "ZipCodeID", c => c.Int(nullable: false));
            DropColumn("dbo.Addresses", "ZipCode");
            CreateIndex("dbo.Addresses", "ZipCodeID");
            AddForeignKey("dbo.Addresses", "ZipCodeID", "dbo.ZipCodes", "ZipCodeID", cascadeDelete: true);
        }
    }
}
