namespace Amazon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderplaced : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderPlaceds",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        OrderNumber = c.String(),
                        Quantity = c.Int(nullable: false),
                        Amount = c.Long(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        PaymentType = c.String(),
                        Status = c.String(),
                        Address = c.String(),
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
            DropForeignKey("dbo.OrderPlaceds", "Product_ID", "dbo.PRODUCT");
            DropForeignKey("dbo.OrderPlaceds", "Customer_ID", "dbo.CUSTOMER");
            DropIndex("dbo.OrderPlaceds", new[] { "Customer_ID" });
            DropIndex("dbo.OrderPlaceds", new[] { "Product_ID" });
            DropTable("dbo.OrderPlaceds");
        }
    }
}
