using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IbulakStoreServer.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a2a2df88-2952-408d-9c34-eca9177d92ac", null, "Admin", "ADMIN" },
                    { "c7bfa821-acfe-4855-bcce-ad2f317e9fea", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2426167f-842e-4933-ae72-d8dfe34abf78", 0, "a2c9e9f4-7674-4ba6-acc0-4f157a4bbd31", "hr.shahshahani@gmail.com", true, "شقایق کریمی", false, null, "hr.shahshahani@gmail.com", "09119660028", "AQAAAAIAAYagAAAAEPn63JUUA5CvENsLWSiFOg27urXKt+pQqj1tehWpglHF76RN93/HeXG6KQafxlw8pg==", null, true, "", false, "09119660028" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a2a2df88-2952-408d-9c34-eca9177d92ac", "2426167f-842e-4933-ae72-d8dfe34abf78" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7bfa821-acfe-4855-bcce-ad2f317e9fea");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a2a2df88-2952-408d-9c34-eca9177d92ac", "2426167f-842e-4933-ae72-d8dfe34abf78" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2a2df88-2952-408d-9c34-eca9177d92ac");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2426167f-842e-4933-ae72-d8dfe34abf78");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");
        }
    }
}
