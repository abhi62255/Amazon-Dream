namespace Amazon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Kart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KART",
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
            DropForeignKey("dbo.KART", "Product_ID", "dbo.PRODUCT");
            DropForeignKey("dbo.KART", "Customer_ID", "dbo.CUSTOMER");
            DropIndex("dbo.KART", new[] { "Customer_ID" });
            DropIndex("dbo.KART", new[] { "Product_ID" });
            DropTable("dbo.KART");
        }
    }
}
