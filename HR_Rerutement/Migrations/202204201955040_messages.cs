namespace HR_Rerutement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class messages : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "id_receiver", c => c.String());
            AlterColumn("dbo.Messages", "id_sender", c => c.String());
            AlterColumn("dbo.Messages", "is_read", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "is_read", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Messages", "id_sender", c => c.Int(nullable: false));
            AlterColumn("dbo.Messages", "id_receiver", c => c.Int(nullable: false));
        }
    }
}
