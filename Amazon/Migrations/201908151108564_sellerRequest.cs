namespace Amazon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sellerRequest : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("dbo.SELLER", "Seller_ID", c => c.Int());
            CreateIndex("dbo.SELLER", "Seller_ID");
            AddForeignKey("dbo.SELLER", "Seller_ID", "dbo.SELLER", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SELLERREQUEST", "Seller_ID", "dbo.SELLER");
            DropForeignKey("dbo.SELLER", "Seller_ID", "dbo.SELLER");
            DropIndex("dbo.SELLERREQUEST", new[] { "Seller_ID" });
            DropIndex("dbo.SELLER", new[] { "Seller_ID" });
            DropColumn("dbo.SELLER", "Seller_ID");
            DropTable("dbo.SELLERREQUEST");
        }
    }
}
