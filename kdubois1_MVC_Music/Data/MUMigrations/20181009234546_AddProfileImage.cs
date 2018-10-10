using Microsoft.EntityFrameworkCore.Migrations;

namespace kdubois1_MVC_Music.Data.MUMigrations
{
    public partial class AddProfileImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePicPath",
                schema: "MU",
                table: "Musicians",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicPath",
                schema: "MU",
                table: "Musicians");
        }
    }
}
