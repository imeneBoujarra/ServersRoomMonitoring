using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace test2.Migrations
{
    /// <inheritdoc />
    public partial class updatedRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChecklistId",
                table: "Historical",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServerRoomId",
                table: "Checkliste",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ChecklistHistorical",
                columns: table => new
                {
                    ChecklistIdChecklist = table.Column<int>(type: "int", nullable: false),
                    HistoricalsDate = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistHistorical", x => new { x.ChecklistIdChecklist, x.HistoricalsDate });
                    table.ForeignKey(
                        name: "FK_ChecklistHistorical_Checkliste_ChecklistIdChecklist",
                        column: x => x.ChecklistIdChecklist,
                        principalTable: "Checkliste",
                        principalColumn: "IdChecklist",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChecklistHistorical_Historical_HistoricalsDate",
                        column: x => x.HistoricalsDate,
                        principalTable: "Historical",
                        principalColumn: "Date",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Checkliste_ServerRoomId",
                table: "Checkliste",
                column: "ServerRoomId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistHistorical_HistoricalsDate",
                table: "ChecklistHistorical",
                column: "HistoricalsDate");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkliste_ServersRoom_ServerRoomId",
                table: "Checkliste",
                column: "ServerRoomId",
                principalTable: "ServersRoom",
                principalColumn: "Id_Room",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkliste_ServersRoom_ServerRoomId",
                table: "Checkliste");

            migrationBuilder.DropTable(
                name: "ChecklistHistorical");

            migrationBuilder.DropIndex(
                name: "IX_Checkliste_ServerRoomId",
                table: "Checkliste");

            migrationBuilder.DropColumn(
                name: "ChecklistId",
                table: "Historical");

            migrationBuilder.DropColumn(
                name: "ServerRoomId",
                table: "Checkliste");
        }
    }
}
