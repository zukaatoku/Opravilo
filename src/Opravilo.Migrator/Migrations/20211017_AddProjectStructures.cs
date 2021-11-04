using System.Collections.Generic;
using FluentMigrator;

namespace Opravilo.Migrator.Migrations
{
    [Migration(20211017120700)]
    public class AddProjectStructures : Migration 
    {
        public override void Up()
        {
            Create
                .Table("Projects")
                .WithColumn("ID").AsInt64().PrimaryKey().Identity()
                .WithColumn("NAME").AsString().NotNullable()
                .WithColumn("DESCRIPTION").AsString().NotNullable()
                .WithColumn("CREATOR_ID").AsInt64().NotNullable().ForeignKey("Users", "ID")
                .WithColumn("CREATED_DATE").AsDateTime().NotNullable()
                .WithColumn("CHANGED_DATE").AsDateTime().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Projects");
        }
    }
}