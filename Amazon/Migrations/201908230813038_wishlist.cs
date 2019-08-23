namespace Amazon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wishlist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Wishlists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
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
            DropForeignKey("dbo.Wishlists", "Product_ID", "dbo.PRODUCT");
            DropForeignKey("dbo.Wishlists", "Customer_ID", "dbo.CUSTOMER");
            DropIndex("dbo.Wishlists", new[] { "Customer_ID" });
            DropIndex("dbo.Wishlists", new[] { "Product_ID" });
            DropTable("dbo.Wishlists");
        }
    }
}
