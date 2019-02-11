namespace MSC_TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApiKey1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "key", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "key");
        }
    }
}
