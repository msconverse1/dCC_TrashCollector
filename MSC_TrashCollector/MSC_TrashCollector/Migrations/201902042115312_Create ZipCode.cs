namespace MSC_TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateZipCode : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ZipCodes",
                c => new
                    {
                        ZipCodeID = c.Int(nullable: false, identity: true),
                        ZipcodeArea = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ZipCodeID);
            
            AddColumn("dbo.Addresses", "ZipCodeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Addresses", "ZipCodeID");
            AddForeignKey("dbo.Addresses", "ZipCodeID", "dbo.ZipCodes", "ZipCodeID", cascadeDelete: true);
            DropColumn("dbo.Addresses", "ZipCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Addresses", "ZipCode", c => c.Int(nullable: false));
            DropForeignKey("dbo.Addresses", "ZipCodeID", "dbo.ZipCodes");
            DropIndex("dbo.Addresses", new[] { "ZipCodeID" });
            DropColumn("dbo.Addresses", "ZipCodeID");
            DropTable("dbo.ZipCodes");
        }
    }
}
