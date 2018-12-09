using Microsoft.EntityFrameworkCore.Migrations;

namespace ZooboxApplication.Migrations
{
    public partial class StateAnimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Animal",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Animal_State",
                table: "Animal",
                column: "State");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_State_State",
                table: "Animal",
                column: "State",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_State_State",
                table: "Animal");

            migrationBuilder.DropIndex(
                name: "IX_Animal_State",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Animal");
        }
    }
}
