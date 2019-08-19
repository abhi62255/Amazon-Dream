namespace Amazon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kartDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KART", "DateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.KART", "DateTime");
        }
    }
}
