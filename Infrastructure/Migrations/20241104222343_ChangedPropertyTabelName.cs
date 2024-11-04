using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPropertyTabelName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EntityTypeBuilder`1",
                table: "EntityTypeBuilder`1");

            migrationBuilder.RenameTable(
                name: "EntityTypeBuilder`1",
                newName: "Property");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Property",
                table: "Property",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Property",
                table: "Property");

            migrationBuilder.RenameTable(
                name: "Property",
                newName: "EntityTypeBuilder`1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntityTypeBuilder`1",
                table: "EntityTypeBuilder`1",
                column: "Id");
        }
    }
}
