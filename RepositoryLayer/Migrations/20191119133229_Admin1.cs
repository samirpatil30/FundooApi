namespace RepositoryLayer.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;
    public partial class Admin1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "AspNetUsers");
        }
    }
}
