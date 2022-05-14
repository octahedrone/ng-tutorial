using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Db.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdventureScripts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdventureScripts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Adventures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdventureScriptId = table.Column<int>(type: "int", nullable: false),
                    Started = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPending = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adventures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adventures_AdventureScripts_AdventureScriptId",
                        column: x => x.AdventureScriptId,
                        principalTable: "AdventureScripts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdventureScriptSteps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdventureScriptId = table.Column<int>(type: "int", nullable: false),
                    ParentStepId = table.Column<int>(type: "int", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdventureScriptSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdventureScriptSteps_AdventureScripts_AdventureScriptId",
                        column: x => x.AdventureScriptId,
                        principalTable: "AdventureScripts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdventureScriptSteps_AdventureScriptSteps_ParentStepId",
                        column: x => x.ParentStepId,
                        principalTable: "AdventureScriptSteps",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AdventureLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdventureId = table.Column<int>(type: "int", nullable: false),
                    AdventureScriptStepId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdventureLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdventureLogs_Adventures_AdventureId",
                        column: x => x.AdventureId,
                        principalTable: "Adventures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdventureLogs_AdventureScriptSteps_AdventureScriptStepId",
                        column: x => x.AdventureScriptStepId,
                        principalTable: "AdventureScriptSteps",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdventureLogs_AdventureId",
                table: "AdventureLogs",
                column: "AdventureId");

            migrationBuilder.CreateIndex(
                name: "IX_AdventureLogs_AdventureScriptStepId",
                table: "AdventureLogs",
                column: "AdventureScriptStepId");

            migrationBuilder.CreateIndex(
                name: "IX_Adventures_AdventureScriptId",
                table: "Adventures",
                column: "AdventureScriptId");

            migrationBuilder.CreateIndex(
                name: "IX_AdventureScriptSteps_AdventureScriptId",
                table: "AdventureScriptSteps",
                column: "AdventureScriptId");

            migrationBuilder.CreateIndex(
                name: "IX_AdventureScriptSteps_ParentStepId",
                table: "AdventureScriptSteps",
                column: "ParentStepId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdventureLogs");

            migrationBuilder.DropTable(
                name: "Adventures");

            migrationBuilder.DropTable(
                name: "AdventureScriptSteps");

            migrationBuilder.DropTable(
                name: "AdventureScripts");
        }
    }
}
