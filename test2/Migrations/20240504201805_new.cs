using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace test2.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hitoricals_User_Id_user",
                table: "Hitoricals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hitoricals",
                table: "Hitoricals");

            migrationBuilder.DropColumn(
                name: "List",
                table: "Hitoricals");

            migrationBuilder.RenameTable(
                name: "Hitoricals",
                newName: "Historical");

            migrationBuilder.RenameIndex(
                name: "IX_Hitoricals_Id_user",
                table: "Historical",
                newName: "IX_Historical_Id_user");

            migrationBuilder.AddColumn<string>(
                name: "PicturesFolderPath",
                table: "Historical",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Historical",
                table: "Historical",
                column: "Date");

            migrationBuilder.AddForeignKey(
                name: "FK_Historical_User_Id_user",
                table: "Historical",
                column: "Id_user",
                principalTable: "User",
                principalColumn: "Id_user",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Historical_User_Id_user",
                table: "Historical");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Historical",
                table: "Historical");

            migrationBuilder.DropColumn(
                name: "PicturesFolderPath",
                table: "Historical");

            migrationBuilder.RenameTable(
                name: "Historical",
                newName: "Hitoricals");

            migrationBuilder.RenameIndex(
                name: "IX_Historical_Id_user",
                table: "Hitoricals",
                newName: "IX_Hitoricals_Id_user");

            migrationBuilder.AddColumn<byte[]>(
                name: "List",
                table: "Hitoricals",
                type: "varbinary(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hitoricals",
                table: "Hitoricals",
                column: "Date");

            migrationBuilder.AddForeignKey(
                name: "FK_Hitoricals_User_Id_user",
                table: "Hitoricals",
                column: "Id_user",
                principalTable: "User",
                principalColumn: "Id_user",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
