using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZooboxApplication.Migrations
{
    public partial class adoptions_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adoption",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdoptionDate = table.Column<DateTime>(nullable: false),
                    Animal = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    AdoptionType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adoption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adoption_AdoptionType_AdoptionType",
                        column: x => x.AdoptionType,
                        principalTable: "AdoptionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Adoption_Animal_Animal",
                        column: x => x.Animal,
                        principalTable: "Animal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Adoption_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adoption_AdoptionType",
                table: "Adoption",
                column: "AdoptionType");

            migrationBuilder.CreateIndex(
                name: "IX_Adoption_Animal",
                table: "Adoption",
                column: "Animal");

            migrationBuilder.CreateIndex(
                name: "IX_Adoption_UserId",
                table: "Adoption",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adoption");
        }
    }
}
