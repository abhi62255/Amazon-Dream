namespace Amazon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderplaced1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.OrderPlaceds", newName: "ORDERPLACED");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ORDERPLACED", newName: "OrderPlaceds");
        }
    }
}
