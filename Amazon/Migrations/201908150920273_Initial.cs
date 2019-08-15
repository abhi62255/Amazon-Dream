namespace Amazon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ADDRESS",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AddressLine1 = c.String(nullable: false, maxLength: 500),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        PostalCode = c.Int(nullable: false),
                        AddressType = c.String(),
                        Seller_ID = c.Int(),
                        Customer_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CUSTOMER", t => t.Customer_ID)
                .ForeignKey("dbo.SELLER", t => t.Seller_ID)
                .Index(t => t.Seller_ID)
                .Index(t => t.Customer_ID);
            
            CreateTable(
                "dbo.CUSTOMER",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 70),
                        Email = c.String(nullable: false, maxLength: 70),
                        Password = c.String(nullable: false, maxLength: 50),
                        Gender = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SELLER",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SellerName = c.String(nullable: false, maxLength: 200),
                        SEmail = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ADDRESS", "Seller_ID", "dbo.SELLER");
            DropForeignKey("dbo.ADDRESS", "Customer_ID", "dbo.CUSTOMER");
            DropIndex("dbo.ADDRESS", new[] { "Customer_ID" });
            DropIndex("dbo.ADDRESS", new[] { "Seller_ID" });
            DropTable("dbo.SellerAddresses");
            DropTable("dbo.SELLER");
            DropTable("dbo.CUSTOMER");
            DropTable("dbo.ADDRESS");
        }
    }
}
