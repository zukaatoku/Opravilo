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
                .WithColumn("CREATED_DATE").AsDateTime().NotNullable()
                .WithColumn("CHANGED_DATE").AsDateTime().NotNullable();

            Create.Table("ProjectPermissions")
                .WithColumn("ID").AsInt64().PrimaryKey().Identity()
                .WithColumn("NAME").AsString().NotNullable();

            Insert.IntoTable("ProjectPermissions")
                .Row(GetRow(0, "Creator"))
                .Row(GetRow(1, "ProjectRW"))
                .Row(GetRow(2, "ProjectRO"))
                .Row(GetRow(3, "ProjectSettingsW"));
            
            Create.Table("ProjectUsers")
                .WithColumn("ID").AsInt64().PrimaryKey().Identity()
                .WithColumn("PROJECT_ID").AsInt64().NotNullable().ForeignKey("Projects", "ID")
                .WithColumn("USER_ID").AsInt64().NotNullable().ForeignKey("Users", "ID")
                .WithColumn("PERMISSION_ID").AsInt64().NotNullable().ForeignKey("ProjectPermissions", "ID")
                .WithColumn("CREATED_DATE").AsDateTime().NotNullable()
                .WithColumn("CHANGED_DATE").AsDateTime().NotNullable();
        }

        private Dictionary<string, object> GetRow(int id, string name)
        {
            var row = new Dictionary<string, object>();
            row.Add("ID", id);
            row.Add("NAME", name);
            return row;
        }

        public override void Down()
        {
            Delete.Table("ProjectUsers");
            Delete.Table("ProjectPermissions");
            Delete.Table("Projects");
        }
    }
}