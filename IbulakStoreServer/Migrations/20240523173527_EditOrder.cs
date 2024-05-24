using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IbulakStoreServer.Migrations
{
    /// <inheritdoc />
    public partial class EditOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cfad0d31-c0e8-41d2-894f-f27320d55f47");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "104ea07b-774d-4fc1-a999-6f9b872255a4", null, "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2426167f-842e-4933-ae72-d8dfe34abf78",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "df27819b-fd8f-4ae8-b2fa-3d3faae8a70a", "AQAAAAIAAYagAAAAENX2YUZacSGS018l/B7NfJpNVrEV8LjnmZfjkI9TM56Hs6MxAOutq0C5IU85Rl61rQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "104ea07b-774d-4fc1-a999-6f9b872255a4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cfad0d31-c0e8-41d2-894f-f27320d55f47", null, "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2426167f-842e-4933-ae72-d8dfe34abf78",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b5acdc6d-2bba-4b92-946e-d4356c793e98", "AQAAAAIAAYagAAAAEDBL6s6JPZ0lCgaL2YPo3rYMzSNK4VNOMBiGKyzhhz+urcEehSki9GWvw44OpedyYg==" });
        }
    }
}
