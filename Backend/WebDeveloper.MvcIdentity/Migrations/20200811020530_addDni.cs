using Microsoft.EntityFrameworkCore.Migrations;

namespace WebDeveloper.MvcIdentity.Migrations
{
    public partial class addDni : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Dni",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dni",
                table: "AspNetUsers");
        }
    }
}
