namespace Amazon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wishlist1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Wishlists", newName: "WISHLIST");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.WISHLIST", newName: "Wishlists");
        }
    }
}
