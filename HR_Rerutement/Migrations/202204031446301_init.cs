namespace HR_Rerutement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
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
            
            CreateTable(
                "dbo.Demandes",
                c => new
                    {
                        Id_demande = c.Int(nullable: false, identity: true),
                        Date_Creation = c.DateTime(nullable: false),
                        NomPrenom = c.String(),
                        Statut = c.Int(nullable: false),
                        RoleCreateur = c.String(),
                        Matricule_Createur = c.String(),
                        IntituleFormation = c.String(),
                        Objectif = c.String(),
                        Priorite = c.String(),
                        typeFormation = c.String(),
                        Service = c.String(),
                        DateFormation = c.DateTime(),
                        Fonction_Demande = c.String(),
                        DateDemandeR = c.DateTime(),
                        TypeRecrutement = c.String(),
                        TitreRecrutement = c.String(),
                        Nb_poste = c.Int(),
                        Mission = c.String(),
                        Contrat = c.String(),
                        Diplome = c.String(),
                        Nb_experience = c.String(),
                        Remarque = c.String(),
                        Affectation = c.String(),
                        Savepath = c.String(),
                        NomCollaborateur = c.String(),
                        FileName = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Permission_Id_compte = c.Int(),
                    })
                .PrimaryKey(t => t.Id_demande)
                .ForeignKey("dbo.Permissions", t => t.Permission_Id_compte)
                .Index(t => t.Permission_Id_compte);
            
            CreateTable(
                "dbo.Demandeurs",
                c => new
                    {
                        Empl_Matricule = c.String(nullable: false, maxLength: 128),
                        Empl_Nom_Prenom = c.String(),
                        Empl_MatResponsable = c.String(),
                        Empl_NumPro = c.String(),
                        Empl_Fonction = c.String(),
                        Empl_CentreCout = c.String(),
                        Empl_Service = c.String(),
                        Empl_Active = c.Boolean(),
                        Empl_Email = c.String(),
                        image = c.String(),
                        PathImage = c.String(),
                        Permission_Id_compte = c.Int(),
                    })
                .PrimaryKey(t => t.Empl_Matricule)
                .ForeignKey("dbo.Permissions", t => t.Permission_Id_compte)
                .Index(t => t.Permission_Id_compte);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        Id_compte = c.Int(nullable: false, identity: true),
                        Empl_Matricule = c.String(),
                        Password = c.String(),
                        Role_Id_Role = c.Int(),
                    })
                .PrimaryKey(t => t.Id_compte)
                .ForeignKey("dbo.Roles", t => t.Role_Id_Role)
                .Index(t => t.Role_Id_Role);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id_Role = c.Int(nullable: false, identity: true),
                        Role1 = c.String(),
                        desc = c.String(),
                    })
                .PrimaryKey(t => t.Id_Role);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Demandeurs", "Permission_Id_compte", "dbo.Permissions");
            DropForeignKey("dbo.Permissions", "Role_Id_Role", "dbo.Roles");
            DropForeignKey("dbo.Demandes", "Permission_Id_compte", "dbo.Permissions");
            DropIndex("dbo.Permissions", new[] { "Role_Id_Role" });
            DropIndex("dbo.Demandeurs", new[] { "Permission_Id_compte" });
            DropIndex("dbo.Demandes", new[] { "Permission_Id_compte" });
            DropTable("dbo.Roles");
            DropTable("dbo.Permissions");
            DropTable("dbo.Demandeurs");
            DropTable("dbo.Demandes");
            DropTable("dbo.Configs");
        }
    }
}
