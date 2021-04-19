using FluentMigrator;

namespace Opravilo.Migrator.Migrations
{
    [Migration(20210419111400)]
    public class AddRefreshTokensTable : Migration 
    {
        public override void Up()
        {
            Create.Table("RefreshTokens")
                .WithColumn("ID").AsInt64().PrimaryKey().Identity()
                .WithColumn("USER_ID").AsInt64().NotNullable().ForeignKey("Users", "ID")
                .WithColumn("REFRESH_TOKEN").AsString().NotNullable()
                .WithColumn("EXPIRATION_DATE").AsDateTime().NotNullable()
                .WithColumn("CREATED_DATE").AsDateTime().NotNullable()
                .WithColumn("CHANGED_DATE").AsDateTime().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("RefreshTokens");
        }
    }
}