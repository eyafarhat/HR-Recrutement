namespace HR_Rerutement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conf : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Configs",
                c => new
                    {
                        key = c.Int(nullable: false, identity: true),
                        val = c.Int(),
                        DataType = c.String(),
                        Description = c.String(),
                        IsActive = c.Int(),
                    })
                .PrimaryKey(t => t.key);
            
        }
        
        public override void Down()
        {
            
        }
    }
}
