namespace Amazon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdminTable : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ADMIN");
        }
    }
}
