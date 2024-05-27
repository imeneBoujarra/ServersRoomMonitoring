using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace test2.Migrations
{
    /// <inheritdoc />
    public partial class foeingnKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "List",
                table: "Hitoricals",
                type: "varbinary(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "Id_user",
                table: "Hitoricals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hitoricals_Id_user",
                table: "Hitoricals",
                column: "Id_user");

            migrationBuilder.AddForeignKey(
                name: "FK_Hitoricals_User_Id_user",
                table: "Hitoricals",
                column: "Id_user",
                principalTable: "User",
                principalColumn: "Id_user",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hitoricals_User_Id_user",
                table: "Hitoricals");

            migrationBuilder.DropIndex(
                name: "IX_Hitoricals_Id_user",
                table: "Hitoricals");

            migrationBuilder.AlterColumn<byte[]>(
                name: "List",
                table: "Hitoricals",
                type: "varbinary(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<int>(
                name: "Id_user",
                table: "Hitoricals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
