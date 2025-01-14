using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRestrictions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inquiry_Property_PropertyId",
                table: "Inquiry");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_UserInformation_UserId",
                table: "Property");

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiry_Property_PropertyId",
                table: "Inquiry",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Property_UserInformation_UserId",
                table: "Property",
                column: "UserId",
                principalTable: "UserInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inquiry_Property_PropertyId",
                table: "Inquiry");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_UserInformation_UserId",
                table: "Property");

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiry_Property_PropertyId",
                table: "Inquiry",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Property_UserInformation_UserId",
                table: "Property",
                column: "UserId",
                principalTable: "UserInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
