using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IbulakStoreServer.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminSeedDataEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7bfa821-acfe-4855-bcce-ad2f317e9fea");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "980462ce-2769-452a-9a2c-8749573412c7", null, "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2426167f-842e-4933-ae72-d8dfe34abf78",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "PasswordHash" },
                values: new object[] { "f0fd716d-b316-4d50-8ffa-4aee6c1b92df", "shaghayeghkrimi2923@gmail.com", "shaghayeghkrimi2923@gmail.com", "AQAAAAIAAYagAAAAECde+gkxZSNVuojbtd7Y8HgCEz/np9Jgs/zb0A0CScL/0N73K9tFxYKwIP+p+Fdgtg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "980462ce-2769-452a-9a2c-8749573412c7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c7bfa821-acfe-4855-bcce-ad2f317e9fea", null, "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2426167f-842e-4933-ae72-d8dfe34abf78",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "PasswordHash" },
                values: new object[] { "a2c9e9f4-7674-4ba6-acc0-4f157a4bbd31", "hr.shahshahani@gmail.com", "hr.shahshahani@gmail.com", "AQAAAAIAAYagAAAAEPn63JUUA5CvENsLWSiFOg27urXKt+pQqj1tehWpglHF76RN93/HeXG6KQafxlw8pg==" });
        }
    }
}
