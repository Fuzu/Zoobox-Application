using Microsoft.EntityFrameworkCore.Migrations;

namespace ZooboxApplication.Data.Migrations
{
    public partial class Animal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         migrationBuilder.AddColumn<int>(
                name: "Race",
                table: "Animal",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Animal_Race",
                table: "Animal",
                column: "Race");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Race_Race",
                table: "Animal",
                column: "Race",
                principalTable: "Race",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Race_Race",
                table: "Animal");

            migrationBuilder.DropIndex(
                name: "IX_Animal_Race",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "Race",
                table: "Animal");

            migrationBuilder.AddColumn<int>(
                name: "AnimalID",
                table: "Race",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Race_AnimalID",
                table: "Race",
                column: "AnimalID");

            migrationBuilder.AddForeignKey(
                name: "FK_Race_Animal_AnimalID",
                table: "Race",
                column: "AnimalID",
                principalTable: "Animal",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
