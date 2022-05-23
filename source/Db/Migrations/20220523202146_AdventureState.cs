using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Db.Migrations
{
    public partial class AdventureState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPending",
                table: "Adventures");

            migrationBuilder.AddColumn<int>(
                name: "AdventureStateId",
                table: "Adventures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AdventureState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdventureState", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AdventureState",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 0, "Impossible" },
                    { 1, "NotStarted" },
                    { 2, "Pending" },
                    { 3, "Finished" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adventures_AdventureStateId",
                table: "Adventures",
                column: "AdventureStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adventures_AdventureState_AdventureStateId",
                table: "Adventures",
                column: "AdventureStateId",
                principalTable: "AdventureState",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adventures_AdventureState_AdventureStateId",
                table: "Adventures");

            migrationBuilder.DropTable(
                name: "AdventureState");

            migrationBuilder.DropIndex(
                name: "IX_Adventures_AdventureStateId",
                table: "Adventures");

            migrationBuilder.DropColumn(
                name: "AdventureStateId",
                table: "Adventures");

            migrationBuilder.AddColumn<bool>(
                name: "IsPending",
                table: "Adventures",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
