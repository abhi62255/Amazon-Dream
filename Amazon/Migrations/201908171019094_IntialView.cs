namespace Amazon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntialView : DbMigration
    {
        public override void Up()
        {
           
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductPictureDescrptions");
        }
    }
}
