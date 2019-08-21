namespace Amazon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feedback : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FEEDBACK",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Rating = c.Int(nullable: false),
                        Review = c.String(nullable: false),
                        Product_ID = c.Long(nullable: false),
                        Customer_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CUSTOMER", t => t.Customer_ID, cascadeDelete: true)
                .ForeignKey("dbo.PRODUCT", t => t.Product_ID, cascadeDelete: true)
                .Index(t => t.Product_ID)
                .Index(t => t.Customer_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FEEDBACK", "Product_ID", "dbo.PRODUCT");
            DropForeignKey("dbo.FEEDBACK", "Customer_ID", "dbo.CUSTOMER");
            DropIndex("dbo.FEEDBACK", new[] { "Customer_ID" });
            DropIndex("dbo.FEEDBACK", new[] { "Product_ID" });
            DropTable("dbo.FEEDBACK");
        }
    }
}
