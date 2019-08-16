namespace Amazon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class trend : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TREND",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Product_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PRODUCT", t => t.Product_ID, cascadeDelete: true)
                .Index(t => t.Product_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TREND", "Product_ID", "dbo.PRODUCT");
            DropIndex("dbo.TREND", new[] { "Product_ID" });
            DropTable("dbo.TREND");
        }
    }
}
