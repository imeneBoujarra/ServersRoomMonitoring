using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace test2.Migrations
{
    /// <inheritdoc />
    public partial class updatedServerRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Checkliste_ServerRoomId",
                table: "Checkliste");

            migrationBuilder.CreateIndex(
                name: "IX_Checkliste_ServerRoomId",
                table: "Checkliste",
                column: "ServerRoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Checkliste_ServerRoomId",
                table: "Checkliste");

            migrationBuilder.CreateIndex(
                name: "IX_Checkliste_ServerRoomId",
                table: "Checkliste",
                column: "ServerRoomId",
                unique: true);
        }
    }
}
