using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sodio_API_Task.Migrations
{
    /// <inheritdoc />
    public partial class tablecreationandseedingdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Description", "DueDate", "Status", "Title" },
                values: new object[,]
                {
                    { 1, "Good Morning", new DateTime(2024, 12, 11, 12, 42, 13, 205, DateTimeKind.Local).AddTicks(1264), 2, "Morning" },
                    { 2, "Good Afternoon", new DateTime(2024, 12, 11, 12, 42, 13, 205, DateTimeKind.Local).AddTicks(1277), 1, "Afternoon" },
                    { 3, "Good Evening", new DateTime(2024, 12, 11, 12, 42, 13, 205, DateTimeKind.Local).AddTicks(1278), 0, "Evening" },
                    { 4, "Good Night", new DateTime(2024, 12, 11, 12, 42, 13, 205, DateTimeKind.Local).AddTicks(1280), 0, "Night" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
