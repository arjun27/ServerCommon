namespace NuGet.Services.Validation
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValidationSetCompleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PackageValidationSets", "Completed", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PackageValidationSets", "Completed");
        }
    }
}
