using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GeoSpotter.API.Data.Migrations;

/// <inheritdoc />
public partial class InitialSeedOfUsers : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "Users",
            columns: new[] { "Id", "Password", "UserName" },
            values: new object[,]
            {
                { 1L, "password1", "user1" },
                { 2L, "password2", "user2" }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "Users",
            keyColumn: "Id",
            keyValue: 1L);

        migrationBuilder.DeleteData(
            table: "Users",
            keyColumn: "Id",
            keyValue: 2L);
    }
}
