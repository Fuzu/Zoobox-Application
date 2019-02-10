using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZooboxApplication.Migrations
{
    public partial class Sponsorship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Story_Animal_AnimalId",
                table: "Story");

            migrationBuilder.AlterColumn<int>(
                name: "AnimalId",
                table: "Story",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Sponsorship",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Status = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    AnimalId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sponsorship", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sponsorship_Animal_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sponsorship_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sponsorship_AnimalId",
                table: "Sponsorship",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Sponsorship_UserId",
                table: "Sponsorship",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Story_Animal_AnimalId",
                table: "Story",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Story_Animal_AnimalId",
                table: "Story");

            migrationBuilder.DropTable(
                name: "Sponsorship");

            migrationBuilder.AlterColumn<int>(
                name: "AnimalId",
                table: "Story",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Story_Animal_AnimalId",
                table: "Story",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
