namespace Amazon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cityState : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CITY",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                        State_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.STATE", t => t.State_ID, cascadeDelete: true)
                .Index(t => t.State_ID);
            
            CreateTable(
                "dbo.STATE",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StateName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CITY", "State_ID", "dbo.STATE");
            DropIndex("dbo.CITY", new[] { "State_ID" });
            DropTable("dbo.STATE");
            DropTable("dbo.CITY");
        }
    }
}
