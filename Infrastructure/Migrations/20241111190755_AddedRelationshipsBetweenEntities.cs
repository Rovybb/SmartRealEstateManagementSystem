using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelationshipsBetweenEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Property",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "User",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Company",
                table: "User",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Property_UserId",
                table: "Property",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_BuyerId",
                table: "Payment",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_PropertyId",
                table: "Payment",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_SellerId",
                table: "Payment",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiry_AgentId",
                table: "Inquiry",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiry_ClientId",
                table: "Inquiry",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiry_PropertyId",
                table: "Inquiry",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiry_Property_PropertyId",
                table: "Inquiry",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiry_User_AgentId",
                table: "Inquiry",
                column: "AgentId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Inquiry_User_ClientId",
                table: "Inquiry",
                column: "ClientId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Property_PropertyId",
                table: "Payment",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_User_BuyerId",
                table: "Payment",
                column: "BuyerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_User_SellerId",
                table: "Payment",
                column: "SellerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Property_User_UserId",
                table: "Property",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inquiry_Property_PropertyId",
                table: "Inquiry");

            migrationBuilder.DropForeignKey(
                name: "FK_Inquiry_User_AgentId",
                table: "Inquiry");

            migrationBuilder.DropForeignKey(
                name: "FK_Inquiry_User_ClientId",
                table: "Inquiry");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Property_PropertyId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_User_BuyerId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_User_SellerId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_User_UserId",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Property_UserId",
                table: "Property");

            migrationBuilder.DropIndex(
                name: "IX_Payment_BuyerId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_PropertyId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_SellerId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Inquiry_AgentId",
                table: "Inquiry");

            migrationBuilder.DropIndex(
                name: "IX_Inquiry_ClientId",
                table: "Inquiry");

            migrationBuilder.DropIndex(
                name: "IX_Inquiry_PropertyId",
                table: "Inquiry");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Property",
                newName: "CreatedBy");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Company",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
