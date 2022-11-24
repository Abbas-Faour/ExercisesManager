using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ExercisesManager.Data.Migrations
{
    public partial class InitialState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(225)", maxLength: 225, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(225)", maxLength: 225, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateBy = table.Column<string>(type: "character varying(225)", maxLength: 225, nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserExercise",
                columns: table => new
                {
                    ExerciseId = table.Column<long>(type: "bigint", nullable: false),
                    ApplicationUserId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(225)", maxLength: 225, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdateBy = table.Column<string>(type: "character varying(225)", maxLength: 225, nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExercise", x => new { x.ApplicationUserId, x.ExerciseId });
                    table.ForeignKey(
                        name: "FK_UserExercise_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "Duration", "IsDeleted", "Name", "UpdateAt", "UpdateBy" },
                values: new object[,]
                {
                    { 1L, new DateTime(2022, 11, 24, 8, 36, 16, 724, DateTimeKind.Local).AddTicks(6611), "Data-Seed", "Exercise 1 Description", new TimeSpan(0, 0, 10, 0, 0), null, "Exercise 1", null, null },
                    { 2L, new DateTime(2022, 11, 24, 8, 36, 16, 724, DateTimeKind.Local).AddTicks(6671), "Data-Seed", "Exercise 2 Description", new TimeSpan(0, 0, 10, 0, 0), null, "Exercise 2", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserExercise_ExerciseId",
                table: "UserExercise",
                column: "ExerciseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserExercise");

            migrationBuilder.DropTable(
                name: "Exercises");
        }
    }
}
