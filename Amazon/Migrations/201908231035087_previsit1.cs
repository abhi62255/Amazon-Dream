namespace Amazon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class previsit1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PreVisits", newName: "PREVISIT");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.PREVISIT", newName: "PreVisits");
        }
    }
}
