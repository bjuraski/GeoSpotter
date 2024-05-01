using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoSpotter.API.Data.Migrations;

/// <inheritdoc />
public partial class AddedUserApiMessageRelation : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<long>(
            name: "UserId",
            table: "ApiMessages",
            type: "bigint",
            nullable: false,
            defaultValue: 0L);

        migrationBuilder.CreateIndex(
            name: "IX_ApiMessages_UserId",
            table: "ApiMessages",
            column: "UserId");

        migrationBuilder.AddForeignKey(
            name: "FK_ApiMessages_Users_UserId",
            table: "ApiMessages",
            column: "UserId",
            principalTable: "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_ApiMessages_Users_UserId",
            table: "ApiMessages");

        migrationBuilder.DropIndex(
            name: "IX_ApiMessages_UserId",
            table: "ApiMessages");

        migrationBuilder.DropColumn(
            name: "UserId",
            table: "ApiMessages");
    }
}
