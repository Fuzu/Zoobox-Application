using Microsoft.EntityFrameworkCore.Migrations;

namespace ZooboxApplication.Migrations
{
    public partial class DonationAddFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Donation",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Donation",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                 name: "Description",
                 table: "Donation");

            migrationBuilder.DropColumn(
               name: "Status",
               table: "Donation");
        }
    }
}
