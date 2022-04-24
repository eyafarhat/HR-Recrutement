namespace HR_Rerutement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rtgygh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Demandes", "CauseRefus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Demandes", "CauseRefus");
        }
    }
}
