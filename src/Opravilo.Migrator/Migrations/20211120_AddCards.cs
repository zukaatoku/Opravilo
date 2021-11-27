using System.Data;
using FluentMigrator;

namespace Opravilo.Migrator.Migrations
{
    [Migration(20211120110100)]
    public class AddCards : Migration
    {
        public override void Up()
        {
            Create
                .Table("Cards")
                .WithColumn("ID").AsInt64().PrimaryKey().Identity()
                .WithColumn("NAME").AsString().NotNullable()
                .WithColumn("DESCRIPTION").AsString().NotNullable()
                .WithColumn("STATE_ID").AsInt64().NotNullable().ForeignKey("States", "ID").OnDeleteOrUpdate(Rule.Cascade)
                .WithColumn("CREATED_DATE").AsDateTime().NotNullable()
                .WithColumn("CHANGED_DATE").AsDateTime().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Cards");
        }
    }
}