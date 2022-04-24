namespace HR_Rerutement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bhgf : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Demandes", "CauseRefusFormation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Demandes", "CauseRefusFormation");
        }
    }
}
