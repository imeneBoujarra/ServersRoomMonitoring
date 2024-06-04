using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace test2.Migrations
{
    /// <inheritdoc />
    public partial class updatedEntitiesServerRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChecklistHistorical");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Historical",
                table: "Historical");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Historical");

            migrationBuilder.DropColumn(
                name: "Hour",
                table: "Historical");

            migrationBuilder.DropColumn(
                name: "QRCodeUrl",
                table: "Checkliste");

            migrationBuilder.AddColumn<string>(
                name: "QRCodeUrl",
                table: "ServersRoom",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "VerifyBackbone",
                table: "ServersRoom",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VerifyHeat",
                table: "ServersRoom",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VerifySecurity",
                table: "ServersRoom",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VerifyStorage",
                table: "ServersRoom",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VerifySwitchers",
                table: "ServersRoom",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VerifyVentilation",
                table: "ServersRoom",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Historical",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "Historical",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "State",
                table: "Checkliste",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Historical",
                table: "Historical",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Historical_ChecklistId",
                table: "Historical",
                column: "ChecklistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Historical_Checkliste_ChecklistId",
                table: "Historical",
                column: "ChecklistId",
                principalTable: "Checkliste",
                principalColumn: "IdChecklist",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Historical_Checkliste_ChecklistId",
                table: "Historical");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Historical",
                table: "Historical");

            migrationBuilder.DropIndex(
                name: "IX_Historical_ChecklistId",
                table: "Historical");

            migrationBuilder.DropColumn(
                name: "QRCodeUrl",
                table: "ServersRoom");

            migrationBuilder.DropColumn(
                name: "VerifyBackbone",
                table: "ServersRoom");

            migrationBuilder.DropColumn(
                name: "VerifyHeat",
                table: "ServersRoom");

            migrationBuilder.DropColumn(
                name: "VerifySecurity",
                table: "ServersRoom");

            migrationBuilder.DropColumn(
                name: "VerifyStorage",
                table: "ServersRoom");

            migrationBuilder.DropColumn(
                name: "VerifySwitchers",
                table: "ServersRoom");

            migrationBuilder.DropColumn(
                name: "VerifyVentilation",
                table: "ServersRoom");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Historical");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Historical");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Checkliste");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Historical",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Hour",
                table: "Historical",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "QRCodeUrl",
                table: "Checkliste",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Historical",
                table: "Historical",
                column: "Date");

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
                name: "IX_ChecklistHistorical_HistoricalsDate",
                table: "ChecklistHistorical",
                column: "HistoricalsDate");
        }
    }
}
