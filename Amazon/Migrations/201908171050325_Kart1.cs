namespace Amazon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Kart1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KART", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.KART", "Quantity");
        }
    }
}
