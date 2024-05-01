using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoSpotter.API.Data.Migrations;

/// <inheritdoc />
public partial class AddedUserFavouriteLocationRelation : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateIndex(
            name: "IX_FavouriteLocations_UserId",
            table: "FavouriteLocations",
            column: "UserId");

        migrationBuilder.AddForeignKey(
            name: "FK_FavouriteLocations_Users_UserId",
            table: "FavouriteLocations",
            column: "UserId",
            principalTable: "Users",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_FavouriteLocations_Users_UserId",
            table: "FavouriteLocations");

        migrationBuilder.DropIndex(
            name: "IX_FavouriteLocations_UserId",
            table: "FavouriteLocations");
    }
}
