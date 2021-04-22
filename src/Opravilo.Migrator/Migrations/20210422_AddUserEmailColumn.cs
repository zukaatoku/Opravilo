using FluentMigrator;

namespace Opravilo.Migrator.Migrations
{
    [Migration(20210422200700)]
    public class AddUserEmailColumn : Migration 
    {
        public override void Up()
        {
            Alter.Table("Users")
                .AddColumn("EMAIL").AsString().NotNullable().Unique();
        }

        public override void Down()
        {
            Delete.Column("EMAIL")
                .FromTable("Users");
        }
    }
}