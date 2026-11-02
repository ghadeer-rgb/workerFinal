using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace workerFinal.Migrations
{
    /// <inheritdoc />
    public partial class AddFavoritesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        //{
        { 
        //    migrationBuilder.RenameColumn(
        //        name: "ccreatedAt",
        //        table: "users",
        //        newName: "createdAt");

            migrationBuilder.AddColumn<string>(
                name: "profileImageUrl",
                table: "taskerProfiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "profileImageUrl",
                table: "taskerProfiles");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "users",
                newName: "ccreatedAt");
        }
    }
}
