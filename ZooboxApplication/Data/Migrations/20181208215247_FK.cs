using Microsoft.EntityFrameworkCore.Migrations;

namespace ZooboxApplication.Data.Migrations
{
    public partial class FK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
          
            migrationBuilder.AddColumn<int>(
                name: "DiseaseNameId",
                table: "Animal",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RaceNameId",
                table: "Animal",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animal_DiseaseNameId",
                table: "Animal",
                column: "DiseaseNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Animal_RaceNameId",
                table: "Animal",
                column: "RaceNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_DiseaseAnimal_DiseaseNameId",
                table: "Animal",
                column: "DiseaseNameId",
                principalTable: "DiseaseAnimal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Race_RaceNameId",
                table: "Animal",
                column: "RaceNameId",
                principalTable: "Race",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_DiseaseAnimal_DiseaseNameId",
                table: "Animal");

            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Race_RaceNameId",
                table: "Animal");

            migrationBuilder.DropIndex(
                name: "IX_Animal_DiseaseNameId",
                table: "Animal");

            migrationBuilder.DropIndex(
                name: "IX_Animal_RaceNameId",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "DiseaseNameId",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "RaceNameId",
                table: "Animal");

            migrationBuilder.CreateIndex(
                name: "IX_Animal_Disease",
                table: "Animal",
                column: "Disease");

            migrationBuilder.CreateIndex(
                name: "IX_Animal_Race",
                table: "Animal",
                column: "Race");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_DiseaseAnimal_Disease",
                table: "Animal",
                column: "Disease",
                principalTable: "DiseaseAnimal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Race_Race",
                table: "Animal",
                column: "Race",
                principalTable: "Race",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
