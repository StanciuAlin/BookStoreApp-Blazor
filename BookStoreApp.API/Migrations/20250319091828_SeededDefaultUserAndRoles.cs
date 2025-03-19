using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreApp.API.Migrations
{
    /// <inheritdoc />
    public partial class SeededDefaultUserAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0cc95a86-62b1-4d07-95c6-c8bf203cd7d4", null, "User", "USER" },
                    { "6743a03e-9508-436e-b18d-75543029feb8", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "9e14a82c-0f1e-47a1-9166-564c6677657a", 0, "fb63cb7f-efff-49e6-b864-99c58aceffef", "admin@bookstore.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAIAAYagAAAAEOlHyIGgX5P3C/bEO5PBDy/8XnoPYcwDOJSSqf3iojzbNxfNNSEd+ItVvK8Qs+dcng==", null, false, "03c9ca8a-7cf5-46a2-91a4-a6400269c8e4", false, "admin@bookstore.com" },
                    { "af684b54-9a92-4982-8d2c-3d1e9075fdf4", 0, "fa63cb7f-efff-49e6-b864-99c58aceffef", "user@bookstore.com", false, "System", "User", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAIAAYagAAAAEOlHyIGgX5P3C/bEO5PBDy/8XnoPYcwDOJSSqf3iojzbNxfNNSEd+ItVvK8Qs+dcng==", null, false, "02c9ca8a-7cf5-46a2-91a4-a6400269c8e4", false, "user@bookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "6743a03e-9508-436e-b18d-75543029feb8", "9e14a82c-0f1e-47a1-9166-564c6677657a" },
                    { "0cc95a86-62b1-4d07-95c6-c8bf203cd7d4", "af684b54-9a92-4982-8d2c-3d1e9075fdf4" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6743a03e-9508-436e-b18d-75543029feb8", "9e14a82c-0f1e-47a1-9166-564c6677657a" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0cc95a86-62b1-4d07-95c6-c8bf203cd7d4", "af684b54-9a92-4982-8d2c-3d1e9075fdf4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0cc95a86-62b1-4d07-95c6-c8bf203cd7d4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6743a03e-9508-436e-b18d-75543029feb8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e14a82c-0f1e-47a1-9166-564c6677657a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "af684b54-9a92-4982-8d2c-3d1e9075fdf4");
        }
    }
}
