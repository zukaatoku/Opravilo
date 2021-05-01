using FluentMigrator;

namespace Opravilo.Migrator.Migrations
{
    [Migration(20210501111400)]
    public class AddUserVkIdDisplayNameColumnsAndSetPasswordNullable : Migration 
    {
        public override void Up()
        {
            Alter.Table("Users")
                .AddColumn("VK_USER_ID").AsString().Nullable().Unique() // todo: может ли айдишник быть текстовым? проверить на ком-нибудь не с id2123123
                .AddColumn("DISPLAY_NAME").AsString().NotNullable()
                .AlterColumn("PASSWORD_HASH").AsString().Nullable();
        }

        public override void Down()
        {
            Delete
                .Column("VK_USER_ID")
                .Column("DISPLAY_NAME")
                .FromTable("Users");

            Alter.Table("Users")
                .AlterColumn("PASSWORD_HASH").AsString().NotNullable();
        }
    }
}