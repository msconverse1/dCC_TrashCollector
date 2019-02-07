namespace MSC_TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Customeruserid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "ANUserID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "ANUserID");
        }
    }
}
