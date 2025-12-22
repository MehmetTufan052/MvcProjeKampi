namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_adminhashsalt_add : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "PasswordHash", c => c.String());
            AddColumn("dbo.Admins", "Salt", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Admins", "Salt");
            DropColumn("dbo.Admins", "PasswordHash");
        }
    }
}
