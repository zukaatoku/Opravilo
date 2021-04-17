using FluentMigrator;

namespace Opravilo.Migrator.Migrations
{
    [Migration(20210417220300)]
    public class AddUsersTable : Migration 
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("ID").AsInt64().PrimaryKey().Identity()
                .WithColumn("LOGIN").AsString().NotNullable()
                .WithColumn("PASSWORD_HASH").AsString().NotNullable()
                .WithColumn("CREATED_DATE").AsDateTime().NotNullable()
                .WithColumn("CHANGED_DATE").AsDateTime().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}