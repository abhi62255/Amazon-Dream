namespace Amazon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletedProduct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DELETEDPRODUCT",
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
            DropForeignKey("dbo.DELETEDPRODUCT", "Product_ID", "dbo.PRODUCT");
            DropIndex("dbo.DELETEDPRODUCT", new[] { "Product_ID" });
            DropTable("dbo.DELETEDPRODUCT");
        }
    }
}
