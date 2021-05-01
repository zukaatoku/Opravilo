using FluentMigrator;

namespace Opravilo.Migrator.Migrations
{
    [Migration(20210501111400)]
    public class AddUserEmailAndVkIdColumns : Migration 
    {
        public override void Up()
        {
            Alter.Table("Users")
                .AddColumn("EMAIL").AsString().Nullable().Unique()
                .AddColumn("VK_USER_ID").AsString().Nullable().Unique(); // todo: может ли айдишник быть текстовым? проверить на ком-нибудь не с id2123123
        }

        public override void Down()
        {
            Delete
                .Column("EMAIL")
                .Column("VK_USER_ID")
                .FromTable("Users");
        }
    }
}