﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorApp1.Migrations.ToDoData
{
    /// <inheritdoc />
    public partial class InitTodo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cpr",
                columns: table => new
                {
                    Cprid = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    User = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    CprNr = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cpr__C0242C1E46EA26CE", x => x.Cprid);
                });

            migrationBuilder.CreateTable(
                name: "TodoList",
                columns: table => new
                {
                    todoId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: true),
                    item = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TodoList__E5C578A103984B01", x => x.todoId);
                    table.ForeignKey(
                        name: "FK__TodoList__UserId__267ABA7A",
                        column: x => x.UserId,
                        principalTable: "Cpr",
                        principalColumn: "Cprid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoList_UserId",
                table: "TodoList",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoList");

            migrationBuilder.DropTable(
                name: "Cpr");
        }
    }
}
