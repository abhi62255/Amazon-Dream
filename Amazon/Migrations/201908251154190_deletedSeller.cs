namespace Amazon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletedSeller : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DELETEDSELLER",
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
            DropForeignKey("dbo.DELETEDSELLER", "Seller_ID", "dbo.SELLER");
            DropIndex("dbo.DELETEDSELLER", new[] { "Seller_ID" });
            DropTable("dbo.DELETEDSELLER");
        }
    }
}
