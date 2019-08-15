namespace Amazon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Intial : DbMigration
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
                        Seller_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SELLER", t => t.Seller_ID)
                .Index(t => t.Seller_ID);
            
            CreateTable(
                "dbo.ADMIN",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 100),
                        Name = c.String(maxLength: 100),
                        Username = c.String(maxLength: 100),
                        Password = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PRODUCT",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ProductName = c.String(nullable: false, maxLength: 200),
                        ProductPrice = c.Long(nullable: false),
                        ProductQuantity = c.Int(nullable: false),
                        ProductDiscount = c.Int(nullable: false),
                        Seller_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SELLER", t => t.Seller_ID, cascadeDelete: true)
                .Index(t => t.Seller_ID);
            
            CreateTable(
                "dbo.PRODUCTDESCRPTION",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ProductCategory = c.String(nullable: false, maxLength: 100),
                        ProductSubCategory = c.String(nullable: false, maxLength: 100),
                        ProductBrand = c.String(nullable: false, maxLength: 100),
                        ProductGenderType = c.String(nullable: false),
                        ProductDescription = c.String(nullable: false, maxLength: 100),
                        Product_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PRODUCT", t => t.Product_ID, cascadeDelete: true)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.PRODUCTPICTURE",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        PicturePath = c.String(nullable: false),
                        Product_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PRODUCT", t => t.Product_ID, cascadeDelete: true)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.PRODUCTREQUEST",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Product_ID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PRODUCT", t => t.Product_ID, cascadeDelete: true)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.ProductAndDescriptions",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ProductName = c.String(nullable: false, maxLength: 200),
                        ProductPrice = c.Long(nullable: false),
                        ProductQuantity = c.Int(nullable: false),
                        ProductDiscount = c.Int(nullable: false),
                        ProductCategory = c.String(nullable: false, maxLength: 100),
                        ProductSubCategory = c.String(nullable: false, maxLength: 100),
                        ProductBrand = c.String(nullable: false, maxLength: 100),
                        ProductGenderType = c.String(nullable: false),
                        ProductDescription = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SellerAddresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SellerName = c.String(nullable: false, maxLength: 200),
                        SEmail = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 50),
                        AddressLine1 = c.String(nullable: false, maxLength: 500),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        PostalCode = c.Int(nullable: false),
                        AddressType = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SELLERREQUEST",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Seller_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SELLER", t => t.Seller_ID, cascadeDelete: true)
                .Index(t => t.Seller_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SELLERREQUEST", "Seller_ID", "dbo.SELLER");
            DropForeignKey("dbo.PRODUCT", "Seller_ID", "dbo.SELLER");
            DropForeignKey("dbo.PRODUCTREQUEST", "Product_ID", "dbo.PRODUCT");
            DropForeignKey("dbo.PRODUCTPICTURE", "Product_ID", "dbo.PRODUCT");
            DropForeignKey("dbo.PRODUCTDESCRPTION", "Product_ID", "dbo.PRODUCT");
            DropForeignKey("dbo.ADDRESS", "Seller_ID", "dbo.SELLER");
            DropForeignKey("dbo.SELLER", "Seller_ID", "dbo.SELLER");
            DropForeignKey("dbo.ADDRESS", "Customer_ID", "dbo.CUSTOMER");
            DropIndex("dbo.SELLERREQUEST", new[] { "Seller_ID" });
            DropIndex("dbo.PRODUCTREQUEST", new[] { "Product_ID" });
            DropIndex("dbo.PRODUCTPICTURE", new[] { "Product_ID" });
            DropIndex("dbo.PRODUCTDESCRPTION", new[] { "Product_ID" });
            DropIndex("dbo.PRODUCT", new[] { "Seller_ID" });
            DropIndex("dbo.SELLER", new[] { "Seller_ID" });
            DropIndex("dbo.ADDRESS", new[] { "Customer_ID" });
            DropIndex("dbo.ADDRESS", new[] { "Seller_ID" });
            DropTable("dbo.SELLERREQUEST");
            DropTable("dbo.SellerAddresses");
            DropTable("dbo.ProductAndDescriptions");
            DropTable("dbo.PRODUCTREQUEST");
            DropTable("dbo.PRODUCTPICTURE");
            DropTable("dbo.PRODUCTDESCRPTION");
            DropTable("dbo.PRODUCT");
            DropTable("dbo.ADMIN");
            DropTable("dbo.SELLER");
            DropTable("dbo.CUSTOMER");
            DropTable("dbo.ADDRESS");
        }
    }
}
