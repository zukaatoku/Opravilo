using System.Data;
using FluentMigrator;

namespace Opravilo.Migrator.Migrations
{
    [Migration(20211106113800)]
    public class AddStates : Migration 
    {
        public override void Up()
        {
            Create
                .Table("States")
                .WithColumn("ID").AsInt64().PrimaryKey().Identity()
                .WithColumn("NAME").AsString().NotNullable()
                .WithColumn("PROJECT_ID").AsInt64().NotNullable().ForeignKey("Projects", "ID").OnDeleteOrUpdate(Rule.Cascade)
                .WithColumn("CREATED_DATE").AsDateTime().NotNullable()
                .WithColumn("CHANGED_DATE").AsDateTime().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("States");
        }
    }
}