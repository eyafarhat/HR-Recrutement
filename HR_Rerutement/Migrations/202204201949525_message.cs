namespace HR_Rerutement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class message : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        id_mess = c.Int(nullable: false, identity: true),
                        id_receiver = c.Int(nullable: false),
                        id_sender = c.Int(nullable: false),
                        contenu = c.String(),
                        is_read = c.Boolean(nullable: false),
                        date_create = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id_mess);
            
        }
        
        public override void Down()
        {
            
        }
    }
}
