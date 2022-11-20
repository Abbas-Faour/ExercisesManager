using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExercisesManager.Data.Migrations
{
    public partial class DatabaseSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 1L, "aec99abf-854d-403d-b529-adf8a86c4ec8", "ADMIN", null });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1L, 0, "c1babddc-9d48-4f29-bf4d-b3f26c6978a6", new DateTime(2000, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Abbasfaour25@gmail.com", false, "Abbas", "Faour", false, null, null, null, "AQAAAAEAACcQAAAAEO02dJ7DSxClltxzhBuZJo1+xat/FZE03jV5KfAZSpHCwY+BpQfxE/aOnhLfKakTbQ==", "71435810", true, null, false, "abbasfaour" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1L, 1L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1L, 1L });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
