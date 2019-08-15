namespace Amazon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Intial1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SELLER", "Seller_ID", "dbo.SELLER");
            DropIndex("dbo.SELLER", new[] { "Seller_ID" });
            DropColumn("dbo.SELLER", "Seller_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SELLER", "Seller_ID", c => c.Int());
            CreateIndex("dbo.SELLER", "Seller_ID");
            AddForeignKey("dbo.SELLER", "Seller_ID", "dbo.SELLER", "ID");
        }
    }
}
