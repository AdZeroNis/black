using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IbulakStoreServer.Migrations
{
    /// <inheritdoc />
    public partial class EditBsaket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "104ea07b-774d-4fc1-a999-6f9b872255a4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "22e56f43-6563-4372-b9bf-27e1282273ac", null, "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2426167f-842e-4933-ae72-d8dfe34abf78",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "86b24d6e-10f3-4409-8197-f1e939138b2c", "AQAAAAIAAYagAAAAEOf1OyYNgh4/duDwVL46wK6SGbULEKqgQZzn7GhwPPWeLzxlRlIwxB1BoS2eGMQIQA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22e56f43-6563-4372-b9bf-27e1282273ac");

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
    }
}
