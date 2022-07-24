using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace To_Do_List.Infrastructure.Migrations
{
    public partial class InitialCommit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tareas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "VARCHAR (50)", maxLength: 50, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoftDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tareas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tareas",
                columns: new[] { "Id", "CreationDate", "DeleteDate", "Estado", "ModificationDate", "Nombre", "SoftDelete" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 7, 24, 13, 28, 48, 675, DateTimeKind.Local).AddTicks(2434), null, false, new DateTime(2022, 7, 24, 13, 28, 48, 680, DateTimeKind.Local).AddTicks(5452), "Aprender C#", false },
                    { 2, new DateTime(2022, 7, 24, 13, 28, 48, 681, DateTimeKind.Local).AddTicks(1980), null, true, new DateTime(2022, 7, 24, 13, 28, 48, 681, DateTimeKind.Local).AddTicks(2010), "Aprender HTML", false },
                    { 3, new DateTime(2022, 7, 24, 13, 28, 48, 681, DateTimeKind.Local).AddTicks(2023), null, true, new DateTime(2022, 7, 24, 13, 28, 48, 681, DateTimeKind.Local).AddTicks(2027), "Aprender CSS", false },
                    { 4, new DateTime(2022, 7, 24, 13, 28, 48, 681, DateTimeKind.Local).AddTicks(2035), null, true, new DateTime(2022, 7, 24, 13, 28, 48, 681, DateTimeKind.Local).AddTicks(2040), "Aprender JS", false },
                    { 5, new DateTime(2022, 7, 24, 13, 28, 48, 681, DateTimeKind.Local).AddTicks(2046), null, true, new DateTime(2022, 7, 24, 13, 28, 48, 681, DateTimeKind.Local).AddTicks(2050), "Aprender React", false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tareas");
        }
    }
}
