using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppManager.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:user_status", "inactive,active,suspended")
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "users",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    status = table.Column<string>(type: "text", nullable: false, defaultValue: "Active", comment: "Options value: \r\n					Active, \r\n					Inactive, \r\n					Suspended"),
                    is_enable = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "idx_user_name",
                schema: "dbo",
                table: "users",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users",
                schema: "dbo");
        }
    }
}
