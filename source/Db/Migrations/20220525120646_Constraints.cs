using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Db.Migrations
{
    public partial class Constraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AdventureLogs_AdventureId",
                table: "AdventureLogs");

            migrationBuilder.CreateIndex(
                name: "UC_AdventureLog_AdventureId_AdventureScriptStepId",
                table: "AdventureLogs",
                columns: new[] { "AdventureId", "AdventureScriptStepId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UC_AdventureLog_AdventureId_AdventureScriptStepId",
                table: "AdventureLogs");

            migrationBuilder.CreateIndex(
                name: "IX_AdventureLogs_AdventureId",
                table: "AdventureLogs",
                column: "AdventureId");
        }
    }
}
