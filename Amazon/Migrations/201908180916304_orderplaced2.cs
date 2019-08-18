namespace Amazon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderplaced2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ORDERPLACED", "OrderNumber", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ORDERPLACED", "OrderNumber", c => c.String());
        }
    }
}
