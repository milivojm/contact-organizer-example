using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactOrganizer.Data.SqlServer.Migrations
{
    public partial class FullAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullAddress",
                table: "Contacts",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullAddress",
                table: "Contacts");
        }
    }
}
