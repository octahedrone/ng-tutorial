using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Db.Migrations
{
    public partial class CurrentScriptStepId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentScriptStepId",
                table: "Adventures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Adventures_CurrentScriptStepId",
                table: "Adventures",
                column: "CurrentScriptStepId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adventures_AdventureScriptSteps_CurrentScriptStepId",
                table: "Adventures",
                column: "CurrentScriptStepId",
                principalTable: "AdventureScriptSteps",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adventures_AdventureScriptSteps_CurrentScriptStepId",
                table: "Adventures");

            migrationBuilder.DropIndex(
                name: "IX_Adventures_CurrentScriptStepId",
                table: "Adventures");

            migrationBuilder.DropColumn(
                name: "CurrentScriptStepId",
                table: "Adventures");
        }
    }
}
