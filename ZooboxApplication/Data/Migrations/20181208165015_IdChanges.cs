using Microsoft.EntityFrameworkCore.Migrations;

namespace ZooboxApplication.Data.Migrations
{
    public partial class IdChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "State",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Specie",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Race",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "DiseaseAnimal",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Animal",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "State",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Specie",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Race",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DiseaseAnimal",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Animal",
                newName: "ID");
        }
    }
}
