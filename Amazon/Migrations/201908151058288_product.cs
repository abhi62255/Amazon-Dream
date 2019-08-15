namespace Amazon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class product : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PRODUCT",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ProductName = c.String(nullable: false, maxLength: 200),
                        ProductPrice = c.Long(nullable: false),
                        ProductQuantity = c.Int(nullable: false),
                        ProductDiscount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PRODUCTPICTURE", "Product_ID", "dbo.PRODUCT");
            DropForeignKey("dbo.PRODUCTDESCRPTION", "Product_ID", "dbo.PRODUCT");
            DropIndex("dbo.PRODUCTPICTURE", new[] { "Product_ID" });
            DropIndex("dbo.PRODUCTDESCRPTION", new[] { "Product_ID" });
            DropTable("dbo.PRODUCTPICTURE");
            DropTable("dbo.PRODUCTDESCRPTION");
            DropTable("dbo.PRODUCT");
        }
    }
}
