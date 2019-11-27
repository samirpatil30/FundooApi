using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class Admin3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ServiceId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
