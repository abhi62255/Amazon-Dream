namespace Amazon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class searchHistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SEARCHHISTORY",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SearchTag = c.String(),
                        Date = c.DateTime(nullable: false),
                        Customer_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CUSTOMER", t => t.Customer_ID, cascadeDelete: true)
                .Index(t => t.Customer_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SEARCHHISTORY", "Customer_ID", "dbo.CUSTOMER");
            DropIndex("dbo.SEARCHHISTORY", new[] { "Customer_ID" });
            DropTable("dbo.SEARCHHISTORY");
        }
    }
}
